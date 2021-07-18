using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class VehicleModelDTO
    {
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int? Generation { get; set; }
    }
}
