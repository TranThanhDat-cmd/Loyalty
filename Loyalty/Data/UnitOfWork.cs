using Loyalty.Core.IConffiguration;
using Loyalty.Core.IRepositories;


namespace Loyalty.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Categories { get; }

        public IProductRepository Products { get; }
        public IProductImageRepository ProductImages { get; }

        private readonly MyDbContext _context;

        public UnitOfWork(ICategoryRepository categories,
            IProductRepository products,
            MyDbContext context)
        {
            Categories = categories;
            Products = products;
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
