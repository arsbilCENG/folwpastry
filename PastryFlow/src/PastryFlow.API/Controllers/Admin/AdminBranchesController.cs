using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.Admin;
using PastryFlow.Application.Interfaces;

namespace PastryFlow.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/branches")]
public class AdminBranchesController : ControllerBase
{
    private readonly IAdminBranchService _branchService;

    public AdminBranchesController(IAdminBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches()
    {
        var result = await _branchService.GetBranchesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranch(Guid id)
    {
        var result = await _branchService.GetBranchByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBranch(Guid id, UpdateBranchDto dto)
    {
        var result = await _branchService.UpdateBranchAsync(id, dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
