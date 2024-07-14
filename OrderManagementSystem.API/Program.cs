
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.API.Extention;
using OrderManagementSystem.Data.Data;
using OrderManagementSystem.Data.Data.SeedingAdmin;
using OrderManagementSystem.Data.Entities.IdentityModels;
using StackExchange.Redis;
using Udemy.pl.Extention;
using Udemy.Repository.Data.SeedingData;

namespace OrderManagementSystem.API
{
    public class Program
    {
        public  static async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<OrderDbContext>(option =>
            {
             option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddIdentity<AppCustomer, IdentityRole>()
                             .AddEntityFrameworkStores<OrderDbContext>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(conf =>
            {
                var configration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(configration);
            });
            builder.Services.AddAplicationServices(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerDocumentation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region SeedData
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var userManger = services.GetRequiredService<UserManager<AppCustomer>>();
                var roleManger = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedRoles.AddRoles(roleManger);
                await SeedAdmin.CreateUser(userManger);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred IN program in Seeding DATA");
            }
            #endregion
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
