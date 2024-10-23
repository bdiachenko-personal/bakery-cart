using Bakery.Application.Common.Models;
using Bakery.Application.DTOs;

namespace Bakery.Application.Services;

public interface IShoppingCartService
{
    Task<ServiceResult<CheckoutDTO>> CheckoutAsync(CartDTO cartDto);
}