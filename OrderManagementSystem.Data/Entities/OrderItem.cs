namespace OrderManagementSystem.Data.Entities
{
    public class OrderItem:BaseEntity
    {
        public OrderItem()
        {
            
        }

        public OrderItem( int productId, int quantity, decimal unitPrice,decimal discound)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discound = discound;    
        }

        public int?  OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discound { get; set; } = 0.0M;

    }
}