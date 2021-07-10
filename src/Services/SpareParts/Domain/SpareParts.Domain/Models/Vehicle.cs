using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SpareParts.Domain.Models
{
    public class Vehicle
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int? Generation { get; set; }
        public DateTime StartProductionYear { get; set; }
        public DateTime? EndProductionYear { get; set; }
        public ICollection<VehicleTechSpecification.VehicleTechSpecification> VehicleTechSpecifications{ get; set; }

        public Vehicle()
        {
            VehicleTechSpecifications = new List<VehicleTechSpecification.VehicleTechSpecification>();
        }

    }
}
