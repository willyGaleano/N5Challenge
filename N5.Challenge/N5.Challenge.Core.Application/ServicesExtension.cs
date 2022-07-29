using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using N5.Challenge.Core.Application.Behaviours;
using N5.Challenge.Core.Application.Service.Logger;
using System.Reflection;

namespace N5.Challenge.Core.Application
{
    public static class ServicesExtension
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            SerilogCloudWatcher serilogCloudWatcher = new(
                     configuration["AWS:Cloudwatch:LogGroup"],
                     configuration["AWS:Cloudwatch:LogEventLevel"],
                     configuration["AWS:Cloudwatch:accesKey"],
                     configuration["AWS:Cloudwatch:secretKey"],
                     configuration["AWS:Cloudwatch:Region"]
                    );
            serilogCloudWatcher.ConfigSeriLogAWS();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
        }
    }
}
