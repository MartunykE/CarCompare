using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    class VehicleDTO
    {
        public int Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public VehicleType VehicleType { get; set; }
        public int? Generation { get; set; }
        public DateTime StartProductionYear { get; set; }
        public DateTime? EndProductionYear { get; set; }
        public ICollection<VehicleTechSpecificationDTO> VehicleTechSpecifications { get; set; }
    }
}
