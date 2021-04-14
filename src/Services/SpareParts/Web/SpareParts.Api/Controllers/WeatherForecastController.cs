using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SpareParts.Application.IntegrationEvents;
using SpareParts.Application.Interfaces;

namespace SpareParts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {       
        private readonly ISparePartsIntegrationEventService sparePartsIntegrationEventService;       
        private readonly ILogger<WeatherForecastController> logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ISparePartsIntegrationEventService sparePartsIntegrationEventService)
        {
            this.sparePartsIntegrationEventService = sparePartsIntegrationEventService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<string> Get( )
        {

            //return "123";
            var httpClient = new HttpClient();
            var result = await httpClient.GetAsync("http://sparepartssearch.api:80/search/трос ручного тормоза/Ford foucs 3");
            return await result.Content.ReadAsStringAsync() + " From Onion";
        }
        [HttpGet("evt")]
        public async Task<string> PostTestEvent()
        {
            logger.LogWarning("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");

            await sparePartsIntegrationEventService.PublishThroughEventBusAsync(new TestIntegrationEvent("Test event hello"));
            return "posted";
        }
        [HttpGet("file")]
        public string CreateFile()
        {
            using (StreamWriter writer = new StreamWriter("/ContainerLogger/CreatedFile.txt"))
            {
                writer.WriteLine("Hello");
            }
            return "Done";

        }


    }
}
