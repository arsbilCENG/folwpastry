using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Purchases;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Entities;
using PastryFlow.Domain.Enums;
namespace PastryFlow.Application.Services;


public class PurchaseService : IPurchaseService
{
    private readonly IPastryFlowDbContext _context;
    private readonly IWalletService _walletService;

    public PurchaseService(IPastryFlowDbContext context, IWalletService walletService)
    {
        _context = context;
        _walletService = walletService;
    }

    public async Task<PagedResult<PurchaseDto>> GetPurchasesAsync(
        Guid branchId, PaginationParams pagination,
        DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Purchases
            .Include(p => p.Branch)
            .Include(p => p.CreatedByUser)
            .Include(p => p.Items).ThenInclude(i => i.Product)
            .Where(p => p.BranchId == branchId && !p.IsDeleted)
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(p => p.PurchaseDate >= startDate.Value.Date);
        if (endDate.HasValue)
            query = query.Where(p => p.PurchaseDate <= endDate.Value.Date.AddDays(1).AddTicks(-1));

        query = query.OrderByDescending(p => p.PurchaseDate).ThenByDescending(p => p.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<PurchaseDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = total,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };
    }

    public async Task<PurchaseDto> GetPurchaseByIdAsync(Guid id)
    {
        var purchase = await _context.Purchases
            .Include(p => p.Branch)
            .Include(p => p.CreatedByUser)
            .Include(p => p.Items).ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception("Satın alım bulunamadı.");

        return MapToDto(purchase);
    }

