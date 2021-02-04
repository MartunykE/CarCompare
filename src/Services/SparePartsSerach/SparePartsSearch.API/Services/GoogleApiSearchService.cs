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
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Util;

namespace SparePartsSearch.API.Services
{
    public class GoogleApiSearchService : ISearchService
    {
        private string apiKey;
        private string searchEngineId;

        public GoogleApiSearchService(string googeApiKey, string googleSearchEngineId)
        {
            apiKey = googeApiKey;
            searchEngineId = googleSearchEngineId;
        }

        public async Task<SparePartPrices> FindSparePartPrice(string sparePartName, string carCharacteristics)
        {
            var response = await GetGoogleSearchResponse(sparePartName, carCharacteristics);
            response.ThrowIfNull($"{sparePartName} {carCharacteristics}");
            var price = await GetPricesFromSearch(response);
            return new SparePartPrices
            {
                Name = sparePartName,
                Price = price
            };
        }

        private async Task<Search> GetGoogleSearchResponse(string sparePartName, string carCharacteristics)
        {
            var searchService = new CustomsearchService(new BaseClientService.Initializer
            {
                ApiKey = apiKey
            });

            CseResource.ListRequest listRequest = searchService.Cse.List();
            listRequest.Q = $"{sparePartName} {carCharacteristics}";
            listRequest.Cx = searchEngineId;

            return await listRequest.ExecuteAsync();           
        }

        private async Task<Price> GetPricesFromSearch(Search search)
        {
            var highPrices = new List<double>();
            var lowPrices = new List<double>();
            string currency = "";
            foreach (var item in search.Items)
            {
                var priceNodeName = "aggregateoffer";

                var priceNode = item.Pagemap.FirstOrDefault(p => p.Key == priceNodeName);

                if (priceNode.Value != null)
                {
                    var priceNodeArray = JArray.Parse(priceNode.Value.ToString()).Children().FirstOrDefault();
                    var priceNodeCurrency = priceNodeArray["pricecurrency"].ToString();
                    
                    if (string.IsNullOrEmpty(currency))
                    {
                        currency = priceNodeCurrency;
                    }

                    if (currency == priceNodeCurrency)
                    {
                        highPrices.Add((double)priceNodeArray["highprice"]);
                        lowPrices.Add((double)priceNodeArray["lowprice"]);
                    }
                }               
            }

            return CreatedPrice(highPrices, lowPrices, currency);

        } 

        private Price CreatedPrice(IEnumerable<double> highPrices, IEnumerable<double> lowPrices, string currency)
        {
            var maxPrice = highPrices.Max();
            var minPrice = lowPrices.Min();
            var averagePrice = (maxPrice + minPrice) / 2;

            return new Price
            {
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                AveragePrice = averagePrice
            };
        }



    }
}
