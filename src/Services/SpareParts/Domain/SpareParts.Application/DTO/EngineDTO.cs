using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class EngineDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double EngineCapacity { get; set; }
        [Required]
        public int HorsePowers { get; set; }
        [Required]
        public string Petrol { get; set; }
    }
}
