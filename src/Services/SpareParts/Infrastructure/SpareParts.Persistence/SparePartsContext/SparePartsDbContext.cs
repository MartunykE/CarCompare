using MongoDB.Driver;
using SpareParts.Application.Interfaces;
using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Persistence.SparePartsContext
{
    public class SparePartsDbContext : ISparePartsDbContext
    {
        private readonly IMongoDatabase database;
        public SparePartsDbContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            database = client.GetDatabase("VehicleSparePartsDb");
        }

        public IMongoCollection<Vehicle> Vehicles => database.GetCollection<Vehicle>("Vehicles");
        public IMongoCollection<VehicleTechSpecification> VehicleTechSpecifications =>
            database.GetCollection<VehicleTechSpecification>("VehicleTechSpecifications") ;

    }
}
