using MediatR;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Building.Queries;
using QIMSchoolPro.Hostel.Application.Features.Floor.Commands;
using QIMSchoolPro.Hostel.Application.Features.Room.Commands;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolProHostelService.Controllers.Base;


namespace QIMSchoolProHostelService.Controllers
{
    public class BedController : BaseController
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateBed.CreateBedCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<BedDto>> GetsByRoomId(int id)
        {
            return await Mediator.Send(new GetBedsByRoomId.Query(id));
        }


    }
}
