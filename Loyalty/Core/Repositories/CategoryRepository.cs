using Loyalty.Core.IRepositories;
using Loyalty.Data;
using Loyalty.Data.Entities;

namespace Loyalty.Core.Repositories
{
    public class CategoryRepository : GennericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(MyDbContext context) : base(context)
        {
        }
    }
}
