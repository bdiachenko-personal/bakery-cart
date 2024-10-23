using Bakery.Application.DTOs;
using Bakery.Domain.Entities;

namespace Bakery.Application.Services;

public interface IProductBestSaleFinder
{
    PriceWithSaleDTO FindTheBestPriceForProduct(DateTime dateCreated, decimal price, int quantity, List<Sale> sales);
}