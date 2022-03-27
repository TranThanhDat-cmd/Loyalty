using Loyalty.Core.IRepositories;
using Loyalty.Data;
using Loyalty.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Repositories
{
    public class ProductRepository : GennericRepository<Product>, IProductRepository
    {
        private readonly MyDbContext _context;
        public ProductRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            var category = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Name == categoryName);
            return category.Products;

        }
    }
}
