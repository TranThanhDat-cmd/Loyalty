using System.Collections;

namespace Loyalty.Core.IRepositories

{
    public interface IGennericRepository<TEntity, TKey>
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(TKey id);
        Task<bool> Add(TEntity entity);
        Task<bool> Detele(TKey id);
        Task<bool> Update(TEntity entity);
    }
}
