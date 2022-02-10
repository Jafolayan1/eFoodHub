using eFoodHub.Services.Implementations;
using eFoodHub.Services.Interfaces;
using eFoodHub.UI.Helpers;
using eFoodHub.UI.Interfaces;

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
        }
    }
}