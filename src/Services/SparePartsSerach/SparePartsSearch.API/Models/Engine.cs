using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Models
{
    public class Engine
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double EngineCapacity { get; set; }
        public int HorsePowers { get; set; }
        public string Petrol { get; set; }
    }
}
