using MediatR;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Building.Commands;
using QIMSchoolPro.Hostel.Application.Features.RoomType.Commands;
using QIMSchoolProHostelService.Controllers.Base;


namespace QIMSchoolProHostelService.Controllers
{
   
    public class RoomTypeController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateRoomType.CreateRoomTypeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


    }
}
