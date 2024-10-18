using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.RoomType.Commands
{
    public static class CreateRoomType
    {

        public class CreateRoomTypeCommand : IRequest
        {
            public RoomTypeCommand Payload { get; set; }
        }

        public class Handler : IRequestHandler<CreateRoomTypeCommand>
        {
            private readonly RoomTypeProcessor _roomTypeProcessor;

            public Handler(RoomTypeProcessor roomTypeProcessor)
            {
                _roomTypeProcessor = roomTypeProcessor;

            }
            public async Task Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
            {
                await _roomTypeProcessor.Create(request.Payload, cancellationToken);
            }
        }
    }




}
