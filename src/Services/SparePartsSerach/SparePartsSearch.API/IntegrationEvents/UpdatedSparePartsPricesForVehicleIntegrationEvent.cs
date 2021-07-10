using EventBus.Abstractions.Events;
using SparePartsSearch.API.Models;
using System.Collections.Generic;

namespace SparePartsSearch.API.IntegrationEvents
{
    class UpdatedSparePartsPricesForVehicleIntegrationEvent : IntegrationEvent
    {
        public string VehicleId { get; set; }
        public string VehicleTechSpecificationId { get; set; }
        public ICollection<SparePart> SpareParts { get; set; }
        public UpdatedSparePartsPricesForVehicleIntegrationEvent(string vehicleId, string vehicleTechSpecificationId, ICollection<SparePart> spareParts)
        {
            VehicleId = vehicleId;
            VehicleTechSpecificationId = vehicleTechSpecificationId;
            SpareParts = spareParts;
        }
    }
}
