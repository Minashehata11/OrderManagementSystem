using Microsoft.AspNetCore.Identity;
using OrderManagementSystem.Data.Entities.IdentityModels;

namespace OrderManagementSystem.Data.Data.SeedingAdmin
{
    public static class SeedAdmin
    {
        public async static Task CreateUser(UserManager<AppCustomer> userManager)
        {
            if (!userManager.Users.Any())
            {
                AppCustomer user = new AppCustomer()
                {
                    Email = "Mina@Gmail.com",
                    UserName = "Mayno",
                    PhoneNumber = "01225666903",
                   
                };
                await userManager.CreateAsync(user, "P@ssW0rd");
                await userManager.AddToRoleAsync(user,"Admin");
            }
        }
    }
}
