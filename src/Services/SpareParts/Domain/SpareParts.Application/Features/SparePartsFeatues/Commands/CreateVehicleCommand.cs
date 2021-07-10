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
using CSharpFunctionalExtensions;
using SpareParts.Application.Mapper;

namespace SpareParts.Application.Features.SparePartsFeatues.Commands
{
    public class CreateVehicleCommand : IRequest<Maybe<string>>
    {
        public VehicleDTO vehicleDTO { get; }

        public CreateVehicleCommand(VehicleDTO vehicleDTO)
        {
            this.vehicleDTO = vehicleDTO;
            vehicleDTO.VehicleTechSpecification.SpareParts.AddDefaultSpareParts();
        }

        public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Maybe<string>>
        {
            private readonly ISparePartsDbContext sparePartsDbContext;
            private readonly ILogger logger;
            private readonly IClientSessionHandle clientSessionHandle;
            private readonly ISparePartsIntegrationEventService sparePartsIntegrationEventService;
            private readonly VehicleMapper vehicleMapper;

            public CreateVehicleCommandHandler(ISparePartsDbContext sparePartsDbContext, ILogger logger, ISparePartsIntegrationEventService sparePartsIntegrationEventService, IClientSessionHandle clientSessionHandle, VehicleMapper vehicleMapper)
            {
                this.sparePartsDbContext = sparePartsDbContext;
                this.logger = logger;
                this.clientSessionHandle = clientSessionHandle;
                this.sparePartsIntegrationEventService = sparePartsIntegrationEventService;
                this.vehicleMapper = vehicleMapper;
            }

            public async Task<Maybe<string>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
            {
                //TODO think about IDs
                var vehicleDTO = request.vehicleDTO;
                vehicleDTO.GeneratePopertyIds();

                var vehicle = vehicleMapper.MapToVehicle(vehicleDTO);

                var existingVehiclesCursor = await sparePartsDbContext.Vehicles.FindAsync(v =>
                    v.ManufacturerName == vehicle.ManufacturerName &&
                    v.Model == vehicle.Model &&
                    v.Generation == vehicle.Generation);
                
                var existingVehicle = await existingVehiclesCursor.ToListAsync();

                if (existingVehicle.Count > 0)
                {
                    return existingVehicle.FirstOrDefault().Id.ToString();
                }

                var askForSparePartsEvent = new AskForSparePartsPricesIntegrationEvent(vehicleDTO);

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
                    return Maybe<string>.None;
                }

                return vehicle.Id.ToString();
            }
        }
    }
}
