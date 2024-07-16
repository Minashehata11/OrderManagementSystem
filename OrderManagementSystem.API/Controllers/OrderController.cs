using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Data.Entities.IdentityModels;
using OrderManagementSystem.Services.Dtos;
using OrderManagementSystem.Services.OrderServices;
using System.Security.Claims;
namespace OrderManagementSystem.API.Controllers
{

    public class OrderController : BaseController
    {
        private readonly IOrderServices _orderServices;
        private readonly UserManager<AppCustomer> _userManager;
        private readonly IMapper _mapper;

        public OrderController(IOrderServices orderServices, UserManager<AppCustomer> userManager, IMapper mapper)
        {
            _orderServices = orderServices;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(string BasketId)
        {
            var Email = User.FindFirstValue(claimType: ClaimTypes.Email);
            var customer = await _userManager.FindByEmailAsync(Email);
            var orders = await _orderServices.CreateOrderAsync(customer.Id, BasketId);
           
            return orders is null ? BadRequest(new ErrorApiResponse(400, "Quantity Over Stock Or Product Not Found")) : Ok(orders);
        }
        [HttpGet]
       [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IReadOnlyList<OrderToResturnDto>>> GetAllOrders()
        {
            var orders = await _orderServices.GetAllOrdersAsync();
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderToResturnDto>>(orders);
            return Ok(mappedOrder);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderToResturnDto>> GetOrder(int orderId)
        {
            var order = await _orderServices.GetOrderById(orderId);
            if (order == null)
                return BadRequest(new ErrorApiResponse(404, "Not Found"));
            var mappedOrder = _mapper.Map<OrderToResturnDto>(order);
            return Ok(mappedOrder);
        }

        [HttpPut("{orderId}")]
       [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateOrderStatus(int orderId, orderPaymentStatus orderPaymentStatus)
        {
            var UpdateSuccess=await _orderServices.UpdateOrderStatus(orderId, orderPaymentStatus);
            return UpdateSuccess ? Ok(UpdateSuccess) : BadRequest(
                                                      new ErrorApiResponse(400, "Update failed. Reason: Insufficient permissions"));
        }
    
    }
}
