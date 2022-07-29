using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.Challenge.Core.Application.DTOs.Permissions;
using N5.Challenge.Core.Application.Interfaces.Repository;
using N5.Challenge.Infraestructure.Persistence.Contexts;
using N5.Challenge.Infraestructure.Persistence.Repository;
using Nest;
using System;

namespace N5.Challenge.Infraestructure.Persistence
{
    public static class ServicesExtension
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region CONTEXTS
            services.AddDbContext<DbN5Context>(
                   options => options.UseSqlServer(
                         configuration["ConnectionStrings:DefaultConnection"],
                         b => b.MigrationsAssembly(typeof(DbN5Context).Assembly.FullName)
                  )
                );
            #endregion            

            #region REPOSITORIES
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            #endregion           
        }

        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:url"];
            var defaultIndex = configuration["ELKConfiguration:index"];
            var username = configuration["ELKConfiguration:username"];
            var password = configuration["ELKConfiguration:password"];

            var settings = new ConnectionSettings(new Uri(url))
                                .BasicAuthentication(username, password)
                                .PrettyJson()
                                .DefaultIndex(defaultIndex);                                

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }
        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<PermissionsDTO>( p => p);
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<PermissionsDTO>(x => x.AutoMap())
            );
        }
    }
}
