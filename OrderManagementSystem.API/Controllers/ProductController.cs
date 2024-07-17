using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Services.Dtos;
using OrderManagementSystem.Services.ProductService;

namespace OrderManagementSystem.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllOrder()
          => Ok(await _productService.GetAllProductAsync());
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(ErrorApiResponse), 404)]
        public async Task<ActionResult<Product>> GetById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return product == null ? BadRequest(new ErrorApiResponse(404, "NotFound")) : Ok(product);
        }
        [HttpPost]
       [Authorize(Roles ="Admin")]
        public async Task<ActionResult<bool>> CreateProduct(CreateOrUpdateProductDto dto)
        => Ok(await _productService.CreateProduct(dto));
        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ErrorApiResponse), 404)]

        public async Task<ActionResult> UpdaetProduct(int productId, CreateOrUpdateProductDto dto)
        {
            bool UpdatedSucessfully = await _productService.Update(productId, dto);
            return UpdatedSucessfully ? Ok():BadRequest(new ErrorApiResponse(404,"Not Found"));
          }
    }
}
