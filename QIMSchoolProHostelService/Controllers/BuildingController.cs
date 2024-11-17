using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Building.Commands;
using QIMSchoolPro.Hostel.Application.Features.Building.Queries;
using QIMSchoolPro.Hostel.Application.Features.RoomType.Commands;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Processors;
using QIMSchoolProHostelService.Controllers.Base;
using Serilog;

namespace QIMSchoolProHostelService.Controllers
{
 
    public class BuildingController : BaseController
    {
     
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateBuilding.CreateBuildingCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IEnumerable<BuildingDto>> Gets()
        //{
        //    return await Mediator.Send(new GetBuildings.Query());
        //}

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<BuildingViewModel>> GetHostels()
        {
            return await Mediator.Send(new GetHostels.Query());
        }


        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> Test()
        {
            try
            {
                // Here you can implement any logic, for example, fetching data from a service
                int result = 34; // Example result
                return Ok(result); // This will return a 200 OK with the integer
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in Test action");
                return StatusCode(500, "Internal server error"); // Return 500 status on error
            }
        }

    }
}

