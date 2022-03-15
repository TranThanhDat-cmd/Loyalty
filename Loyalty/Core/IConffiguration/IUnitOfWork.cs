using Loyalty.Core.IRepositories;

namespace Loyalty.Core.IConffiguration
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        Task CompleteAsync();
    }
}
