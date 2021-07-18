using EventBus.Abstractions;
using MongoDB.Driver;
using SpareParts.Application.IntegrationEvents;
using SpareParts.Application.Interfaces;
using SpareParts.Application.Mapper;
using SpareParts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using Serilog;

namespace SpareParts.Application.IntegrationEventHandlers
{
    public class UpdatedSparePartsPricesForVehicleIntegrationEventHandler : IIntegrationEventHandler<UpdatedSparePartsPricesForVehicleIntegrationEvent>
    {
        private readonly ISparePartsDbContext sparePartsDbContext;
        private readonly ILogger logger;

        public UpdatedSparePartsPricesForVehicleIntegrationEventHandler(ISparePartsDbContext sparePartsDbContext, ILogger logger)
        {
            this.sparePartsDbContext = sparePartsDbContext;
            this.logger = logger;
        }
        public async Task Handle(UpdatedSparePartsPricesForVehicleIntegrationEvent @event)
        {
            var vehicleCursor = await sparePartsDbContext.Vehicles.FindAsync(v => v.Id == ObjectId.Parse(@event.VehicleId));
            var vehicle = await vehicleCursor.FirstOrDefaultAsync();

            if (vehicle == null)
            {
                logger.Warning($"Error when processing event {@event.GetType().Name} Vehicle with id: {@event.VehicleId} wasn`t found");
                return;
            }

            var spareParts = vehicle.VehicleTechSpecifications
                .FirstOrDefault(spec => spec.Id == ObjectId.Parse(@event.VehicleTechSpecificationId)).SpareParts;

            SparePartPrices prices;
            foreach (var sparePart in spareParts)
            {
                prices = @event.SpareParts.FirstOrDefault(sp => sp.Name == sparePart.Name).Prices;
                
                if (prices == null ||
                    prices.MaxPrice == 0 ||
                    prices.AveragePrice == 0 ||
                    prices.MinPrice == 0)
                {
                    continue;
                }
                
                sparePart.Prices = prices;
            }

            var update = Builders<Vehicle>.Update.Set(
                x => x.VehicleTechSpecifications.ElementAt(-1).SpareParts,
                spareParts);

            var filter = Builders<Vehicle>.Filter.And(
                Builders<Vehicle>.Filter.Eq(v => v.Id, vehicle.Id),
                Builders<Vehicle>.Filter.ElemMatch(v => v.VehicleTechSpecifications, a => a.Id == ObjectId.Parse(@event.VehicleTechSpecificationId)));

            var updateResult = await sparePartsDbContext.Vehicles.UpdateOneAsync(filter, update);
        }
    }
}
