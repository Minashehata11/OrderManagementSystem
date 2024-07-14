using OrderManagementSystem.Data.Entities;
using Talabat.Core.Entities.Basket;

namespace OrderManagementSystem.Services.PaymentServices
{
    public interface IPaymentService
    {
      
        Task<CustomerBasket> CreateOrUpdatePaymentforNewOrder(string BasketId);
        Task<Order> UpdatePaymentIntentToSucceedOrfailed(string paymentIntentId, bool flag);
     
    }
}
