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
    public static class CreateRoom
    {
        public class CreateRoomCommand : IRequest
        {
            public RoomCommand Payload { get; set; }
        }

        public class Handler : IRequestHandler<CreateRoomCommand>
        {
            private readonly RoomProcessor _roomProcessor;
            public Handler(RoomProcessor roomProcessor)
            {
                _roomProcessor = roomProcessor;
            }

            public async Task Handle(CreateRoomCommand request, CancellationToken cancellationToken)
            {
                await _roomProcessor.Create(request.Payload, cancellationToken);
            }
        }
    }
}