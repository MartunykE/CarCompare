﻿using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class VehicleDTO
    {
        public string Id { get; set; }
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int? Generation { get; set; }
        public DateTime StartProductionYear { get; set; }
        public DateTime? EndProductionYear { get; set; }
        public VehicleTechSpecificationDTO VehicleTechSpecification { get; set; }
    }
}
