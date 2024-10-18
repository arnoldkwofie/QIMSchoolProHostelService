using Azure.Core;
using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.eRoomTypeAmenity.Commands
{
    public static class CreateRoomTypeAmenity
    {
        public class CreateRoomTypeAmenityCommand : IRequest
        {
            public RommTypeAmenityCommand Payload { get; set; }
        }

        public class Handler : IRequestHandler<CreateRoomTypeAmenityCommand>
        {
            private readonly RoomTypeAmenityProcessor _roomTypeAmenityProcessor;
            public Handler(RoomTypeAmenityProcessor roomTypeAmenityProcessor)
            {
                _roomTypeAmenityProcessor = roomTypeAmenityProcessor;
            }

            public async Task Handle(CreateRoomTypeAmenityCommand request, CancellationToken cancellationToken)
            {
                await _roomTypeAmenityProcessor.Create(request.Payload, cancellationToken);
            }
        }
    }
}