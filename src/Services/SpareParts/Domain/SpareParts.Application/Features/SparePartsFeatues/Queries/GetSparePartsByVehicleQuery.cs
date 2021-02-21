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
    public class GetSparePartsByVehicleQuery : IRequest<IEnumerable<SparePartDTO>>
    {
        

        class GetSparePartsByVehicleQueryHandler : IRequestHandler<GetSparePartsByVehicleQuery, IEnumerable<SparePartDTO>>
        {
            public Task<IEnumerable<SparePartDTO>> Handle(GetSparePartsByVehicleQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
