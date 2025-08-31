using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Million.Properties.Application.Contracts.Persistence;
using Million.Properties.Infrastructure.Persistence;
using Million.Properties.Infrastructure.Persistence.Repositories;
using MongoDB.Driver;

namespace Million.Properties.Infrastructure.Startup
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
        {
            services.Configure<MongoSettings>(cfg.GetSection("MongoSettings"));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var cs = sp.GetRequiredService<IOptions<MongoSettings>>().Value.ConnectionString;
                return new MongoClient(cs);
            });

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var opt = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(opt.DatabaseName);
            });

            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();


            services.AddHealthChecks()
                .AddMongoDb(
                    sp => sp.GetRequiredService<IMongoClient>(),
                    name: "mongodb",
                    timeout: TimeSpan.FromSeconds(3));

            return services;
        }
    }
}

