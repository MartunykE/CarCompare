using MediatR;
using Serilog;
using SpareParts.Application.DTO;
using SpareParts.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using SpareParts.Domain.Models;
using MongoDB.Bson;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System.Collections.Generic;
using SpareParts.Application.Extentions;
using SpareParts.Application.IntegrationEvents.Services;
using SpareParts.Application.IntegrationEvents;

namespace SpareParts.Application.Features.SparePartsFeatues.Commands
{
    public class CreateTechSpecificationByVehicleIdCommand : IRequest<string>
    {
        public VehicleTechSpecificationDTO VehicleTechSpecificationDTO { get; }
        public string VehicleId { get; }

        public CreateTechSpecificationByVehicleIdCommand(string vehicleId, VehicleTechSpecificationDTO vehicleTechSpecificationDTO)
        {
            VehicleId = vehicleId;
            VehicleTechSpecificationDTO = vehicleTechSpecificationDTO;
            VehicleTechSpecificationDTO.SpareParts.AddDefaultSpareParts();
        }

        class CreateTechSpecificationByVehicleIdCommandHandler : IRequestHandler<CreateTechSpecificationByVehicleIdCommand, string>
        {
            private readonly ISparePartsDbContext sparePartsDbContext;
            private readonly ILogger logger;
            private readonly IMediator mediator;
            private readonly ISparePartsIntegrationEventService sparePartsIntegrationEventService;
            public CreateTechSpecificationByVehicleIdCommandHandler(
                ISparePartsDbContext sparePartsDbContext,
                ILogger logger, 
                IMediator mediator,
                ISparePartsIntegrationEventService sparePartsIntegrationEventService)
            {
                this.logger = logger;
                this.sparePartsDbContext = sparePartsDbContext;
                this.mediator = mediator;
                this.sparePartsIntegrationEventService = sparePartsIntegrationEventService;
            }

            public async Task<string> Handle(CreateTechSpecificationByVehicleIdCommand request, CancellationToken cancellationToken)
            {
                var vehicleIdFilter = Builders<Vehicle>.Filter.Eq(v => v.Id, new ObjectId(request.VehicleId));

                var techSpecificationFilter = Builders<Vehicle>.Filter.ElemMatch(
                    v => v.VehicleTechSpecifications, request.VehicleTechSpecificationDTO.ToBsonDocument());

                var filter = Builders<Vehicle>.Filter.And(vehicleIdFilter, techSpecificationFilter);

                var vehicles = await sparePartsDbContext.Vehicles.FindAsync(filter);
                var vehicle = await vehicles.FirstOrDefaultAsync();

                if (vehicle == null)
                {
                    return null;
                }

                var vehicleTechSpecifictaion = MapDtoToVehicleTechSpecification(request.VehicleTechSpecificationDTO);

                var vehicleTechSpecificationUpdateDefinition = Builders<Vehicle>.Update.Push(v => v.VehicleTechSpecifications, vehicleTechSpecifictaion);

                var askForSparePartsEvent = new AskForSparePartsPricesIntegrationEvent(request.VehicleId, request.VehicleTechSpecificationDTO.SpareParts);

                using (var session = await sparePartsDbContext.Vehicles.Database.Client.StartSessionAsync())
                {
                    session.StartTransaction();

                    try
                    {
                        await sparePartsDbContext.Vehicles.UpdateOneAsync(filter, vehicleTechSpecificationUpdateDefinition);
                        await sparePartsIntegrationEventService.PublishThroughEventBusAsync(askForSparePartsEvent);
                        await session.CommitTransactionAsync();

                        return vehicleTechSpecifictaion.Id.ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Information($"Can`t create vehicle tech specification : {vehicleTechSpecifictaion.ToJson()}. Exception: {ex.Message}");
                        session.AbortTransaction();
                        return null;
                    }
                }

            }

            public VehicleTechSpecification MapDtoToVehicleTechSpecification(VehicleTechSpecificationDTO dto)
            {
                return new VehicleTechSpecification
                {
                    Id = new ObjectId(),
                    Engine = new Engine
                    {
                        Id = new ObjectId(),
                        EngineCapacity = dto.Engine.EngineCapacity,
                        HorsePowers = dto.Engine.HorsePowers,
                        Name = dto.Engine.Name,
                        Petrol = dto.Engine.Petrol
                    },
                    GearBox = new GearBox
                    {
                        Id = new ObjectId(),
                        Name = dto.GearBox.Name,
                        GearBoxType = dto.GearBox.GearBoxType,
                        GearsCount = dto.GearBox.GearsCount
                    },
                    AdditionalCharacteristics = dto.AdditionalCharacteristics,
                    SpareParts = new List<SparePart>(),
                };
            }
        }
    }
}
