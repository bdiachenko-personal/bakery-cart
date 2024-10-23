using Bakery.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var result = await productService.GetAllProductsAsync();
        return Ok(result);
    }
}