using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Domain.Models.VehicleTechSpecification
{
    public class GearBox
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string GearBoxType { get; set; }
        public int GearsCount { get; set; }

    }
}
