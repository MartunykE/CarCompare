using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Models
{
    public class SparePartPrices
    {
        
        public string Name { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public double AveragePrice { get; set; }

    }
}
