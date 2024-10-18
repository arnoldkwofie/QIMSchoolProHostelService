using MediatR;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Booking.Commands;
using QIMSchoolPro.Hostel.Application.Features.Building.Queries;
using QIMSchoolPro.Hostel.Application.Features.Floor.Commands;
using QIMSchoolPro.Hostel.Application.Features.Room.Commands;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Processors;
using QIMSchoolProHostelService.Controllers.Base;


namespace QIMSchoolProHostelService.Controllers
{
    public class BookingController : BaseController
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ResponseData> Create([FromBody] CreateBooking.CreateBookingCommand command)
        {
            return await Mediator.Send(command);
        }

    }
}
