using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SpareParts.Application.Interfaces;
using SpareParts.Persistence.SparePartsContext;

namespace SpareParts.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("VehicleSparePartsDb");

            serviceCollection.AddScoped<IMongoClient>(c =>
            {
                return new MongoClient(dbConnectionString);
            });

            serviceCollection.AddScoped<ISparePartsDbContext, SparePartsDbContext>();

            serviceCollection.AddScoped(c => c.GetRequiredService<IMongoClient>().StartSession());
        }
    }
}