    public async Task<PurchaseDto> CreatePurchaseAsync(Guid branchId, Guid userId, CreatePurchaseDto dto)
    {
        if (!dto.Items.Any())
            throw new Exception("En az bir kalem girilmelidir.");

        // PurchaseNumber oluştur: PUR-2026-0001
        var year = DateTime.Now.Year;
        var lastNumber = await _context.Purchases
            .Where(p => p.PurchaseNumber.StartsWith($"PUR-{year}-"))
            .CountAsync();
        var purchaseNumber = $"PUR-{year}-{(lastNumber + 1):D4}";

        var purchase = new Purchase
        {
            Id = Guid.NewGuid(),
            PurchaseNumber = purchaseNumber,
            BranchId = branchId,
            PurchaseDate = dto.PurchaseDate.Date,
            PaymentMethod = dto.PaymentMethod,
            Notes = dto.Notes,
            CreatedByUserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        decimal totalAmount = 0;

        foreach (var itemDto in dto.Items)
        {
            // Ürün varsa bilgilerini al
            Product? product = null;
            bool affectsStock = false;

            if (itemDto.ProductId.HasValue)
            {
                product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == itemDto.ProductId.Value && !p.IsDeleted);

                if (product != null)
                    affectsStock = product.TrackingType == TrackingType.Purchased;
            }

            var totalPrice = itemDto.Quantity * itemDto.UnitPrice;
            totalAmount += totalPrice;

            var item = new PurchaseItem
            {
                Id = Guid.NewGuid(),
                PurchaseId = purchase.Id,
                ProductId = itemDto.ProductId,
                ItemName = !string.IsNullOrWhiteSpace(itemDto.ItemName)
                    ? itemDto.ItemName
                    : (product?.Name ?? "Ürün"),
                Quantity = itemDto.Quantity,
                Unit = itemDto.Unit,
                UnitPrice = itemDto.UnitPrice,
                TotalPrice = totalPrice,
                AffectsStock = affectsStock,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            purchase.Items.Add(item);

            // Stok güncelle
            if (affectsStock && product != null)
            {
                var stock = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.BranchId == branchId && s.ProductId == product.Id);

                if (stock == null)
                {
                    stock = new Stock
                    {
                        Id = Guid.NewGuid(),
                        BranchId = branchId,
                        ProductId = product.Id,
                        CurrentQuantity = itemDto.Quantity,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Stocks.Add(stock);
                }
                else
                {
                    stock.CurrentQuantity += itemDto.Quantity;
                    stock.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        purchase.TotalAmount = totalAmount;
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();

        // Ödeme yöntemine göre wallet tipi belirle
        var walletType = purchase.PaymentMethod == PaymentMethod.Cash
            ? WalletType.Cash
            : WalletType.Bank;

        await _walletService.ApplyPurchaseDeductionAsync(
            purchase.BranchId,
            walletType,
            purchase.TotalAmount,
            purchase.PurchaseNumber,
            userId);

        return await GetPurchaseByIdAsync(purchase.Id);
    }

    public async Task<PurchaseDto> UploadReceiptPhotoAsync(Guid id, IFormFile photo, string webRootPath)
    {
        var purchase = await _context.Purchases.FindAsync(id)
            ?? throw new Exception("Satın alım bulunamadı.");

        var photoUrl = await FileUploadHelper.SaveFileAsync(photo, "purchases", webRootPath);
        purchase.ReceiptPhotoUrl = photoUrl;
        purchase.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetPurchaseByIdAsync(id);
    }

    public async Task DeletePurchaseAsync(Guid id, Guid userId, bool isAdmin)
    {
        var purchase = await _context.Purchases
            .Include(p => p.Items).ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception("Satın alım bulunamadı.");

        // Sadece admin veya aynı gün oluşturan silebilir
        if (!isAdmin)
        {
            if (purchase.CreatedByUserId != userId)
                throw new Exception("Bu satın alımı silme yetkiniz yok.");
            
            // local time comparison might be better but UtcNow is safer for backend
            if (purchase.CreatedAt.Date != DateTime.UtcNow.Date)
                throw new Exception("Sadece bugün oluşturulan satın alımlar silinebilir.");
        }

        // Stok geri al
        foreach (var item in purchase.Items.Where(i => i.AffectsStock && i.ProductId.HasValue))
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.BranchId == purchase.BranchId && s.ProductId == item.ProductId);

            if (stock != null)
            {
                stock.CurrentQuantity = Math.Max(0, stock.CurrentQuantity - item.Quantity);
                stock.UpdatedAt = DateTime.UtcNow;
            }
        }

        purchase.IsDeleted = true;
        purchase.DeletedAt = DateTime.UtcNow;
        purchase.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var walletType = purchase.PaymentMethod == PaymentMethod.Cash
            ? WalletType.Cash
            : WalletType.Bank;

        await _walletService.RevertPurchaseDeductionAsync(
            purchase.BranchId,
            walletType,
            purchase.TotalAmount,
            purchase.PurchaseNumber,
            userId);
    }

    public async Task<PagedResult<PurchaseDto>> GetAllPurchasesAsync(
        PaginationParams pagination, Guid? branchId = null,
        DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Purchases
            .Include(p => p.Branch)
            .Include(p => p.CreatedByUser)
            .Include(p => p.Items).ThenInclude(i => i.Product)
            .Where(p => !p.IsDeleted)
            .AsQueryable();

        if (branchId.HasValue)
            query = query.Where(p => p.BranchId == branchId.Value);
        if (startDate.HasValue)
            query = query.Where(p => p.PurchaseDate >= startDate.Value.Date);
        if (endDate.HasValue)
            query = query.Where(p => p.PurchaseDate <= endDate.Value.Date.AddDays(1).AddTicks(-1));

        query = query.OrderByDescending(p => p.PurchaseDate).ThenByDescending(p => p.CreatedAt);

        var total = await query.CountAsync();
        var items = await query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<PurchaseDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = total,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };
    }

    private static PurchaseDto MapToDto(Purchase p) => new()
    {
        Id = p.Id,
        PurchaseNumber = p.PurchaseNumber,
        BranchId = p.BranchId,
        BranchName = p.Branch?.Name ?? string.Empty,
        PurchaseDate = p.PurchaseDate,
        PaymentMethod = p.PaymentMethod,
        TotalAmount = p.TotalAmount,
        ReceiptPhotoUrl = p.ReceiptPhotoUrl,
        Notes = p.Notes,
        CreatedByUserName = p.CreatedByUser?.Email ?? string.Empty,
        CreatedAt = p.CreatedAt,
        Items = p.Items.Where(i => !i.IsDeleted).Select(i => new PurchaseItemDto
        {
            Id = i.Id,
            ProductId = i.ProductId,
            ProductName = i.Product?.Name,
            ItemName = i.ItemName,
            Quantity = i.Quantity,
            Unit = i.Unit,
            UnitPrice = i.UnitPrice,
            TotalPrice = i.TotalPrice,
            AffectsStock = i.AffectsStock
        }).ToList()
    };
}
