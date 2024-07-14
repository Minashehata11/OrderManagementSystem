using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string CustomerId, string basketId);
        Task<Order> GetOrderById(int orderId);
        Task<IReadOnlyList<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderStatus(int orderId,orderPaymentStatus paymentStatus);


    }
}
