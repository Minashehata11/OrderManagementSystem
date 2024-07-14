using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Data.Entities.IdentityModels;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Repository.Specefication;
using OrderManagementSystem.Services.Dtos;

namespace OrderManagementSystem.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<AppCustomer> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(UserManager<AppCustomer> userManager,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
      //  [Authorize(Roles ="Admin")]
        public async Task<bool> CreateCustomer(ReqgisterOrLoginDto registerDto)
        {
            AppCustomer customer = new AppCustomer()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0]

            };
          var result= await _userManager.CreateAsync(customer,registerDto.PassWord);
            await _userManager.AddToRoleAsync(customer,"Customer");
            if(!result.Succeeded) 
                return  false;
            return true;
        }

        public async Task< IReadOnlyList<Order>> GetOrdersForCustomers(string CustomerId)
        {
            var spec= new OrderItemsWithSpecification(CustomerId);
            var OrdersWithSpec = await _unitOfWork.Repository<Order>().GetAllAsyncWithSpecification(spec);
            if (!OrdersWithSpec.Any())
                return null;
            return OrdersWithSpec; ;


        }
    }
}
