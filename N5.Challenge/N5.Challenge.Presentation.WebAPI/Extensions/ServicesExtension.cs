using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace N5.Challenge.Presentation.WebAPI.Extensions
{
    public static class ServicesExtension
    {
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(confing =>
            {
                confing.DefaultApiVersion = new ApiVersion(1, 0);
                confing.AssumeDefaultVersionWhenUnspecified = true;
                confing.ReportApiVersions = true;
            });
        }

        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {                
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "N5 Challenge API",
                    Description = "N5",
                    Contact = new OpenApiContact
                    {
                        Name = "WillyNet",
                        Email = "willyrhcp96@gmail.com",
                        Url = new Uri("https://www.instagram.com/_willyvanilli/"),
                    }
                });                               
            });
        }
    }
}
