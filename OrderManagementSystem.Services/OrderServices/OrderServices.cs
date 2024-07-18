using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.BasketRepositories;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Repository.Specefication;
using OrderManagementSystem.Services.EmailService;
using OrderManagementSystem.Services.PaymentServices;

namespace OrderManagementSystem.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketRepository _basket;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;

        public OrderServices( IBasketRepository basket,IUnitOfWork unitOfWork,IPaymentService paymentService,   IEmailService emailService
            

            )
        {
            _basket = basket;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            _emailService = emailService;
        }
        public async Task<Order> CreateOrderAsync(string CustomerId, string basketId)
        {
            var basket = await _basket.GetBasketAsync(basketId);
            if (basket is null)
                return null;
            var orderItems = new List<OrderItem>();
            var orderitem= new OrderItem();
            var product= new Product();
            if (basket.BasketItems.Count > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                     product = await _unitOfWork.Repository<Product>().GetById(item.ProductId);
                    if (product == null) return null;
                    if (item.Quantity < product.Stock)
                    {
                        var discound = 0.0m;
                        var TotalPrice=item.UnitPrice*item.Quantity;
                        if (TotalPrice > 200)
                            discound = (10m / 100) * TotalPrice;
                        else if (TotalPrice > 100)
                            discound = (5m / 100) * TotalPrice;
                        orderitem = new OrderItem(product.Id, item.Quantity, item.UnitPrice,discound);
                        item.Discound = discound;
                        //Update Stock

                        product.Stock=product.Stock-item.Quantity;

                    }
                    else
                        return null;
                    orderItems.Add(orderitem);

                }
              _unitOfWork.Repository<Product>().UpdateEntity(product);
             await _basket.UpdateBasketAsync(basket);
            }
            else
                return null;
           
            var TotalAfterDiscount = orderItems.Sum(items => (items.UnitPrice * items.Quantity)-items.Discound);

            #region For More Security
            var spec = new OrderWithPaymentIntentId(basket.PaymentIntentId);
            var exOrder = await _unitOfWork.Repository<Order>().GetByIdAsyncWithSpecification(spec);
            if (exOrder != null)
            {
                _unitOfWork.Repository<Order>().DeleteEntity(exOrder);
                await _paymentService.CreateOrUpdatePaymentforNewOrder(basketId);
            } 
            #endregion


            // create Order 
            var order = new Order(basket.PaymentIntentId, TotalAfterDiscount, CustomerId);
              order.orderItems = orderItems;
            // create Invoice 
            //Add Order
            try{
            _unitOfWork.Repository<Order>().AddEntity(order);
            Invoice invoice = new Invoice(order, order.TotalAmount);
            _unitOfWork.Repository<Invoice>().AddEntity(invoice);

            }
            catch {
                return null;
            }

            //Save
            await _unitOfWork.CompleteAsync();
            return order;

        }

        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync()
        {
            var specs = new OrderItemsWithSpecification();
            var order= await _unitOfWork.Repository<Order>().GetAllAsyncWithSpecification(specs);
            return order;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var specs= new OrderItemsWithSpecification(orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdAsyncWithSpecification(specs);
            return (order);
        }

        public async Task<bool> UpdateOrderStatus(int orderId, orderPaymentStatus orderPaymentStatus)
        {
            var spec = new OrderItemsWithSpecification(orderId);
            var order = await _unitOfWork.Repository<Order>().GetByIdAsyncWithSpecification(spec);
            if(order == null) return false;
            var email = order.Customer.Email;
            order.Status=orderPaymentStatus;
            EmailSetting emailSetting = new EmailSetting()
            {
                Body = $"{orderPaymentStatus.ToString()}",
                TO = email,
                Title="Orders From OrderMangmentSystem"


            };
             _emailService.SendEmail(emailSetting);
            _unitOfWork.Repository<Order>().UpdateEntity(order);
              await  _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
