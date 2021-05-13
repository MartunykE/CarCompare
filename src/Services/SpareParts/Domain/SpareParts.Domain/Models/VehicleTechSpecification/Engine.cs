using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpareParts.Domain.Models.VehicleTechSpecification
{
    public class Engine
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public double EngineCapacity { get; set; }
        public int HorsePowers { get; set; }
        public string Petrol { get; set; }
    }
}
