using System.Collections;

namespace Loyalty.Core.IRepositories

{
    public interface IGennericRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        Task<bool> Add(TEntity entity);
        Task<bool> Detele(object id);
        Task<bool> Update(TEntity entity);
    }
}
