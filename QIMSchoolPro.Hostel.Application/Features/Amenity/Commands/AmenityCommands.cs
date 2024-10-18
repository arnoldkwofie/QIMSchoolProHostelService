using Azure.Core;
using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Amenity.Commands
{
    public static class CreateAmenity
    {
        public class CreateAmenityCommand : IRequest
        {
            public AmenityCommand Payload { get; set; }
        }

        public class Handler : IRequestHandler<CreateAmenityCommand>
        {
            private readonly AmenityProcessor _amenityProcessor;
            public Handler(AmenityProcessor amenityProcessor)
            {
                _amenityProcessor = amenityProcessor;
            }

            public async Task Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
            {
                await _amenityProcessor.Create(request.Payload, cancellationToken);
            }
        }
    }
}