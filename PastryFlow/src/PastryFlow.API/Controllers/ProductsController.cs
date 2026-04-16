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
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] Guid? branchId, [FromQuery] Guid? categoryId, [FromQuery] string? productType)
    {
        ProductType? pType = null;
        if (Enum.TryParse<ProductType>(productType, out var parsedType))
            pType = parsedType;

        var result = await _productService.GetProductsAsync(branchId, categoryId, pType);
        return Ok(result);
    }

    [HttpGet("by-category")]
    public async Task<IActionResult> GetCategoriesWithProducts([FromQuery] Guid? branchId, [FromQuery] string? productType)
    {
        ProductType? pType = null;
        if (Enum.TryParse<ProductType>(productType, out var parsedType))
            pType = parsedType;

        var result = await _productService.GetCategoriesWithProductsAsync(branchId, pType);
        return Ok(result); // According to spec: Response: CategoryWithProductsDto[]
    }
}
