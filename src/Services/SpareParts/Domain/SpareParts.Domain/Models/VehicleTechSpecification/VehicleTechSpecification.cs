using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace SpareParts.Domain.Models.VehicleTechSpecification
{
    public class VehicleTechSpecification
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public Engine Engine { get; set; }
        public GearBox GearBox { get; set; }
        public IDictionary<string, string> AdditionalCharacteristics { get; set; }
        public ICollection<SparePart> SpareParts { get; set; }
        public DateTime LastSparePartsPricesUpdateDate { get; set; }
        public VehicleTechSpecification()
        {
            AdditionalCharacteristics = new Dictionary<string, string>();
            SpareParts = new List<SparePart>();
        }
    }
}
