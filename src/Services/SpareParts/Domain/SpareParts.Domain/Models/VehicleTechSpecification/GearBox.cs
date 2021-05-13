using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpareParts.Domain.Models.VehicleTechSpecification
{
    public class GearBox
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string GearBoxType { get; set; }
        public int GearsCount { get; set; }

    }
}
