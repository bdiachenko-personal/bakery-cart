using Bakery.Application.DTOs;
using Bakery.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase
{
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CartDTO cartDto)
    {
        var result = await shoppingCartService.CheckoutAsync(cartDto);

        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Message);
    }
}