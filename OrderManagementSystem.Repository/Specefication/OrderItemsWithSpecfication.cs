using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Repository.Specefication
{
    public class OrderItemsWithSpecification : SpeceficationBase<Order>
    {
        public OrderItemsWithSpecification(string? customerId=null) :
            base(order =>
                string.IsNullOrEmpty(customerId) ||order.CustomerId == customerId)
        {
            AddInclude(order => order.orderItems);
            AddInclude(order => order.Customer);
        }

        public OrderItemsWithSpecification(int id) :
           base(order =>  order.Id == id)
        {
            AddInclude(order => order.orderItems);
            AddInclude(order => order.Customer);
        }
    }
}
