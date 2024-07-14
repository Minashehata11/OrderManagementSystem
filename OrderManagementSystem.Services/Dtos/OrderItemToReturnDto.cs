namespace OrderManagementSystem.Services.Dtos
{
    public class OrderItemToReturnDto
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discound { get; set; } 
    }
}