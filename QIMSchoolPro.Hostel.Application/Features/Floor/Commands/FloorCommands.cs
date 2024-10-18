using Azure.Core;
using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Floor.Commands
{
    public static class CreateFloor
    {
        public class CreateFloorCommand : IRequest
        {
            public FloorCommand Payload { get; set; }
        }

        public class Handler : IRequestHandler<CreateFloorCommand>
        {
            private readonly FloorProcessor _floorProcessor;
            public Handler(FloorProcessor floorProcessor)
            {
                _floorProcessor = floorProcessor;
            }

            public async Task Handle(CreateFloorCommand request, CancellationToken cancellationToken)
            {
                await _floorProcessor.Create(request.Payload, cancellationToken);
            }
        }
    }
}