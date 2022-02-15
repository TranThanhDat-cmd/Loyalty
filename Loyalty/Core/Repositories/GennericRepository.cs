using Loyalty.Core.IRepositories;
using Loyalty.Data;

namespace Loyalty.Core.Repositories
{
    public class GennericRepository<TEntity> : IGennericRepository<TEntity>
    {
        private MyDbContext _context;

        public GennericRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(TEntity entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Task<bool> Detele(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
