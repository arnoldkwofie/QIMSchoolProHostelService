using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.Building.Queries;
using QIMSchoolPro.Hostel.Application.Features.Floor.Commands;
using QIMSchoolPro.Hostel.Application.Features.Payment.Commands;
using QIMSchoolPro.Hostel.Application.Features.Payment.Queries;
using QIMSchoolPro.Hostel.Application.Features.Room.Commands;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolProHostelService.Controllers.Base;


namespace QIMSchoolProHostelService.Controllers
{
    public class PaymentController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreatePayment.CreatePaymentCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<PaymentDto>> GetPaymentByPartyId()
        {
            return await Mediator.Send(new GetPaymentByPartyId.Query());
        }


    }
}
