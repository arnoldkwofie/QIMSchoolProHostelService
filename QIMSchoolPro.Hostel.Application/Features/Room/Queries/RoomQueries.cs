using MediatR;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Building.Queries
{
    public static class GetRoomsByBuildingId
    {
        public class Query : IRequest<IEnumerable<RoomViewModel>>
        {
            public Query(int id)
            {
                Id=id;
            }
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<RoomViewModel>>
        {
            private readonly RoomProcessor _roomProcessor;

            public Handler(RoomProcessor roomProcessor)
            {
                _roomProcessor = roomProcessor;
            }

            public async Task<IEnumerable<RoomViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _roomProcessor.GetRoomsByBuildingId(request.Id);
                return result;
            }
        }
    }

   
}
