using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Models
{
    public class Gearbox
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GearBoxType { get; set; }
        public int GearsCount { get; set; }
    }
}
