using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Common;
using PastryFlow.Application.DTOs.Wallet;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;

namespace PastryFlow.API.Controllers;

[ApiController]
[Route("api/wallets")]
[Authorize(Roles = "Admin")]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletsController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _walletService.GetWalletSummaryAsync();
        return Ok(new ApiResponse<WalletSummaryDto> { Success = true, Data = result });
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> GetBranchWallet(Guid branchId)
    {
        var result = await _walletService.GetBranchWalletAsync(branchId);
        return Ok(new ApiResponse<BranchWalletDto> { Success = true, Data = result });
    }

    [HttpPost("initial-balance")]
    public async Task<IActionResult> SetInitialBalance([FromBody] SetInitialBalanceRequest request)
    {
        await _walletService.SetInitialBalanceAsync(request, GetUserId());
        return Ok(new ApiResponse<bool> { Success = true, Data = true, Message = "Başlangıç bakiyesi ayarlandı." });
    }

    [HttpPost("transfer/branch-to-admin")]
    public async Task<IActionResult> TransferBranchToAdmin([FromBody] TransferRequest request)
    {
        await _walletService.TransferBranchToAdminAsync(request, GetUserId());
        return Ok(new ApiResponse<bool> { Success = true, Data = true, Message = "Transfer işlemi başarılı." });
    }

    [HttpPost("transfer/admin-to-branch")]
    public async Task<IActionResult> TransferAdminToBranch([FromBody] TransferRequest request)
    {
        await _walletService.TransferAdminToBranchAsync(request, GetUserId());
        return Ok(new ApiResponse<bool> { Success = true, Data = true, Message = "Transfer işlemi başarılı." });
    }

    [HttpPost("adjustment")]
    public async Task<IActionResult> ManualAdjustment([FromBody] ManualAdjustmentRequest request)
    {
        await _walletService.ManualAdjustmentAsync(request, GetUserId());
        return Ok(new ApiResponse<bool> { Success = true, Data = true, Message = "Bakiye düzeltme başarılı." });
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] Guid? branchId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var result = await _walletService.GetTransactionsAsync(branchId, startDate, endDate);
        return Ok(new ApiResponse<List<WalletTransactionDto>> { Success = true, Data = result });
    }
}
