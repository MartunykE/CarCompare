using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SpareParts.Application.Interfaces;

namespace SpareParts.Api.Controllers
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
        private readonly ISparePartsDbContext sparePartsDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISparePartsDbContext sparePartsDbContext)
        {
            _logger = logger;
            this.sparePartsDbContext = sparePartsDbContext;
        }

        [HttpGet]
        public IEnumerable<SpareParts.Domain.Models.SparePart> Get( )
        {
            sparePartsDbContext.Vehicles.InsertOne(new Domain.Models.SparePart() { Id = 1, Name = "piter" });
            var a = sparePartsDbContext.Vehicles.Find((car) => true).ToList();
            return a;
        }

        
    }
}
