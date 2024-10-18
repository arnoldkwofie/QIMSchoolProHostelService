using Azure.Core;
using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Room.Commands
{
    public static class CreateBed
    {
        public class CreateBedCommand : IRequest
        {
            public BedCommand Payload { get; set; }
        }

        public class Handler : IRequestHandler<CreateBedCommand>
        {
            private readonly BedProcessor _bedProcessor;
            public Handler(BedProcessor bedProcessor)
            {
                _bedProcessor = bedProcessor;
            }

            public async Task Handle(CreateBedCommand request, CancellationToken cancellationToken)
            {
                await _bedProcessor.Create(request.Payload, cancellationToken);
            }
        }
    }
}