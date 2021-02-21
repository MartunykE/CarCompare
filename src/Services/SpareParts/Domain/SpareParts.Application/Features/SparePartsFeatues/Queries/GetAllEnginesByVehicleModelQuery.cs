using MediatR;
using SpareParts.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Application.Features.SparePartsFeatues.Queries
{
    public class GetAllEnginesByVehicleModelQuery : IRequest<IEnumerable<EngineDTO>>
    {
        public string ManufacturerName { get; }
        public string Model { get; }
        public int? Generation { get; }
        public GetAllEnginesByVehicleModelQuery(string manufacturerName, string vehicleModel, int? generation)
        {
            ManufacturerName = manufacturerName;
            Model = vehicleModel;
            Generation = generation;
        }

        class GetAllEnginesByVehicleModelQueryHandler : IRequestHandler<GetAllEnginesByVehicleModelQuery, IEnumerable<EngineDTO>>
        {
            public Task<IEnumerable<EngineDTO>> Handle(GetAllEnginesByVehicleModelQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
