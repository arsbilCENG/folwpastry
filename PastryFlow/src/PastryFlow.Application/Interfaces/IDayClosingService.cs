using System;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.DayClosing;

namespace PastryFlow.Application.Interfaces;

public interface IDayClosingService
{
    Task<ApiResponse<string>> SaveCountAsync(CountInputDto dto);
    Task<ApiResponse<string>> SaveCarryOverAsync(CarryOverInputDto dto);
    
    // Kasa Sayımı
    Task<ApiResponse<ExpectedCashDto>> CalculateExpectedCashAsync(Guid branchId, DateOnly date);
    Task<ApiResponse<DayClosingSummaryDto>> SubmitCashCountAsync(Guid dayClosingId, CashCountDto dto, Guid currentUserId);
    
    // Fotoğraflar
    Task<ApiResponse<string>> UpdateReceiptPhotoAsync(Guid dayClosingId, string photoUrl);
    Task<ApiResponse<string>> UpdateCounterPhotoAsync(Guid dayClosingId, string photoUrl);

    Task<ApiResponse<DayClosingSummaryDto>> CloseDayAsync(Guid branchId, DateOnly date, Guid closedByUserId);
    Task<ApiResponse<DayClosingSummaryDto>> GetSummaryAsync(Guid branchId, DateOnly date);
}
