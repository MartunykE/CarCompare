using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class EngineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EngineCapacity { get; set; }
        public int HorsePowers { get; set; }
        public string Petrol { get; set; }
    }
}
