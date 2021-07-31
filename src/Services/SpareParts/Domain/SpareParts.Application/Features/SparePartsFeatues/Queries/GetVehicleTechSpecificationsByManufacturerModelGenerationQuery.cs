using MediatR;
using SpareParts.Application.DTO;
using SpareParts.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using CSharpFunctionalExtensions;
using SpareParts.Application.Mapper;
using SpareParts.Domain.Models;

namespace SpareParts.Application.Features.SparePartsFeatues.Queries
{
    public class GetVehicleTechSpecificationsByManufacturerModelGenerationQuery : IRequest<Maybe<IEnumerable<VehicleTechSpecificationDTO>>>
    {
        public string ManufacturerName { get; set; }
        public string Model { get; set; }
        public int? Generation { get; set; }

        public GetVehicleTechSpecificationsByManufacturerModelGenerationQuery(string manufacturerName, string model, int? generation)
        {
            ManufacturerName = manufacturerName;
            Model = model;
            Generation = generation;
        }

        class GetVehicleTechSpecificationsByManufacturerModelGenerationQueryHandler
            : IRequestHandler<GetVehicleTechSpecificationsByManufacturerModelGenerationQuery, Maybe<IEnumerable<VehicleTechSpecificationDTO>>>
        {

            private readonly ISparePartsDbContext sparePartsDbContext;
            private readonly VehicleMapper vehicleMapper;

            public GetVehicleTechSpecificationsByManufacturerModelGenerationQueryHandler(ISparePartsDbContext sparePartsDbContext, VehicleMapper vehicleMapper)
            {
                this.sparePartsDbContext = sparePartsDbContext;
                this.vehicleMapper = vehicleMapper;
            }

            public async Task<Maybe<IEnumerable<VehicleTechSpecificationDTO>>> Handle(GetVehicleTechSpecificationsByManufacturerModelGenerationQuery request, CancellationToken cancellationToken)
            {
                IAsyncCursor<Vehicle> vehiclesCursor;
                if (request.Generation != null)
                {
                    vehiclesCursor = await sparePartsDbContext.Vehicles.FindAsync(v =>
                        v.ManufacturerName == request.ManufacturerName &&
                        v.Model == request.Model &&
                        v.Generation == request.Generation);
                }
                else
                {
                    vehiclesCursor = await sparePartsDbContext.Vehicles.FindAsync(v =>
                        v.ManufacturerName == request.ManufacturerName &&
                        v.Model == request.Model);
                }
               

                var vehicleTechSpecificationDTOs = new List<VehicleTechSpecificationDTO>();

                await vehiclesCursor.ForEachAsync(vehicle =>
                {
                    foreach (var spec in vehicle.VehicleTechSpecifications)
                    {
                        vehicleTechSpecificationDTOs.Add(vehicleMapper.MapToVehicleTechSpecificationDTO(spec));
                    }
                });

                if (vehicleTechSpecificationDTOs.Count == 0)
                {
                    return Maybe<IEnumerable<VehicleTechSpecificationDTO>>.None;
                }

                return vehicleTechSpecificationDTOs;
            }
        }

    }
}
