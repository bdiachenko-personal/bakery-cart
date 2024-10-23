using System.Linq.Expressions;
using Bakery.Domain.Entities;

namespace Bakery.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync(Expression<Func<Product, bool>> filter = null);
}