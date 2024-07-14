using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.Services.EmailService;
using OrderManagementSystem.Services.PaymentServices;
using Stripe;
using Talabate.PL.Dtos;

namespace OrderManagementSystem.API.Controllers
{

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        const string endpointSecret = "whsec_e380b303af894faca5623a3e0e71caeb5977fab05a2c81e11d5e36a4ce472bb7";
        public PaymentController(IPaymentService paymentService,IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            
        }
        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdaetPaymentIntend(string basketId)
        {
            var customerbasset = await _paymentService.CreateOrUpdatePaymentforNewOrder(basketId);
            if (customerbasset is null) return BadRequest(new ErrorApiResponse(400,"Cant Complete Payment?! Try Again"));
            var mapped = _mapper.Map<CustomerBasketDto>(customerbasset);
            return Ok(mapped);

        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                var paymentIntent=stripeEvent.Data.Object as PaymentIntent;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    await _paymentService.UpdatePaymentIntentToSucceedOrfailed(paymentIntent.Id, false);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    await _paymentService.UpdatePaymentIntentToSucceedOrfailed(paymentIntent.Id, true);

                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e) 
            {
                return BadRequest();
            }
        }
    }
    }

