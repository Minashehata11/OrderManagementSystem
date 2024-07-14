namespace OrderManagementSystem.Data.Entities
{
    public class Invoice:BaseEntity
    {
        public Invoice()
        {
            
        }

        public Invoice(Order order, decimal totalAmount)
        {
            Order = order;
            TotalAmount = totalAmount;
        }

        public Order Order { get; set; }
        public int? OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }=DateTime.Now; 
        public decimal TotalAmount { get; set; }
    }
}
