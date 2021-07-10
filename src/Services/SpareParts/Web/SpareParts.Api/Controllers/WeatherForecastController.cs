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
using SpareParts.Application.IntegrationEvents.Services;
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

        [HttpGet("evt")]
        public async Task<string> PostTestEvent()
        {
            logger.LogWarning("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");

            await sparePartsIntegrationEventService.PublishThroughEventBusAsync(new TestIntegrationEvent(new A
            {
                Name = " aaaa",
                Year = 2
            }));
            return "posted";
        }

    }
}
