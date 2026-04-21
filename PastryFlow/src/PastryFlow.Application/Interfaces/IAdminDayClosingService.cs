using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.DTOs.DayClosing;

namespace PastryFlow.Application.Interfaces;

public interface IAdminDayClosingService
{
    Task<ApiResponse<DayClosingSummaryDto>> GetBranchDayClosingAsync(Guid branchId, DateOnly date);
    Task<ApiResponse<DailySummaryItemDto>> CorrectDayClosingDetailAsync(Guid dayClosingId, DayClosingCorrectionDto dto, Guid currentUserId);
}
