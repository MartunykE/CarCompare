using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.API.Models
{
    public class Manufacturer
    {
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public string MaufacturerName { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
