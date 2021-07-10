using System.Collections.Generic;

namespace SparePartsSearch.API.Models
{
    public class VehicleTechSpecification
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public Engine Engine { get; set; }
        public Gearbox GearBox { get; set; }
        public IDictionary<string, string> AdditionalCharacteristics { get; set; }
        public ICollection<SparePart> SpareParts { get; set; }
    }
}
