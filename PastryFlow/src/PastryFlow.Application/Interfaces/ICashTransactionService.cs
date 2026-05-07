using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.CashTransactions;

namespace PastryFlow.Application.Interfaces;

public interface ICashTransactionService
{
    // Admin: Para çek/yatır
    Task<CashTransactionDto> CreateTransactionAsync(Guid adminUserId, CreateCashTransactionDto dto);
    
    // Şube kasa hareketleri (tarih aralığı)
    Task<PagedResult<CashTransactionDto>> GetTransactionsAsync(
        Guid branchId, PaginationParams pagination,
        DateTime? startDate = null, DateTime? endDate = null);
    
    // Admin: Tüm şubeler
    Task<PagedResult<CashTransactionDto>> GetAllTransactionsAsync(
        PaginationParams pagination,
        Guid? branchId = null,
        DateTime? startDate = null,
        DateTime? endDate = null);
    
    // Şube kasa özeti
    Task<BranchCashSummaryDto> GetBranchCashSummaryAsync(Guid branchId, DateTime date);
    
    // Admin: Tüm şubelerin kasa özetleri
    Task<List<BranchCashSummaryDto>> GetAllBranchCashSummariesAsync(DateTime date);
    
    // Silme (sadece admin, aynı gün)
    Task DeleteTransactionAsync(Guid id, Guid adminUserId);
}
