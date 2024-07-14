using Microsoft.Extensions.Configuration;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Repository.BasketRepositories;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Repository.Specefication;
using OrderManagementSystem.Services.EmailService;
using Stripe;
using Talabat.Core.Entities.Basket;
using Product = OrderManagementSystem.Data.Entities.Product;


namespace OrderManagementSystem.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IEmailService _emailService;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IBasketRepository basketRepository,IEmailService emailService )
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _emailService = emailService;
        }
       
        public async Task<CustomerBasket> CreateOrUpdatePaymentforNewOrder(string BasketId)
        {
            //sercrect Key
            StripeConfiguration.ApiKey = _configuration["Strip:Secretkey"];
            //Get basket
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket == null)
                return null;
            
            if (basket.BasketItems.Count > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                    var product = await _unitOfWork.Repository<Product>().GetById(item.ProductId);
                    if (item.UnitPrice != product.Price)
                        item.UnitPrice = product.Price;
                }
            }
            var subTotal = basket.BasketItems.Sum(items => (items.UnitPrice * items.Quantity)-items.Discound);
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId)) //create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(subTotal * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }

                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else //Updaate
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100),

                };
                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return basket;



        }

        public async Task<Data.Entities.Order> UpdatePaymentIntentToSucceedOrfailed(string paymentIntentId, bool flag)
        {
            var specs = new OrderWithPaymentIntentId(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetByIdAsyncWithSpecification(specs);
            var email = order.Customer.Email;
           
           
            if (flag)
            {
                order.Status=orderPaymentStatus.Recieve;
                EmailSetting emailSetting = new EmailSetting()
                {
                    Body = $"{orderPaymentStatus.Recieve.ToString()}",
                    TO = email,
                    Title = "Orders From OrderMangmentSystem"


                };

                _emailService.SendEmail(emailSetting);
            }
            else
            {

                order.Status=orderPaymentStatus.Failed;
                EmailSetting emailSetting = new EmailSetting()
                {
                    Body = $"{orderPaymentStatus.Failed.ToString()}",
                    TO = email,
                    Title = "Orders From OrderMangmentSystem"


                };

                _emailService.SendEmail(emailSetting);

            }
            _unitOfWork.Repository<Order>().UpdateEntity(order);
            await _unitOfWork.CompleteAsync();
            return order;
        }
    }
}
