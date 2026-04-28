using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.Waste;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;
using PastryFlow.Application.Common;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WastesController : ControllerBase
{
    private readonly IWasteService _wasteService;
    private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

    public WastesController(IWasteService wasteService, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
    {
        _wasteService = wasteService;
        _env = env;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWaste([FromForm] CreateWasteFormDto form)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        string? photoPath = null;

        if (form.Photo != null && form.Photo.Length > 0)
        {
            var contentRootPath = _env.ContentRootPath;
            photoPath = await FileUploadHelper.SaveFileAsync(form.Photo, "wastes", contentRootPath);
        }

        var dto = new CreateWasteDto
        {
            BranchId = form.BranchId,
            ProductId = form.ProductId,
            Quantity = form.Quantity,
            Notes = form.Notes,
            Date = form.Date,
            PhotoPath = photoPath
        };

        var result = await _wasteService.CreateWasteAsync(dto, userId);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetWastes([FromQuery] Guid branchId, [FromQuery] string date)
    {
        if (!DateOnly.TryParse(date, out var parsedDate))
            return BadRequest("Geçersiz tarih formatı.");

        var result = await _wasteService.GetWastesAsync(branchId, parsedDate);
        return Ok(result);
    }
}

public class CreateWasteFormDto
{
    public Guid BranchId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public string? Notes { get; set; }
    public DateOnly Date { get; set; }
    public IFormFile? Photo { get; set; }
}
