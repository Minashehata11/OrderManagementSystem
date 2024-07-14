using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Services.ProductService
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> CreateProduct(CreateOrUpdateProductDto dto);
        Task<bool> Update(int productId, CreateOrUpdateProductDto dto);
    }
}
