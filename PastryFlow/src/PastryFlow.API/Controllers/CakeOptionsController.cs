using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PastryFlow.Application.Interfaces;
using PastryFlow.Domain.Enums;

namespace PastryFlow.API.Controllers;

[Authorize]
[ApiController]
[Route("api/cake-options")]
public class CakeOptionsController : ControllerBase
{
    private readonly ICakeOptionService _cakeOptionService;

    public CakeOptionsController(ICakeOptionService cakeOptionService)
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
}
