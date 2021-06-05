using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpareParts.Application.DTO;
using MediatR;
using SpareParts.Application.Features.SparePartsFeatues.Commands;
using Serilog;
using Newtonsoft.Json;

namespace SpareParts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController: ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;
        public VehiclesController(IMediator mediator, ILogger logger)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("ByEngine")]
        public async Task<IActionResult> GetVehiclesByEngine(EngineDTO engineDTO)
        {
            throw new NotImplementedException();
        }

        [HttpGet("Manufacturers/{manufacturerName}")]
        public async Task<IActionResult> GetVehiclesByManufacturerName(string manufacturerName)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]VehicleDTO vehicleDTO)
        {
            var createVehicleCommand = new CreateVehicleCommand(vehicleDTO);

            logger.Information($"Sending CreateVehicleCommand with parameters {vehicleDTO.ManufacturerName} {vehicleDTO.Model} {vehicleDTO.Generation}");
            var createdVehicleId = await mediator.Send(createVehicleCommand);

            if (createdVehicleId.HasNoValue)
            {
                return BadRequest($"Cannot create vehicle");
            }

            return Created("", createdVehicleId.Value);

        }


        [HttpPost("{vehicleId}/TechSpecifications")]
        public async Task<IActionResult> CreateVehicleTechSpecificationByVehnicleId(string vehicleId, [FromBody] VehicleTechSpecificationDTO vehicleTechSpecificationDTO)
        {
            var createVehicleTechSpecByVehicleIdCommand = new CreateTechSpecificationByVehicleIdCommand(vehicleId, vehicleTechSpecificationDTO);

            logger.Information($"Senidng CreateTechSpecificationByVehicleIdCommand for vehicle with id: {vehicleId} " +
                $"tech specification: Engine: {vehicleTechSpecificationDTO.Engine.Name} Gearbox {vehicleTechSpecificationDTO.GearBox}");

            var vehicleTechSpecId = await mediator.Send(createVehicleTechSpecByVehicleIdCommand);

            if (vehicleTechSpecId == null)
            {
                return BadRequest($"Cannot create vehicle tech specification for vehicle with id {vehicleId}");
            }

            return Ok(vehicleTechSpecId);
        }


    }
}
