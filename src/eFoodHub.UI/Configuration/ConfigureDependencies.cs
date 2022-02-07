using eFoodHub.Services.Implementations;
using eFoodHub.Services.Interfaces;

namespace eFoodHub.UI.Configuration
{
    /// <summary>
    /// Configures Authentication service
    /// </summary>
    public static class ConfigureDependencies
    {
        public static void ConfigureDependenciesServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        }
    }
}
