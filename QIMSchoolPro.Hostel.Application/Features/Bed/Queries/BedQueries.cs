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
    public static class GetBedsByRoomId
    {
        public class Query : IRequest<IEnumerable<BedDto>>
        {
            public Query(int id)
            {
                Id=id;
            }
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<BedDto>>
        {
            private readonly BedProcessor _bedProcessor;

            public Handler(BedProcessor bedProcessor)
            {
                _bedProcessor = bedProcessor;
            }

            public async Task<IEnumerable<BedDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _bedProcessor.GetBedsByRoomId(request.Id);
                return result;
            }
        }
    }

   
}
