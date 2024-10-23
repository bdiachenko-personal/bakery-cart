using System.Linq.Expressions;
using Bakery.Domain.Entities;
using Bakery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Repositories;

public class ProductRepository(BakeryDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null)
    {
        var query = context.Products
            .Include(product => product.Sales)
            .AsQueryable();
            
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        return await query.ToListAsync();
    }
}