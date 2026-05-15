using PastryFlow.Application.DTOs.Wallet;
using PastryFlow.Domain.Enums;

namespace PastryFlow.Application.Interfaces;

public interface IWalletService
{
    // Bakiye sorgulama
    Task<WalletSummaryDto> GetWalletSummaryAsync();
    Task<BranchWalletDto> GetBranchWalletAsync(Guid branchId);

    // Başlangıç bakiyesi (Admin — tek seferlik, sonra güncelleme)
    Task SetInitialBalanceAsync(SetInitialBalanceRequest request, Guid adminUserId);

    // Otomatik — gün kapanışında çağrılır
    Task ApplyDayClosingRevenueAsync(
        Guid branchId, decimal cashRevenue, decimal bankRevenue, Guid closedByUserId);

    // Otomatik — satın alım anında çağrılır
    Task ApplyPurchaseDeductionAsync(
        Guid branchId, WalletType walletType, decimal amount,
        string purchaseNumber, Guid createdByUserId);

    // Otomatik — satın alım iptalinde çağrılır
    Task RevertPurchaseDeductionAsync(
        Guid branchId, WalletType walletType, decimal amount,
        string purchaseNumber, Guid createdByUserId);

    // Admin satın alımları için
    Task ApplyAdminPurchaseDeductionAsync(
        WalletType walletType, decimal amount,
        string purchaseNumber, Guid createdByUserId);

    Task RevertAdminPurchaseDeductionAsync(
        WalletType walletType, decimal amount,
        string purchaseNumber, Guid createdByUserId);

    // Manuel — Admin
    Task TransferBranchToAdminAsync(TransferRequest request, Guid adminUserId);
    Task TransferAdminToBranchAsync(TransferRequest request, Guid adminUserId);
    Task ManualAdjustmentAsync(ManualAdjustmentRequest request, Guid adminUserId);

    // Hareket geçmişi
    Task<List<WalletTransactionDto>> GetTransactionsAsync(
        Guid? branchId, DateTime? startDate, DateTime? endDate);
}
