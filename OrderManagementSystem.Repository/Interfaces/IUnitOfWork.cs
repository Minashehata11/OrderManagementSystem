using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Repository.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();
        public void Add<T>(T entity) where T : class;

    }
}
