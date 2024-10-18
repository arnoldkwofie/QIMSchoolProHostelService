using MediatR;
using Microsoft.AspNetCore.Http;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Building.Commands
{
    public static class CreateBuilding
    {

        public class CreateBuildingCommand : IRequest
        {
            public BuildingCommand Payload { get; set; }

            public CreateBuildingCommand(BuildingCommand payload)
            {
                Payload = payload;
            }
        }

        public class Handler : IRequestHandler<CreateBuildingCommand>
        {
            private readonly BuildingProcessor _buildingProcessor;

            public Handler(BuildingProcessor buildingProcessor)
            {
                _buildingProcessor = buildingProcessor;

            }
            public async Task Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
            {
                await _buildingProcessor.Create(request.Payload, cancellationToken);
            }
        }


    }

}
