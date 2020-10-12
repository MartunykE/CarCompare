using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.API.Models
{
    public class Vehicle
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public VehicleType VehicleType { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public Manufacturer Manufacturer { get; set; }
        [Required]
        public string VehicleModel { get; set; }

    }
}
