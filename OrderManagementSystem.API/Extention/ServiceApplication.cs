using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.API.ErrorsHandle;
using OrderManagementSystem.API.Helper;
using OrderManagementSystem.Repository.BasketRepositories;
using OrderManagementSystem.Repository.Interfaces;
using OrderManagementSystem.Repository.Repositories;
using OrderManagementSystem.Services.CustomerService;
using OrderManagementSystem.Services.EmailService;
using OrderManagementSystem.Services.OrderServices;
using OrderManagementSystem.Services.PaymentServices;
using OrderManagementSystem.Services.ProductService;
using OrderManagementSystem.Services.TokenServices;
using System.Text;

namespace OrderManagementSystem.API.Extention
{
    public static class ServiceApplication
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {

            Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToArray();
                    var validtionErrorResponse = new ApiValidtionErrorRespones()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validtionErrorResponse);
                };
            }
            );
            Services.AddScoped<ITokenService,TokenServices>();
            Services.AddScoped<IUnitOfWork,UnitOfWork>();
            Services.AddScoped<ICustomerService,CustomerService>();
            Services.AddScoped<IBasketRepository,BasketRepository>();
            Services.AddScoped<IOrderServices, OrderServices>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<IEmailService, EmailService>();
            Services.AddAutoMapper(typeof(ProfileMapping));
            Services.AddAuthentication(Option =>
            {
                Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Option =>
            {
                Option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWt:Key"]))
                };

            });
            Services.AddAuthorization();
            return Services;
        }
    }
}