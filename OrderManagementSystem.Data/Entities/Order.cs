using OrderManagementSystem.Data.Entities.IdentityModels;
using System.Runtime.Serialization;

namespace OrderManagementSystem.Data.Entities
{
    public enum orderPaymentStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "Receive")]

        Recieve,
        [EnumMember(Value = "Failed")]

        Failed
    }
    public enum PaymentMethod
    {
        [EnumMember(Value = "card")]

        CreditCard,
        [EnumMember(Value = "Google Pay")]

        GooglePay
    }
    public class Order:BaseEntity
    {
        public Order( string? paymentIntentId, decimal totalAmount, string? customerId)
        {
            PaymentIntentId = paymentIntentId;
            TotalAmount = totalAmount;
            CustomerId = customerId;
        }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public orderPaymentStatus Status { get; set; }= orderPaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; }=PaymentMethod.CreditCard;
        public string? PaymentIntentId { get; set; }
        public decimal TotalAmount { get; set; }
        public IReadOnlyList<OrderItem> orderItems { get; set; } = new List<OrderItem>();   
        public AppCustomer Customer { get; set; }
        public string? CustomerId { get; set; }



    }
}
