using System.ComponentModel.DataAnnotations;

namespace Talabate.PL.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        [Range(0, int.MaxValue)]

        public int Quantity { get; set; }
        


    }
}