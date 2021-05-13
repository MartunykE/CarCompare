using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpareParts.Domain.Models
{
    public class SparePart
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public SparePartPrices Prices { get; set; }
    }
}
