using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.DTOs.Waste;
using PastryFlow.Application.Interfaces;
using System.Security.Claims;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WastesController : ControllerBase
{
    private readonly IWasteService _wasteService;

    public WastesController(IWasteService wasteService)
    {
        _wasteService = wasteService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWaste([FromForm] CreateWasteFormDto form)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out var userId)) return Unauthorized();

        string? photoPath = null;

        if (form.Photo != null && form.Photo.Length > 0)
        {
            var extension = Path.GetExtension(form.Photo.FileName);
            if (string.IsNullOrEmpty(extension)) extension = ".jpg";

            var dateFolder = form.Date.ToString("yyyy-MM-dd");
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "wastes", dateFolder);
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await form.Photo.CopyToAsync(stream);
            }

            // Store as relative URL accessible from /uploads/
            photoPath = $"/uploads/wastes/{dateFolder}/{fileName}";
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
