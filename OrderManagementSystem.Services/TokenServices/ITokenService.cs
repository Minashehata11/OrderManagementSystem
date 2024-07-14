using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Data.Entities.IdentityModels;

namespace OrderManagementSystem.Services.TokenServices
{
    public interface ITokenService
    {
        Task<string> GenerateToken( AppCustomer user, UserManager<AppCustomer> userManager);

    }
}
