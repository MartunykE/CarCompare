using MediatR;
using SpareParts.Application.DTO;
using SpareParts.Application.Interfaces;
using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using System.Linq;
using SpareParts.Application.Extentions;
using MongoDB.Driver;
using MongoDB.Bson;
using Serilog;
using System;
using SpareParts.Application.IntegrationEvents;
using SpareParts.Application.IntegrationEvents.Services;

namespace SpareParts.Application.Features.SparePartsFeatues.Commands
{
    public class CreateVehicleCommand : IRequest<string>
    {
        public VehicleDTO vehicleDTO { get; }

        public CreateVehicleCommand(VehicleDTO vehicleDTO)
        {
            this.vehicleDTO = vehicleDTO;
            vehicleDTO.VehicleTechSpecification.SpareParts.AddDefaultSpareParts();
        }

        class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, string>
        {
            private readonly ISparePartsDbContext sparePartsDbContext;
            private readonly ILogger logger;
            private readonly IClientSessionHandle clientSessionHandle;
            private readonly ISparePartsIntegrationEventService sparePartsIntegrationEventService;

            public CreateVehicleCommandHandler(ISparePartsDbContext sparePartsDbContext, ILogger logger, ISparePartsIntegrationEventService sparePartsIntegrationEventService, IClientSessionHandle clientSessionHandle)
            {
                this.sparePartsDbContext = sparePartsDbContext;
                this.logger = logger;
                this.clientSessionHandle = clientSessionHandle;
                this.sparePartsIntegrationEventService = sparePartsIntegrationEventService;
            }

            public async Task<string> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
            {
                var vehicleDTO = request.vehicleDTO;

                var vehicle = MapDtoToVehicle(vehicleDTO);

                var allvehicles = await sparePartsDbContext.Vehicles.FindAsync(new BsonDocument());

                var a = allvehicles.ToList();
                var existingVehicles = await sparePartsDbContext.Vehicles.FindAsync(v => 
                    v.ManufacturerName == vehicle.ManufacturerName && 
                    v.Model == vehicle.Model && 
                    v.Generation == vehicle.Generation);

                if (existingVehicles.Any())
                {
                    return existingVehicles.FirstOrDefault().Id.ToString();
                }

                var askForSparePartsEvent = new AskForSparePartsPricesIntegrationEvent(vehicle.Id.ToString(), vehicleDTO.VehicleTechSpecification.SpareParts);

                clientSessionHandle.StartTransaction();

                try
                {
                    await sparePartsDbContext.Vehicles.InsertOneAsync(vehicle);
                    await sparePartsIntegrationEventService.PublishThroughEventBusAsync(askForSparePartsEvent);
                    await clientSessionHandle.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    logger.Information($"Can`t write to db vehicle: {vehicle.ToJson()} Exception: {ex.Message}");

                    await clientSessionHandle.AbortTransactionAsync();
                    return null;
                }

                return vehicle.Id.ToString();
            }

            private Vehicle MapDtoToVehicle(VehicleDTO vehicleDTO)
            {
                //TODO: Add validation for null
                var vehicle = new Vehicle
                {
                    Id = ObjectId.GenerateNewId(),
                    Model = vehicleDTO.Model,
                    VehicleType = vehicleDTO.VehicleType,
                    Generation = vehicleDTO.Generation,
                    ManufacturerName = vehicleDTO.ManufacturerName,
                    StartProductionYear = vehicleDTO.StartProductionYear,
                    EndProductionYear = vehicleDTO.EndProductionYear,
                };
                vehicle.VehicleTechSpecifications.Add(new VehicleTechSpecification
                {
                    Id = ObjectId.GenerateNewId(),
                    Engine = new Engine
                    {
                        Id = new ObjectId(),
                        Name = vehicleDTO.VehicleTechSpecification.Engine.Name,
                        EngineCapacity = vehicleDTO.VehicleTechSpecification.Engine.EngineCapacity,
                        HorsePowers = vehicleDTO.VehicleTechSpecification.Engine.HorsePowers,
                        Petrol = vehicleDTO.VehicleTechSpecification.Engine.Petrol
                    },
                    GearBox = new GearBox
                    {
                        Id = ObjectId.GenerateNewId(),
                        Name = vehicleDTO.VehicleTechSpecification.GearBox.Name,
                        GearBoxType = vehicleDTO.VehicleTechSpecification.GearBox.GearBoxType,
                        GearsCount = vehicleDTO.VehicleTechSpecification.GearBox.GearsCount
                    },
                    AdditionalCharacteristics = vehicleDTO.VehicleTechSpecification.AdditionalCharacteristics,

                });

                return vehicle;
            }


        }

    }
}
