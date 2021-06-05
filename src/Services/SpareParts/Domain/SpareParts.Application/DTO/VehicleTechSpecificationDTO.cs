using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class VehicleTechSpecificationDTO
    {
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public EngineDTO Engine { get; set; }
        public GearboxDTO GearBox { get; set; }
        public IDictionary<string, string> AdditionalCharacteristics { get; set; }
        public ICollection<SparePartDTO> SpareParts { get; set; }
    }
}
