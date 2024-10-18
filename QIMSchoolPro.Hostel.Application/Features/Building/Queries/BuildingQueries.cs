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
    //public static class GetBuildings
    //{
    //    public class Query : IRequest<IEnumerable<BuildingDto>>
    //    {
    //        public Query()
    //        {

    //        }
    //    }

    //    public class Handler : IRequestHandler<Query, IEnumerable<BuildingDto>>
    //    {
    //        private readonly BuildingProcessor _buildingProcessor;

    //        public Handler(BuildingProcessor buildingProcessor)
    //        {
    //            _buildingProcessor = buildingProcessor;
    //        }

    //        public async Task<IEnumerable<BuildingDto>> Handle(Query request, CancellationToken cancellationToken)
    //        {
    //            var result = await _buildingProcessor.GetAllAsync();
    //            return result;
    //        }
    //    }
    //}

    public static class GetBuildingsByType
    {
        public class Query : IRequest<IEnumerable<BuildingViewModel>>
        {
            public Query(int type)
            {
                Type = type;
            }
            public int Type { get; set; }
        }

        public class Handler : IRequestHandler<Query, IEnumerable<BuildingViewModel>>
        {
            private readonly BuildingProcessor _buildingProcessor;

            public Handler(BuildingProcessor buildingProcessor)
            {
                _buildingProcessor = buildingProcessor;
            }

            public async Task<IEnumerable<BuildingViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _buildingProcessor.GetBuildingsByType(request.Type);
                return result;
            }
        }
    }
}
