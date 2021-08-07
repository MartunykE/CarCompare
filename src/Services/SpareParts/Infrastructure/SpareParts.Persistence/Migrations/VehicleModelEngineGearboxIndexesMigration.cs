using MongoDB.Driver;
using SpareParts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Persistence.Migrations
{
    class VehicleModelEngineGearboxIndexesMigration : IMigration
    {
        public int DbVersion => 2;

        public void Down(IMongoDatabase database)
        {
            throw new NotImplementedException();
        }

        public void Up(IMongoDatabase database)
        {

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

            var vehiclesCollection = database.GetCollection<Vehicle>("Vehicles");

            vehiclesCollection.Indexes.CreateOne(indexModel);
        }
    }
}
