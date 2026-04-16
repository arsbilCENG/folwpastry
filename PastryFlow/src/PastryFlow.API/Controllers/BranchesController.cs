using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _branchService;

    public BranchesController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBranches([FromQuery] BranchType? type)
    {
        var result = await _branchService.GetBranchesAsync(type);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBranch(Guid id)
    {
        var result = await _branchService.GetBranchByIdAsync(id);
        if (!result.Success) return NotFound(result);
        return Ok(result);
    }
}
