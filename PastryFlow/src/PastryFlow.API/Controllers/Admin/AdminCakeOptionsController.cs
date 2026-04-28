using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.CustomCakeOrders;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.API.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/cake-options")]
public class AdminCakeOptionsController : ControllerBase
{
    private readonly ICakeOptionService _cakeOptionService;

    public AdminCakeOptionsController(ICakeOptionService cakeOptionService)
    {
        _cakeOptionService = cakeOptionService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CakeOptionType? optionType)
    {
        var result = await _cakeOptionService.GetAllAsync(optionType);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCakeOptionDto dto)
    {
        var result = await _cakeOptionService.CreateAsync(dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCakeOptionDto dto)
    {
        var result = await _cakeOptionService.UpdateAsync(id, dto);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _cakeOptionService.DeleteAsync(id);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
