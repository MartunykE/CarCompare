using SpareParts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.DTO
{
    public class SparePartDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SparePartPrices Prices { get; set; }

    }
}
