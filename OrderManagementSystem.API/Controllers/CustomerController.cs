using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
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
        [ProducesResponseType(typeof(ErrorApiResponse), 400)]
        public async Task<ActionResult<bool>> AddCustomer(ReqgisterOrLoginDto dto)
        {
            return await _customerService.CreateCustomer(dto) ? Ok("Added Succefully"): BadRequest(new ErrorApiResponse(400));
        }
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(ErrorApiResponse), 404)]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAllOrder(string customerId)
        {
            var Orders=await _customerService.GetOrdersForCustomers(customerId);
            if (Orders is null)
                return BadRequest(new ErrorApiResponse(404));
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderDto> >(Orders);
            return Ok(mappedOrder);
        }
    }
}
