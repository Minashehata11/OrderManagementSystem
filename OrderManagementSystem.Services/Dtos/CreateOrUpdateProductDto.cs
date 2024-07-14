using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Services.Dtos
{
    public class CreateOrUpdateProductDto
    {
        public string Name { get; set; }
        [Range(0.1, 400, ErrorMessage = "Price must be between {0.1} and {400}")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
