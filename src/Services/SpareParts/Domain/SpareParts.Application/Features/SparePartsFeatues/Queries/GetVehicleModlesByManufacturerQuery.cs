using CSharpFunctionalExtensions;
using MediatR;
using MongoDB.Driver;
using Serilog;
using SpareParts.Application.DTO;
using SpareParts.Application.Interfaces;
using SpareParts.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Application.Features.SparePartsFeatues.Queries
{
    public class GetVehicleModlesByManufacturerQuery : IRequest<Maybe<IEnumerable<VehicleModelDTO>>>
    {
        public string MaufacturerName { get; }
        public GetVehicleModlesByManufacturerQuery(string maufcaturerName)
        {
            MaufacturerName = maufcaturerName;
        }

        public class GetVehicleModlesByManufacturerQueryHandler : IRequestHandler<GetVehicleModlesByManufacturerQuery, Maybe<IEnumerable<VehicleModelDTO>>>
        {
            private readonly ISparePartsDbContext sparePartsDbContext;
            private readonly VehicleMapper vehicleMapper;
            private readonly ILogger logger;

            public GetVehicleModlesByManufacturerQueryHandler(ISparePartsDbContext sparePartsDbContext, VehicleMapper vehicleMapper, ILogger logger)
            {
                this.sparePartsDbContext = sparePartsDbContext;
                this.vehicleMapper = vehicleMapper;
                this.logger = logger;
            }

            public async Task<Maybe<IEnumerable<VehicleModelDTO>>> Handle(GetVehicleModlesByManufacturerQuery request, CancellationToken cancellationToken)
            {
                var vehicles = await (await sparePartsDbContext.Vehicles.FindAsync(v => v.ManufacturerName == request.MaufacturerName)).ToListAsync();
                
                if (vehicles == null || vehicles.Count == 0)
                {
                    return Maybe<IEnumerable<VehicleModelDTO>>.None;
                }

                List<VehicleModelDTO> vehicleModels = new List<VehicleModelDTO>();
                foreach (var vehicle  in vehicles)
                {
                    vehicleModels.Add(new VehicleModelDTO
                    {
                        ManufacturerName = vehicle.ManufacturerName,
                        Model = vehicle.Model,
                        Generation = vehicle.Generation,
                    });
                }

                return vehicleModels;
            }
        }
    }
}
