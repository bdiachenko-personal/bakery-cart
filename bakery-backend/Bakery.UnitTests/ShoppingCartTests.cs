using Bakery.Application.Common.Constants;
using Bakery.Application.DTOs;
using Bakery.Application.Services;
using Bakery.Infrastructure.Repositories;
using Moq;
using System.Linq.Expressions;
using Bakery.Domain.Entities;
using FluentAssertions;

namespace Bakery.UnitTests;

public class ShoppingCartServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IProductBestSaleFinder> _productBestSaleFinderMock;
    private readonly ShoppingCartService _service;

    public ShoppingCartServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productBestSaleFinderMock = new Mock<IProductBestSaleFinder>();
        _service = new ShoppingCartService(_productRepositoryMock.Object, _productBestSaleFinderMock.Object);
    }

    [Fact]
    public async Task Checkout_CartIsEmpty_ReturnsFailure()
    {
        var emptyCart = new CartDTO { Items = new List<CartItemDTO>() };

        var result = await _service.CheckoutAsync(emptyCart);

        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(ErrorMessages.EmptyCart);
    }

    [Fact]
    public async Task Checkout_ProductsNotFound_ReturnsFailure()
    {
        var cartDto = new CartDTO
        {
            Items = new List<CartItemDTO>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 2 }
            }
        };

        _productRepositoryMock
            .Setup(repo => repo.GetProductsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(new List<Product>());

        var result = await _service.CheckoutAsync(cartDto);

        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(ErrorMessages.UnrecognizedProduct);
    }

    [Fact]
    public async Task Checkout_ValidCartWithNoSales_ReturnsTotalWithoutSales()
    {
        var cartDto = new CartDTO
        {
            Items = [new CartItemDTO { ProductId = Guid.NewGuid(), Quantity = 2 }]
        };

        var products = new List<Product>
        {
            new() { Id = cartDto.Items.First().ProductId, Price = 10, Sales = new List<Sale>() }
        };

        _productRepositoryMock
            .Setup(repo => repo.GetProductsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(products);

        _productBestSaleFinderMock
            .Setup(finder => finder.FindTheBestPriceForProduct(It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<List<Sale>>()))
            .Returns(new PriceWithSaleDTO { Price = 20 });

        var result = await _service.CheckoutAsync(cartDto);

        result.IsSuccess.Should().BeTrue();
        result.Value.Sum.Should().Be(20);
        result.Value.AppliedSales.Should().BeEmpty();
    }

    [Fact]
    public async Task Checkout_ValidCartWithSales_ReturnsTotalWithSalesApplied()
    {
        var cartDto = new CartDTO
        {
            Items = [new CartItemDTO { ProductId = Guid.NewGuid(), Quantity = 2 }]
        };

        var sale = new Sale { Id = Guid.NewGuid(), Description = "Test" };
        var saleDto = new SaleDTO { Description = sale.Description, Id = sale.Id };

        var products = new List<Product>
        {
            new() { Id = cartDto.Items.First().ProductId, Price = 10, Sales = new List<Sale> { sale } }
        };

        _productRepositoryMock
            .Setup(repo => repo.GetProductsAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(products);

        _productBestSaleFinderMock
            .Setup(finder => finder.FindTheBestPriceForProduct(It.IsAny<DateTime>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<List<Sale>>()))
            .Returns(new PriceWithSaleDTO { Price = 15, Sale = saleDto });

        var result = await _service.CheckoutAsync(cartDto);

        result.IsSuccess.Should().BeTrue();
        result.Value.Sum.Should().Be(15);
        result.Value.AppliedSales.Should().HaveCount(1);
    }
}
