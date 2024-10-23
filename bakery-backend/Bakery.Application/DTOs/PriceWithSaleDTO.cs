namespace Bakery.Application.DTOs;

public class PriceWithSaleDTO
{
    public decimal Price { get; set; }

    public SaleDTO Sale { get; set; }
}