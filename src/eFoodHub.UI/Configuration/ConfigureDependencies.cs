using eFoodHub.Services.Implementations;
using eFoodHub.Services.Interfaces;
using eFoodHub.UI.Helpers;
using eFoodHub.UI.Interfaces;

using ePizzaHub.Services.Implementations;

namespace eFoodHub.UI.Configuration
{
    /// <summary>
    /// Configures all dependent services
    /// </summary>
    public static class ConfigureDependencies
    {
        public static void ConfigureDependenciesServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IUserAccessor, UserAccessor>();

            services.AddTransient<ICatalogService, CatalogService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();

            services.AddTransient<IFileHelper, FileHelper>();
        }
    }
}