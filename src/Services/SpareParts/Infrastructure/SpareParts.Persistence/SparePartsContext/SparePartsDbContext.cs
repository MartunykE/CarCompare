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

namespace SpareParts.Persistence.SparePartsContext
{
    public class SparePartsDbContext : ISparePartsDbContext
    {
        private readonly IMongoDatabase database;
        public SparePartsDbContext(IMongoClient mongoClient)
        {
            database = mongoClient.GetDatabase("VehicleSparePartsDb");
            ConfigureVehiclesCollection();
        }


        public IMongoCollection<Vehicle> Vehicles => database.GetCollection<Vehicle>("Vehicles");

        private void ConfigureVehiclesCollection()
        {
            //database.CreateCollection("Vehicles");
            var vehicleIndexDefinition = Builders<Vehicle>.IndexKeys.Combine(
                Builders<Vehicle>.IndexKeys.Ascending(v => v.ManufacturerName),
                Builders<Vehicle>.IndexKeys.Ascending(v => v.Model),
                Builders<Vehicle>.IndexKeys.Ascending(v => v.Generation),
                Builders<Vehicle>.IndexKeys.Ascending(v => v.StartProductionYear));

            var engineVehicleTechSpecIndexDefinition = Builders<Vehicle>.IndexKeys.Combine(
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.Engine.Name"),
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.Engine.EngineCapacity"),
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.Engine.HorsePowers"),
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.Engine.Petrol"));

            var gearBoxVehicleTechSpecIndexDefinition = Builders<Vehicle>.IndexKeys.Combine(
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.GearBox.Name"),
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.GearBox.GearBoxType"),
                Builders<Vehicle>.IndexKeys.Ascending("VehicleTechSpecification.GearBox.GearsCount"));

            var indexDefinition = Builders<Vehicle>.IndexKeys.Combine(
                vehicleIndexDefinition,
                engineVehicleTechSpecIndexDefinition,
                gearBoxVehicleTechSpecIndexDefinition);

            var indexOptions = new CreateIndexOptions { Unique = true };

            var indexModel = new CreateIndexModel<Vehicle>(indexDefinition, indexOptions);

            Vehicles.Indexes.CreateOne(indexModel);
        }



    }
}
