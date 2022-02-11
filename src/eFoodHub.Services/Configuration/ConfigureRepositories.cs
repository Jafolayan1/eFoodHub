using eFoodHub.Entities;
using eFoodHub.Repositories;
using eFoodHub.Repositories.Implementations;
using eFoodHub.Repositories.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eFoodHub.Services.Configuration
{
    /// <summary>
    /// Configures Datbase connection service (For IConfiguration...builder.Configuration is passed in program class.
    /// Configures Identity service for Users and Roles
    /// Configures DbContext
    /// </summary>
    public static class ConfigureRepositories
    {
        public static void ConfigureRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //options.SignIn.RequireConfirmedEmail = false;
                //options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddTransient<IRepository<Item>, Repository<Item>>();
            services.AddTransient<IRepository<Category>, Repository<Category>>();
            services.AddTransient<IRepository<ItemType>, Repository<ItemType>>();
            services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();
            services.AddTransient<ICartRepository, CartRepository>();
        }
    }
}