using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Services.Dtos;

namespace OrderManagementSystem.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(ReqgisterOrLoginDto registerDto);
        Task<IReadOnlyList<Order>> GetOrdersForCustomers(string CustomerId);
    }
}
