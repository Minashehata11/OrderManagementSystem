using OrderManagementSystem.Data.Entities;

namespace OrderManagementSystem.Repository.Specefication
{
    public class InvoiceWithSpecefication : SpeceficationBase<Invoice>
    {
        public InvoiceWithSpecefication(int? orderId ) :
            base(inv =>
                !orderId.HasValue || inv.OrderId == orderId)
        {
            AddInclude(order => order.Order);
        }

        public InvoiceWithSpecefication(int id) :
           base(inv => inv.Id == id)
        {
            AddInclude(order => order.Order);
        }
    }
}
