namespace PastryFlow.Domain.Enums;

public enum WalletTransactionType
{
    InitialBalance = 0,      // Başlangıç bakiyesi
    DayClosingCash = 1,      // Gün sonu nakit ciro
    DayClosingBank = 2,      // Gün sonu kart ciro
    PurchaseDeduction = 3,   // Satın alım kesintisi
    PurchaseRefund = 4,      // Satın alım iptali iadesi
    BranchToAdmin = 5,       // Şube → Admin para çekme
    AdminToBranch = 6,       // Admin → Şube para gönderme
    ManualAdjustment = 7,     // Manuel düzeltme
    CakeOrderDeposit = 8,     // Kapora
    CakeOrderFinalPayment = 9 // Kalan ödeme
}
