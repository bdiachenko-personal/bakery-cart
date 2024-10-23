using Bakery.Application.DTOs;
using Bakery.Infrastructure.Repositories;

namespace Bakery.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetProductsAsync();
        return products.Select(product => new ProductDto
        {
            Id = product.Id,
            Image = product.Image,
            Price = product.Price,
            Name = product.Name
        });
    }
}