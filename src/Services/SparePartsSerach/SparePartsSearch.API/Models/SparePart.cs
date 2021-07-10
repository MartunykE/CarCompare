
namespace SparePartsSearch.API.Models
{
    public class SparePart
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SparePartPrices Prices { get; set; }

    }
}
