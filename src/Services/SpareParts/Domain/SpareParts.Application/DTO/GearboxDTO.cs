using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class GearboxDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string GearBoxType { get; set; }
        [Required]
        public int GearsCount { get; set; }
    }
}
