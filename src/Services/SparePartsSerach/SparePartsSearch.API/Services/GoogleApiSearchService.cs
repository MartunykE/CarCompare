using SparePartsSearch.API.Models;
using SparePartsSearch.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Google.Apis.Services;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;

namespace SparePartsSearch.API.Services
{
    public class GoogleApiSearchService : ISearchService
    {
        private string apiKey = "AIzaSyADW4f_Hk9G5wYZp3r6ThEXuvXz1rAYr4A";
        private string searchEngineId = "3d995c346c46c7cf8";
        public async Task<SparePartPrices> FindSparePartPrice(string sparePartName, string carCharacteristics)
        {
            //await Run();
            return new SparePartPrices() { Name = "2", AveragePrice = 200 };
        }

        public async Task Run()
        {
            var searchService = new CustomsearchService(new BaseClientService.Initializer
            {
                ApiKey = apiKey
            });

            CseResource.ListRequest listRequest = searchService.Cse.List();
            listRequest.Q = "трос ручного тормоза ford focus 3";
            listRequest.Cx = searchEngineId;

            Search search = await listRequest.ExecuteAsync();

            foreach (var item in search.Items)
            {
                foreach (var pagemap in item.Pagemap)
                {
                    Debug.WriteLine(pagemap);
                }
            }

        }
    }
}
