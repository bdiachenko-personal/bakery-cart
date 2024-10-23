using Bakery.Application.Services;
using Bakery.Domain.Entities;
using FluentAssertions;

namespace Bakery.UnitTests;

public class ProductBestSaleFinderTests
{
    private readonly ProductBestSaleFinder _productBestSaleFinder;

    public ProductBestSaleFinderTests()
    {
        _productBestSaleFinder = new ProductBestSaleFinder();
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_NoSales_ReturnsBasePriceWithoutSale()
    {
        var dateCreated = DateTime.Now;
        var price = 10m;
        var quantity = 3;
        var sales = new List<Sale>();
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(dateCreated, price, quantity, sales);
        
        result.Price.Should().Be(price * quantity);
        result.Sale.Should().BeNull();
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_QuantityBasedSale_AppliesSale()
    {
        var dateCreated = DateTime.Now;
        var price = 10m;
        var quantity = 5;
        
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                QuantityRequired = 5,
                PercentageDiscount = 20m
            }
        };
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(dateCreated, price, quantity, sales);
        
        var expectedPrice = price * quantity * 0.8m;
        result.Price.Should().Be(expectedPrice);
        result.Sale.Should().NotBeNull();
        result.Sale.Id.Should().Be(sales.First().Id);
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_DayBasedSale_AppliesSaleOnCorrectDay()
    {
        var mondayDate = new DateTime(2024, 10, 21);
        var price = 10m;
        var quantity = 3;
        
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                PercentageDiscount = 15m
            }
        };
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(mondayDate, price, quantity, sales);
        
        var expectedPrice = price * quantity * 0.85m;
        result.Price.Should().Be(expectedPrice);
        result.Sale.Should().NotBeNull();
        result.Sale.Id.Should().Be(sales.First().Id);
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_MultipleSales_AppliesBestSale()
    {
        var dateCreated = DateTime.Now;
        var price = 10m;
        var quantity = 4;
        
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                QuantityRequired = 4,
                PercentageDiscount = 10m
            },
            new()
            {
                Id = Guid.NewGuid(),
                QuantityRequired = 4,
                PercentageDiscount = 20m
            }
        };
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(dateCreated, price, quantity, sales);
        
        var expectedPrice = price * quantity * (100 - 20) / 100;
        result.Price.Should().Be(expectedPrice);
        result.Sale.Should().NotBeNull();
        result.Sale.Id.Should().Be(sales[1].Id);
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_InvalidSale_IgnoresInvalidSale()
    {
        var dateCreated = DateTime.Now;
        var price = 10m;
        var quantity = 3;
        
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                QuantityRequired = 3
            }
        };
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(dateCreated, price, quantity, sales);
        
        result.Price.Should().Be(price * quantity);
        result.Sale.Should().BeNull();
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_GroupedSale_AppliesGroupedSalePrice()
    {
        var dateCreated = DateTime.Now;
        var price = 10m;
        var quantity = 7;
        
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                QuantityRequired = 3,
                SalePrice = 25m
            }
        };
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(dateCreated, price, quantity, sales);
        
        var expectedPrice = 25m * 2 + 10m;
        result.Price.Should().Be(expectedPrice);
        result.Sale.Should().NotBeNull();
        result.Sale.Id.Should().Be(sales.First().Id);
    }
    
    [Fact]
    public void FindTheBestPriceForProduct_InvalidSaleDate_IgnoresInvalidDaySale()
    {
        var tuesdayDate = new DateTime(2024, 10, 22);
        var price = 10m;
        var quantity = 5;
        
        var sales = new List<Sale>
        {
            new()
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                PercentageDiscount = 10m
            }
        };
        
        var result = _productBestSaleFinder.FindTheBestPriceForProduct(tuesdayDate, price, quantity, sales);

        result.Price.Should().Be(price * quantity);
        result.Sale.Should().BeNull();
    }
}
