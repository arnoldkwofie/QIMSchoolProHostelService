using MediatR;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Amenity.Commands;
using QIMSchoolPro.Hostel.Application.Features.Building.Queries;
using QIMSchoolPro.Hostel.Application.Features.Floor.Commands;
using QIMSchoolPro.Hostel.Application.Features.Room.Commands;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolProHostelService.Controllers.Base;


namespace QIMSchoolProHostelService.Controllers
{
    public class AmenityController : BaseController
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateAmenity.CreateAmenityCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        


    }
}
