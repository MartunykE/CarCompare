using MediatR;
using SpareParts.Application.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Application.Features.SparePartsFeatues.Commands
{
    public class CreateTechSpecificationByVehicleIdCommand: IRequest<int>
    {
        class CreateTechSpecificationByVehicleIdCommandHandler : IRequestHandler<CreateTechSpecificationByVehicleIdCommand, int>
        {
            public Task<int> Handle(CreateTechSpecificationByVehicleIdCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
