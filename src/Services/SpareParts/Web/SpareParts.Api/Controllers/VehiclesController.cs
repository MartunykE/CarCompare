using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpareParts.Application.DTO;

namespace SpareParts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController: ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetVehicleById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Vehicles")]
        public IActionResult GetVehiclesByEngine(EngineDTO engineDTO)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Manufacturers/{manufacturerName}")]
        public IActionResult GetVehiclesByManufacturerName(string manufacturerName)
        {
            throw new NotImplementedException();
        }




        [HttpPost]
        public IActionResult CreateVehicle([FromBody]VehicleDTO vehicleDTO)
        {
            throw new NotImplementedException();
        }
    }
}
