using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Services.Dtos
{
    public class OrderToResturnDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } 
        public orderPaymentStatus Status { get; set; } 
        public PaymentMethod PaymentMethod { get; set; } 
        public decimal TotalAmount { get; set; }
        public IReadOnlyList<OrderItemToReturnDto> orderItems { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerId { get; set; }
    }
}
