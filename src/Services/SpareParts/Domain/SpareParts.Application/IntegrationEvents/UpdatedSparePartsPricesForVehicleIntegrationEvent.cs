using EventBus.Abstractions.Events;
using SpareParts.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.IntegrationEvents
{
    public class UpdatedSparePartsPricesForVehicleIntegrationEvent: IntegrationEvent
    {
        public string VehicleId { get; set; }
        public string VehicleTechSpecificationId { get; set; }
        public ICollection<SparePartDTO> SpareParts { get; set; }
        public UpdatedSparePartsPricesForVehicleIntegrationEvent(string vehicleId, string vehicleTechSpecificationId, ICollection<SparePartDTO> spareParts)
        {
            VehicleId = vehicleId;
            VehicleTechSpecificationId = vehicleTechSpecificationId;
            SpareParts = spareParts;
        }
    }
}
