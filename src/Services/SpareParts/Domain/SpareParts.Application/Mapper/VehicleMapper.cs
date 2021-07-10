using MongoDB.Bson;
using SpareParts.Application.DTO;
using SpareParts.Domain.Models;
using SpareParts.Domain.Models.VehicleTechSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpareParts.Application.Mapper
{
    public class VehicleMapper
    {
        public virtual Vehicle MapToVehicle(VehicleDTO vehicleDTO)
        {
            ObjectId objectId;

            var vehicle = new Vehicle
            {
                Id = ObjectId.TryParse(vehicleDTO.Id, out objectId) ? objectId : ObjectId.GenerateNewId(),
                Model = vehicleDTO.Model,
                Generation = vehicleDTO.Generation,
                ManufacturerName = vehicleDTO.ManufacturerName,
                StartProductionYear = vehicleDTO.StartProductionYear,
                EndProductionYear = vehicleDTO.EndProductionYear,
            };
            vehicle.VehicleTechSpecifications.Add(new VehicleTechSpecification
            {
                Id = ObjectId.TryParse(vehicleDTO.VehicleTechSpecification.Id, out objectId) ? objectId : ObjectId.GenerateNewId(),
                Engine = new Engine
                {
                    Id = ObjectId.TryParse(vehicleDTO.VehicleTechSpecification.Engine.Id, out objectId) ? objectId : ObjectId.GenerateNewId(),
                    Name = vehicleDTO.VehicleTechSpecification.Engine.Name,
                    EngineCapacity = vehicleDTO.VehicleTechSpecification.Engine.EngineCapacity,
                    HorsePowers = vehicleDTO.VehicleTechSpecification.Engine.HorsePowers,
                    Petrol = vehicleDTO.VehicleTechSpecification.Engine.Petrol
                },
                GearBox = new GearBox
                {
                    Id = ObjectId.TryParse(vehicleDTO.VehicleTechSpecification.GearBox.Id, out objectId) ? objectId : ObjectId.GenerateNewId(),
                    Name = vehicleDTO.VehicleTechSpecification.GearBox.Name,
                    GearBoxType = vehicleDTO.VehicleTechSpecification.GearBox.GearBoxType,
                    GearsCount = vehicleDTO.VehicleTechSpecification.GearBox.GearsCount
                },
                AdditionalCharacteristics = vehicleDTO.VehicleTechSpecification.AdditionalCharacteristics,
                SpareParts = vehicleDTO.VehicleTechSpecification.SpareParts.Select(part => new SparePart
                {
                    Id = ObjectId.TryParse(part.Id, out objectId) ? objectId : ObjectId.GenerateNewId(),
                    Name = part.Name
                })
                .ToList()

            });

            return vehicle;
        }

        public VehicleDTO MapToVehicleDTO(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SparePart> MapToSparePartsCollection(IEnumerable<SparePartDTO> sparePartsDTO)
        {
            return sparePartsDTO.Select(s => new SparePart
            {
                Id = ObjectId.Parse(s.Id),
                Name = s.Name,
                Prices = s.Prices
            });
        }
    }
}
