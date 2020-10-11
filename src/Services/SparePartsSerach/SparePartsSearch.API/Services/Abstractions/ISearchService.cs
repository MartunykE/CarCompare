using SparePartsSearch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Services.Abstractions
{
    public interface ISearchService
    {
        public SparePartPrices FindSparePartPrice(string sparePartName, string carCharacteristics);
    }
}
