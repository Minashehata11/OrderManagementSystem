using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Data;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Repository.Specefication;

namespace OrderManagementSystem.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderDbContext _orderDbContext;

        public GenericRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public void AddEntity(T entity)
        => _orderDbContext.Set<T>().Add(entity);

        public void DeleteEntity(T entity)
        => _orderDbContext.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await _orderDbContext.Set<T>().ToListAsync();

        public async Task<T> GetById(int id)
         => await _orderDbContext.Set<T>().FindAsync(id);

        public void UpdateEntity(T entity)
         => _orderDbContext.Set<T>().Update(entity);
        public async Task<IReadOnlyList<T>> GetAllAsyncWithSpecification(ISpecefication<T> specs)
        =>
            await ApplySpecification(specs).ToListAsync();


        
        public async Task<T> GetByIdAsyncWithSpecification(ISpecefication<T> specs)
            =>
         await ApplySpecification(specs).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecefication<T> spec)
            => SpecefiationEvaluator<T>.GetQuery(_orderDbContext.Set<T>(), spec);
    }
}
