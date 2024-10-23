using Bakery.Application.DTOs;
namespace Bakery.Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
}