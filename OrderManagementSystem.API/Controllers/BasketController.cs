using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.Repository.BasketRepositories;
using Talabat.Core.Entities.Basket;
using Talabate.PL.Dtos;

namespace OrderManagementSystem.API.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string basketId)
         => await _basketRepository.DeleteBasketAsync(basketId);
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string basketId)
        {
            var Basket = await _basketRepository.GetBasketAsync(basketId);
            return Basket is null ? new CustomerBasket(basketId) : Basket;

        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basket);
            var UpdatedBasked = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (UpdatedBasked is null) return BadRequest(new ErrorApiResponse(400));
            return Ok(UpdatedBasked);
        }
    }
}
