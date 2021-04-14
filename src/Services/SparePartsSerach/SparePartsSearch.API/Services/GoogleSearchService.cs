using SparePartsSearch.API.Models;
using SparePartsSearch.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Services
{
    public class GoogleSearchService : ISearchService
    {
        public Task<SparePartPrices> FindSparePartPrice(string sparePartName, string carCharacteristics)
        {
            var googleHtml = GetGoogleHtml(sparePartName, carCharacteristics);

            using (StreamWriter streamWriter = new StreamWriter("google.html"))
            {
                streamWriter.WriteLine(googleHtml);
            }

            var prices = GetPricesFromGoogleHtml(googleHtml, sparePartName);

            SparePartPrices sparePartPrices = new SparePartPrices
            {
                AveragePrice = 5             
            };

            return Task.FromResult(sparePartPrices);
        }

        private string GetGoogleHtml(string sparePartName, string carCharacteristics)
        {
            var requestString = CreateGoogleRequestString(sparePartName, carCharacteristics);

            WebRequest request = WebRequest.Create(requestString);
            string html = "";

            var webResponse = (HttpWebResponse)request.GetResponse();

            using (Stream stream = webResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            webResponse.Close();
            if (string.IsNullOrWhiteSpace(html))
            {
                throw new KeyNotFoundException($"Can`t find results for query params {sparePartName}, {carCharacteristics} ");
            }

            return html;
        }

        private string CreateGoogleRequestString(string sparePartName, string carCharacteristics)
        {
            Regex pattern = new Regex("\\w+");
            var sparePartNameMatches = pattern.Matches(sparePartName);
            var carCharacteristicsMatches = pattern.Matches(carCharacteristics);
            StringBuilder sparePartBuilder = new StringBuilder();
            foreach (var word in sparePartNameMatches)
            {
                sparePartBuilder.Append(word + "+");
            }
            StringBuilder carCharacteristicsBuilder = new StringBuilder();

            foreach (var word in carCharacteristicsMatches)
            {
                carCharacteristicsBuilder.Append(word + "+");
            }

            var requestString = $"https://www.google.com/search?q={sparePartBuilder}+{carCharacteristicsBuilder}&tbm=shop";

            return requestString;
        }

        private List<double> GetPricesFromGoogleHtml(string googleHtml, string sparePartName)
        {
            var prices = new List<double>();
            string startPoint = "<div class=\"P8xhZc\">";
            int maxPricesCount = 15;
            for (int i = 0; i < maxPricesCount; i++)
            {
                var startIndex = googleHtml.IndexOf(startPoint) + startPoint.Length;
                googleHtml = googleHtml.Substring(startIndex);

                //get advetisement header 
                var advertisementTagStartIndex = googleHtml.IndexOf("<a");
                var advertisementTagEndIndex = googleHtml.IndexOf(">", advertisementTagStartIndex);

                var nextTagLength = 4;
                googleHtml = googleHtml.Substring(advertisementTagEndIndex + nextTagLength);

                var advetisementTextEndIndex = googleHtml.IndexOf("<");
                var adveticementText = googleHtml.Substring(0, advetisementTextEndIndex);

                var decodedAdvetisementText = DecodeFromDecimalToText(adveticementText);

                var sparePartNameWithoutSpaces = sparePartName.Replace(" ", string.Empty);
                if (decodedAdvetisementText.ToLower().Contains(sparePartNameWithoutSpaces.ToLower()))
                {
                    var price = ParsePrice(googleHtml);
                    if (price != default)
                    {
                        prices.Add(price);
                    }
                }
            }
            return prices;
        }
        private double ParsePrice(string priceString)
        {
            int spanBeginIndex = priceString.IndexOf("<span");
            int spanBeginBracketIndex = priceString.IndexOf(">", spanBeginIndex) + 1;

            int spanEndIndex = priceString.IndexOf("</span>");
            var span = priceString.Substring(spanBeginBracketIndex, spanEndIndex - spanBeginIndex);

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

            var formatter = new NumberFormatInfo { NumberDecimalSeparator = "," };

            var parsed = double.TryParse(price, NumberStyles.Float, formatter, out double parseResult);
            //if (!parsed && parseResult <= 0)
            //{
            //    throw new Exception($"Can`t find price in string: {priceString}");
            //}
            return parseResult;
        }

        private string DecodeFromDecimalToText(string advertisementText)
        {
            Regex regex = new Regex("\\w+");
            var matches = regex.Matches(advertisementText);
            var text = new StringBuilder();
            foreach (Match match in matches)
            {
                var canParse = int.TryParse(match.Value, out int letterNumber);
                if (canParse)
                {
                    char letter = (char)letterNumber;
                    text.Append(letter);
                }

            }

            return text.ToString();
        }
    }
}
