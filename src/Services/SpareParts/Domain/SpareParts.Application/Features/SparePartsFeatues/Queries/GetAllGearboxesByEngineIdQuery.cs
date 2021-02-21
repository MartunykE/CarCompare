using MediatR;
using SpareParts.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Application.Features.SparePartsFeatues.Queries
{
    public class GetAllGearboxesByEngineIdQuery: IRequest<IEnumerable<GearboxDTO>>
    {
        public int EngineId{ get; }
        public GetAllGearboxesByEngineIdQuery(int engineId)
        {
            EngineId = engineId;
        }

        class GetAllGearboxesByEngineIdQueryHandler : IRequestHandler<GetAllGearboxesByEngineIdQuery, IEnumerable<GearboxDTO>>
        {
            public Task<IEnumerable<GearboxDTO>> Handle(GetAllGearboxesByEngineIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
