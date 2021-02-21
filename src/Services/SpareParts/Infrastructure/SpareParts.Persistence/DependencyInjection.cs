using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpareParts.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SpareParts.Persistence.SparePartsContext;
namespace SpareParts.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistance(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("VehicleSparePartsDb");
            
            serviceCollection.AddTransient<ISparePartsDbContext, SparePartsDbContext>(
                provider => new SparePartsDbContext(dbConnectionString));
        }
    }
}
