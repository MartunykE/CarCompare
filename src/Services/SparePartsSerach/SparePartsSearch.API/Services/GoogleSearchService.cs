using SparePartsSearch.API.Models;
using SparePartsSearch.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Services
{
    public class GoogleSearchService : ISearchService
    {
        public SparePartPrices FindSparePartPrice(string sparePartName, string[] carCharacteristics)
        {
            var googleHtml = GetGoogleHtml(sparePartName, carCharacteristics);
            var prices = GetPricesFromGoogleHtml(googleHtml);

            SparePartPrices sparePartPrices = new SparePartPrices
            {
                Name = sparePartName,
                MaxPrice = prices.Max(),
                MinPrice = prices.Min(),
                AveragePrice = prices.Average()
            };

            return sparePartPrices;
        }

        private string GetGoogleHtml(string sparePartName, string[] carCharacteristics)
        {
            StringBuilder carCharacteristic = new StringBuilder();
            foreach (var characteristic in carCharacteristics)
            {
                carCharacteristic.Append("+" + characteristic);
            }

            var requestString = $"https://www.google.com/search?q={sparePartName}+{carCharacteristic}&tbm=shop";

            WebRequest request = WebRequest.Create(requestString);
            var webResponse = (HttpWebResponse)request.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            string html = "";
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            if (string.IsNullOrWhiteSpace(html))
            {
                throw new KeyNotFoundException($"Can`t find results for query params {sparePartName}, {carCharacteristic} ");
            }
            return html;
        }
        // TODO: Filter html by name of the goods
        private List<double> GetPricesFromGoogleHtml(string googleHtml)
        {
            var prices = new List<double>();
            string startPoint = "<div class=\"xcR77\">";
            int maxPricesCount= 15;
            for (int i = 0; i < maxPricesCount; i++)
            {
                var startIndex = googleHtml.IndexOf(startPoint) + startPoint.Length;
                googleHtml = googleHtml.Substring(startIndex);

                int spanBeginIndex = googleHtml.IndexOf("<span");
                int spanBeginBracketIndex = googleHtml.IndexOf(">", spanBeginIndex) + 1;

                int spanEndIndex = googleHtml.IndexOf("</span>");
                var span = googleHtml.Substring(spanBeginBracketIndex, spanEndIndex - spanBeginIndex);

                //remove unknown chars
                for (int j = 0; j < span.Length; j++)
                {
                    if ((int)span[j] > 255)
                    {
                        var index = span.IndexOf(span[j]);
                        span = span.Remove(index, 1);
                    }

                }
                var endPriceIndex = span.IndexOf('&');
                var price = span.Remove(endPriceIndex);

                var parsed = double.TryParse(price, out double parseResult);
                if (parsed && parseResult > 0)
                {
                    prices.Add(parseResult);
                }
            }
            return prices;
        }
    }
}
