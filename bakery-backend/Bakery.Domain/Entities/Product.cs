﻿namespace Bakery.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }

    public ICollection<Sale> Sales { get; set; } = [];
}