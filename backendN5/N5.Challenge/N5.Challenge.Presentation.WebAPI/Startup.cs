using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N5.Challenge.Core.Application;
using N5.Challenge.Infraestructure.Persistence;
using N5.Challenge.Infraestructure.Shared;
using N5.Challenge.Infraestructure.Shared.Util;
using N5.Challenge.Presentation.WebAPI.Extensions;
using System;

namespace N5.Challenge.Presentation.WebAPI
{
    public class Startup
    {
        readonly string myPolicy = "policyApi";
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment env)
        {
            string executionEnvironment = Environment.GetEnvironmentVariable("N5_ENVIRONMENT");
            string environmentConfigurationFile = EnvironmentFinder.GetConfigurationFileName(executionEnvironment);

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile(environmentConfigurationFile, true, true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer(Configuration);
            services.AddPersistenceInfraestructure(Configuration);
            services.AddElasticsearch(Configuration);
            services.AddSharedInfraestructure(Configuration);            
            services.AddApiVersioningExtension();
            services.AddSwaggerExtension();            
            services.AddCors(options => options.AddPolicy(myPolicy,
                             builder => builder.WithOrigins("http://localhost:5173")
                                              .AllowAnyHeader()
                                              .AllowAnyMethod()
                                              .AllowCredentials()
                            ));
            services.AddControllers();            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseErrorHandlingMiddleware();
            app.UseRouting();
            app.UseSwaggerExtension();
            app.UseCors(myPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
