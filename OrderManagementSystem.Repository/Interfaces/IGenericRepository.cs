using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.Specefication;

namespace OrderManagementSystem.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetById(int id);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        Task<T> GetByIdAsyncWithSpecification(ISpecefication<T> specs);
        Task<IReadOnlyList<T>> GetAllAsyncWithSpecification(ISpecefication<T> specs);

    }
}
