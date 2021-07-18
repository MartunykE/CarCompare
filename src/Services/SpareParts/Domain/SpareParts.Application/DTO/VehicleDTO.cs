using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class VehicleDTO
    {
        public string Id { get; set; }
        [Required]
        public string ManufacturerName { get; set; }
        [Required]
        public string Model { get; set; }
        public int? Generation { get; set; }
        [Required]
        public DateTime StartProductionYear { get; set; }
        public DateTime? EndProductionYear { get; set; }
        public VehicleTechSpecificationDTO VehicleTechSpecification { get; set; }
    }
}
