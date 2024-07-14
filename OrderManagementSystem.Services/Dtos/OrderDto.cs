using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Services.Dtos
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public orderPaymentStatus Status { get; set; } = orderPaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public IReadOnlyList<OrderItem> orderItems { get; set; } = new List<OrderItem>();

    }
}
