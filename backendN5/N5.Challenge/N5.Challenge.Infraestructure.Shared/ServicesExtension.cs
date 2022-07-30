using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.Challenge.Core.Application.Interfaces;
using N5.Challenge.Infraestructure.Shared.Service;

namespace N5.Challenge.Infraestructure.Shared
{
    public static class ServicesExtension
    {
        public static void AddSharedInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();            
        }
    }
}
