using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Building.Queries;
using QIMSchoolPro.Hostel.Application.Features.Floor.Commands;
using QIMSchoolPro.Hostel.Application.Features.Room.Commands;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Processors;
using QIMSchoolProHostelService.Controllers.Base;
using static QIMSchoolPro.Hostel.Application.Features.Floor.Commands.CreateFloor;

namespace QIMSchoolProHostelService.Controllers
{
  
    public class RoomController : BaseController
    {
       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateRoom.CreateRoomCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<RoomViewModel>> GetsByHostelId(int id)
        {
            return await Mediator.Send(new GetRoomsByHostelId.Query(id));
        }

    }
}
