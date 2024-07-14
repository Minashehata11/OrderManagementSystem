using OrderManagementSystem.Data.Data;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.Interfaces;
using System.Collections;

namespace OrderManagementSystem.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _respositories;
        private readonly OrderDbContext _orderDbContext;

        public UnitOfWork(OrderDbContext orderDbContext)
        {
            _respositories = new Hashtable();
            _orderDbContext = orderDbContext;
        }

        public async Task<int> CompleteAsync()
        => await _orderDbContext.SaveChangesAsync();

        public void Add<T>(T entity) where T : class
        {
            _orderDbContext.Add(entity);
        }
        public async ValueTask DisposeAsync()
        => await _orderDbContext.DisposeAsync();

        public IGenericRepository<Tentity> Repository<Tentity>() where Tentity : BaseEntity
        {

            var type = typeof(Tentity).Name; //product //address
            if (!_respositories.ContainsKey(type))
            {
                var Repository = new GenericRepository<Tentity>(_orderDbContext);
                _respositories.Add(type, Repository);
            }
            return (IGenericRepository<Tentity>)_respositories[type];
        }


    }
}
