using Loyalty.Core.IRepositories;
using Loyalty.Data;
using Loyalty.Data.Entities;

namespace Loyalty.Core.Repositories
{
    public class ProductRepository : GennericRepository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context)
        {
        }
    }
}
