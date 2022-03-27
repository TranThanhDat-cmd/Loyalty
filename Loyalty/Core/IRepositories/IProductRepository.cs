using Loyalty.Data.Entities;

namespace Loyalty.Core.IRepositories
{
    public interface IProductRepository : IGennericRepository<Product>
    {
        IEnumerable<Product> GetProductsByCategory(string categoryName);
    }
}
