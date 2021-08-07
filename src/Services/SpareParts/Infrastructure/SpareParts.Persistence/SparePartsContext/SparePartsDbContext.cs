using MongoDB.Driver;
using MongoDB.Bson;
using SpareParts.Application.Interfaces;
using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using SpareParts.Persistence.Migrations.Services;

namespace SpareParts.Persistence.SparePartsContext
{
    public class SparePartsDbContext : ISparePartsDbContext
    {
        private readonly IMongoDatabase database;
        public SparePartsDbContext(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase("VehicleSparePartsDb");
        }

        public IMongoCollection<Vehicle> Vehicles => database.GetCollection<Vehicle>("Vehicles");

    }
}
