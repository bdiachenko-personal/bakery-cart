using Bakery.Domain.Entities;

namespace Bakery.Infrastructure.Data;

public static class SeedData
{
    public static List<Product> Products = new()
    {
        new()
        {
            Id = new Guid("FB1BDC5E-3955-4159-B4B2-8DA7EB6CEA34"),
            Name = "Cookie",
            Image = "https://img.freepik.com/free-photo/delicious-cookies-arrangement_23-2150707201.jpg",
            Price = 1.25m,
        },
        new()
        {
            Id = new Guid("4AC7ED67-ED11-4942-8ADC-01C414171557"),
            Name = "Brownie",
            Image = "https://img.freepik.com/free-photo/high-angle-cooling-rack-with-brownies-kettle_23-2148569710.jpg?ga=GA1.1.188753712.1729520476&semt=ais_hybrid",
            Price = 2,
        },
        new()
        {
            Id = new Guid("5D7B62BA-F46E-4F98-B32E-5655F16A9639"),
            Name = "Key Lime Cheesecake",
            Image = "https://img.freepik.com/free-photo/raw-vegan-cake-with-lemon-lime-black-surface-covered-with-tiny-daisy-flowers_181624-24502.jpg?size=626&ext=jpg",
            Price = 8m,
        },
        new()
        {
            Id = new Guid("56DA23E6-CB38-4653-9543-ED203B7E251D"),
            Name = "Mini Gingerbread Donut",
            Image = "https://img.freepik.com/free-photo/two-decorated-donuts-arranged-red-background_23-2148123889.jpg?ga=GA1.1.188753712.1729520476&semt=ais_hybrid",
            Price = 0.5m,
        }
    };
    
    public static List<Sale> Sales = new()
    {
        new()
        {
            Id = Guid.Parse("A6D1D899-925E-4B61-AB7C-B1B9A7BB2D79"),
            ProductId = new Guid("FB1BDC5E-3955-4159-B4B2-8DA7EB6CEA34"),
            Description = "Every Friday: 8 cookies for $6.00",
            DayOfWeek = DayOfWeek.Friday,
            QuantityRequired = 8,
            PercentageDiscount = 40m,
            IsActive = true
        },
        new()
        {
            Id = new Guid("D95794BD-133F-4A42-A150-9B0B965446B3"),
            ProductId = new Guid("FB1BDC5E-3955-4159-B4B2-8DA7EB6CEA34"),
            Description = "6 cookies for $6.00",
            QuantityRequired = 6,
            PercentageDiscount = 20m,
            IsActive = true
        },
        new()
        {
            Id = Guid.Parse("B0B26A2C-0B3B-43A7-BB4A-CFCE8EB30EED"),
            ProductId = new Guid("5D7B62BA-F46E-4F98-B32E-5655F16A9639"),
            Description = "October 1st: 25% off on Key Lime Cheesecake",
            Day = 1,
            Month = 10,
            PercentageDiscount = 25m,
            IsActive = true
        },
        new()
        {
            Id = Guid.Parse("E3E19C6A-301E-4D84-A85D-9E25D44D78B8"),
            ProductId = new Guid("56DA23E6-CB38-4653-9543-ED203B7E251D"),
            Description = "Every Tuesday: Buy 1 Get 1 Free on Mini Gingerbread Donuts",
            DayOfWeek = DayOfWeek.Tuesday,
            QuantityRequired = 2,
            PercentageDiscount = 50m,
            IsActive = true
        },
        new()
        {
            Id = new Guid("1E92A252-EAD9-4E81-B3A7-1E181C879E5D"),
            ProductId = new Guid("4AC7ED67-ED11-4942-8ADC-01C414171557"),
            Description = "4 Brownies for $7.00",
            QuantityRequired = 4,
            SalePrice = 7m,
            IsActive = true
        }
    };
}