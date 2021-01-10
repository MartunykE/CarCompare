using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cars.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            return "get";
        }
        [HttpGet("get2")]
        public  string Get2()
        {

            HttpClient httpClient = new HttpClient();
            //var result =  httpClient.GetAsync("http://localhost:82/api/Search/трос ручного тормоза/Ford foucs 3");

            //var result =  httpClient.GetAsync("http://sparepartssearch.api:82/weatherforecast");
            //var result = httpClient.GetAsync("http://sparepartssearch.api:80/weatherforecast/2");
            //var result = httpClient.GetAsync("http://sparepartssearch.api:80/search");


            var result = httpClient.GetAsync("http://sparepartssearch.api:80/search/трос ручного тормоза/Ford foucs 3");


            //var result = httpClient.GetAsync("http://localhost:4441/weatherforecast");
            //var result = httpClient.GetAsync("https://localhost:44345/api/User/ggg");
            //var res = result.Content.ReadAsStringAsync();
            return result.Result.Content.ReadAsStringAsync().Result;
        }

    }
}
