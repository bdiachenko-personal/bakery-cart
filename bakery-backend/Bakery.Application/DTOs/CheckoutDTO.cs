namespace Bakery.Application.DTOs;

public class CheckoutDTO
{
    public decimal Sum { get; set; }
    public IEnumerable<SaleDTO> AppliedSales { get; set; }
}