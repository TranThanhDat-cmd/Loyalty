using Loyalty.Core.IRepositories;
using Loyalty.Data;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Repositories
{
    public class GennericRepository<T> : IGennericRepository<T> where T : class
    {
        private MyDbContext _context;

        public GennericRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
