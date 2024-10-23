using Bakery.Application.Common.Constants;
using Bakery.Application.Common.Models;
using Bakery.Application.DTOs;
using Bakery.Infrastructure.Repositories;

namespace Bakery.Application.Services;

public class ShoppingCartService(IProductRepository productRepository, IProductBestSaleFinder productBestSaleFinder) : IShoppingCartService
{
    public async Task<ServiceResult<CheckoutDTO>> CheckoutAsync(CartDTO cartDto)
    {
        if (!cartDto.Items.Any())
        {
            return ServiceResult<CheckoutDTO>.Failure(ErrorMessages.EmptyCart);
        }

        var quantityPerProduct = cartDto.Items
            .Where(item => item.Quantity > 0)
            .ToDictionary(item => item.ProductId, item => item.Quantity);

        var products = await productRepository
            .GetProductsAsync(product => quantityPerProduct.Keys.Contains(product.Id));

        if (quantityPerProduct.Count != products.Count)
        {
            return ServiceResult<CheckoutDTO>.Failure(ErrorMessages.UnrecognizedProduct);
        }

        var productPricesWithSales = products.Select(product => productBestSaleFinder.FindTheBestPriceForProduct(
                cartDto.DateCreated,
                product.Price,
                quantityPerProduct[product.Id],
                product.Sales.ToList()
            )
        );

        var totalSum = productPricesWithSales.Sum(item => item.Price);
        
        var appliedSales = productPricesWithSales
            .Where(item => item.Sale != null)
            .Select(item => item.Sale);
        
        var checkoutDto = new CheckoutDTO
        {
            Sum = totalSum,
            AppliedSales = appliedSales
        };

        return ServiceResult<CheckoutDTO>.Success(checkoutDto);
    }
}