using Microsoft.Extensions.DependencyInjection;
using EMT.BLL.Services;

namespace EMT.BLL
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterBLLServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyManagerService, CurrencyManagerService>();
            // Additional services here...
        }
    }
}
