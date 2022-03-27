
using Loyalty.Core.IRepositories;
using Loyalty.Data;
using Loyalty.Data.Entities;

namespace Loyalty.Core.Repositories
{
    public class ProductImageRepository : GennericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(MyDbContext context) : base(context)
        {
        }
    }
}
