using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Services.Dtos;

namespace OrderManagementSystem.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateProduct(CreateOrUpdateProductDto dto)
        {
            var product = new Product()
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
            };
            _unitOfWork.Repository<Product>().AddEntity(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IReadOnlyList<Product>> GetAllProductAsync()
          =>await _unitOfWork.Repository<Product>().GetAllAsync();

        public async Task<Product> GetProductByIdAsync(int id)
         => await _unitOfWork.Repository<Product>().GetById(id);

        public async Task<bool> Update(int productId, CreateOrUpdateProductDto dto)
        {
            var product = await GetProductByIdAsync(productId);
            if (product == null)
                return false;
            product.Price = dto.Price;
            product.Stock = dto.Stock; 
            product.Name = dto.Name;
            _unitOfWork.Repository<Product>().UpdateEntity(product);
           await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
