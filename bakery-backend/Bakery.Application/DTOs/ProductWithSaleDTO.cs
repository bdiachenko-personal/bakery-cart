using Bakery.Domain.Entities;

namespace Bakery.Application.DTOs;

public class ProductWithSaleDTO
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Sale? Sale { get; set; }
}