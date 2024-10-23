using Bakery.Application.DTOs;
using Bakery.Domain.Entities;

namespace Bakery.Application.Services;

public class ProductBestSaleFinder : IProductBestSaleFinder
{
    public PriceWithSaleDTO FindTheBestPriceForProduct(DateTime dateCreated, decimal price, int quantity, List<Sale> sales)
    {
        var availableSales = GetAvailableSales(dateCreated, quantity, sales);

        if (!availableSales.Any())
        {
            return new PriceWithSaleDTO
            {
                Price = CalculateItemBasePrice(price, quantity),
                Sale = null
            };
        }

        return availableSales
            .Select(sale => new PriceWithSaleDTO
            {
                Price = CalculateProductPriceForSale(price, quantity, sale),
                Sale = new SaleDTO
                {
                    Description = sale.Description, 
                    Id = sale.Id
                }
            })
            .OrderBy(item => item.Price)
            .First();
    }

    private List<Sale> GetAvailableSales(DateTime dateCreated, int quantity, List<Sale> sales)
    {
        return sales
            .Where(sale =>
                (sale.DayOfWeek == null || sale.DayOfWeek == dateCreated.DayOfWeek)
                && (sale.Day == null || sale.Day == dateCreated.Day)
                && (sale.Month == null || sale.Month == dateCreated.Month)
                && (sale.QuantityRequired == null || sale.QuantityRequired <= quantity)
                && IsSaleValid(sale))
            .ToList();
    }

    private decimal CalculateProductPriceForSale(decimal price, int quantity, Sale sale)
    {
        var fullPrice = CalculateItemBasePrice(price, quantity);

        if (sale == null)
        {
            return fullPrice;
        }

        if (!sale.QuantityRequired.HasValue && sale.PercentageDiscount.HasValue)
        {
            return CalculateItemDiscountPrice(price, quantity, sale.PercentageDiscount.Value);
        }

        return CalculateGroupedSalePrice(price, quantity, sale);
    }
    
    private decimal CalculateGroupedSalePrice(decimal price, int quantity, Sale sale)
    {
        var matchedGroups = quantity / sale.QuantityRequired.Value;
        var leftoverQuantity = quantity % sale.QuantityRequired.Value;

        var discountedGroupPrice = sale.SalePrice ??
                                   CalculateItemDiscountPrice(price, sale.QuantityRequired.Value, sale.PercentageDiscount.Value);

        return discountedGroupPrice * matchedGroups + CalculateItemBasePrice(price, leftoverQuantity);
    }

    private bool IsSaleValid(Sale sale)
    {
        if (sale.QuantityRequired.HasValue && (sale.SalePrice.HasValue || sale.PercentageDiscount.HasValue))
        {
            return true;
        }

        if (!sale.QuantityRequired.HasValue && sale.PercentageDiscount.HasValue)
        {
            return true;
        }

        return false;
    }

    private decimal CalculateItemDiscountPrice(decimal price, int quantity, decimal percentageDiscount) =>
        CalculateItemBasePrice(price, quantity) * CalculatePayableProductRate(percentageDiscount);

    private decimal CalculateItemBasePrice(decimal price, int quantity) => price * quantity;

    private decimal CalculatePayableProductRate(decimal percentageDiscount) => (100m - percentageDiscount) / 100;
}