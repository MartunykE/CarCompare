
namespace SparePartsSearch.API.Models
{
    public class SparePart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SparePartPrices Prices { get; set; }

    }
}
