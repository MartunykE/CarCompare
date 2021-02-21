using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class VehicleTechSpecificationDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Engine Engine { get; set; }
        public GearBox GearBox { get; set; }
        public IDictionary<string, string> AdditionalCharacteristics { get; set; }
        public ICollection<SparePartDTO> SpareParts { get; set; }
    }
}
