using Bakery.Application.Services;
using Bakery.Infrastructure.Data;
using Bakery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();

services.AddDbContext<BakeryDbContext>(options =>
    options.UseInMemoryDatabase("BakeryDB"));

services.AddScoped<IProductBestSaleFinder, ProductBestSaleFinder>();
services.AddScoped<IProductService, ProductService>();
services.AddScoped<IShoppingCartService, ShoppingCartService>();
services.AddScoped<IProductRepository, ProductRepository>();

services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

InitializeDatabase(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseCors();
app.UseHttpsRedirection();

app.Run();

void InitializeDatabase(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();
        context.Database.EnsureCreated();
    }
}

public partial class Program { }