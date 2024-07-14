namespace OrderManagementSystem.Data.Entities.Basket
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discound { get; set; } = 0M;


    }
}
