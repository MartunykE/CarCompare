using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpareParts.Application.Features.SparePartsFeatues.Commands
{
    public class CreateVehicleCommand : IRequest<int>
    {



        class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, int>
        {
            public Task<int> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
