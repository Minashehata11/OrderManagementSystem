using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Services.CustomerService;
using OrderManagementSystem.Services.Dtos;

namespace OrderManagementSystem.API.Controllers
{
    
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService,IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<bool>> AddCustomer(ReqgisterOrLoginDto dto)
        {
            return await _customerService.CreateCustomer(dto) ? Ok("Added Succefully"): BadRequest("Not ADDed");
        }
        [HttpGet("{customerId}")]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAllOrder(string customerId)
        {
            var Orders=await _customerService.GetOrdersForCustomers(customerId);
            if (Orders is null)
                return BadRequest("Not Orders Created");
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderDto> >(Orders);
            return Ok(mappedOrder);
        }
    }
}
