using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SpareParts.Application.DTO;
using SpareParts.Domain.Models;

namespace SpareParts.Application.Features.SparePartsFeatues.Queries
{
    public class GetVehicleByIdQuery : IRequest<VehicleDTO>
    {


        class GetSparePartsByVehicleQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleDTO>
        {
            public Task<VehicleDTO> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
