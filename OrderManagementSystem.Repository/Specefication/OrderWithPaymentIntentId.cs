using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Repository.Specefication
{
    public class OrderWithPaymentIntentId : SpeceficationBase<Order>
    {
        public OrderWithPaymentIntentId(string intentId) :
             base(order => order.PaymentIntentId == intentId)
        {
            Includes.Add(o => o.Customer);
        }

    }
}
