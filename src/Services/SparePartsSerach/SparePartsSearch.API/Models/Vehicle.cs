using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Models
{
    public class Vehicle
    {
        public string Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int? Generation { get; set; }
        public DateTime StartProductionYear { get; set; }
        public DateTime? EndProductionYear { get; set; }
        public VehicleTechSpecification VehicleTechSpecification { get; set; }
    }
}
