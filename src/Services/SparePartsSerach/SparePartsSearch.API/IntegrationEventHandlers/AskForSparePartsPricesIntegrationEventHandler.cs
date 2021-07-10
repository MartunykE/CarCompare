using EventBus.Abstractions;
using System.Threading.Tasks;
using SparePartsSearch.API.IntegrationEvents;
using SparePartsSearch.API.Services.Abstractions;
using SparePartsSearch.API.Models;

namespace SparePartsSearch.API.IntegrationEventHandlers
{
    public class AskForSparePartsPricesIntegrationEventHandler : IIntegrationEventHandler<AskForSparePartsPricesIntegrationEvent>
    {

        private readonly ISearchService searchService;
        private readonly IEventBus eventBus;
        public AskForSparePartsPricesIntegrationEventHandler(ISearchService searchService, IEventBus eventBus)
        {
            this.searchService = searchService;
            this.eventBus = eventBus;
        }
        public async Task Handle(AskForSparePartsPricesIntegrationEvent @event)
        {
            var vehicle = @event.Vehicle;
            var vehcileCharachetistics = CreateVehicleCharacteristicsString(vehicle);

            foreach (var sparePart in vehicle.VehicleTechSpecification.SpareParts)
            {
                sparePart.Prices = await searchService.FindSparePartPrice(sparePart.Name, vehcileCharachetistics);
            }
            var updatedSparePartsPricesEvent = new UpdatedSparePartsPricesForVehicleIntegrationEvent(
                vehicle.Id, vehicle.VehicleTechSpecification.Id, vehicle.VehicleTechSpecification.SpareParts);

            eventBus.Publish(updatedSparePartsPricesEvent);
        }


        private string CreateVehicleCharacteristicsString(Vehicle vehicle)
        {
            string baseVehicle = $"{vehicle.ManufacturerName} {vehicle.Model} {vehicle.Generation} ";
            //string engine = $"{vehicle.VehicleTechSpecification.Engine.Name} {vehicle.VehicleTechSpecification.Engine.EngineCapacity} " +
            //    $"{vehicle.VehicleTechSpecification.Engine.Petrol} {vehicle.VehicleTechSpecification.Engine.HorsePowers} ";
            //string gearbox = $"{vehicle.VehicleTechSpecification.GearBox.Name} {vehicle.VehicleTechSpecification.GearBox.GearBoxType} ";
            return baseVehicle;
        }
    }
}
