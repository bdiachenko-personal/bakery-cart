namespace Bakery.Application.DTOs;

public class CartDTO
{
    public List<CartItemDTO> Items { get; set; } = [];
    public DateTime DateCreated { get; set; }
}