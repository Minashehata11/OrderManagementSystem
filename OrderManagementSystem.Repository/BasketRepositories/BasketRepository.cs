using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Entities.Basket;
using IDatabase = StackExchange.Redis.IDatabase;

namespace OrderManagementSystem.Repository.BasketRepositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer radis)
        {
            _database = radis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
      => await _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var IsCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(7));
            if (!IsCreated)
                return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
