namespace Bakery.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public int? Day { get; set; }
    public int? Month { get; set; }
    
    public int? QuantityRequired { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal? PercentageDiscount { get; set; }  
    
    public Product Product { get; set; }
}