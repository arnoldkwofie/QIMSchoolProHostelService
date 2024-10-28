using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QIMSchoolPro.Hostel.Application.Features.User.Queries;
using QIMSchoolPro.Hostel.Processors.Processors;
using QIMSchoolProHostelService.Controllers.Base;

namespace QIMSchoolProHostelService.Controllers
{
    public class UserController : BaseController
    {
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<UserViewModel> UserInfo()
        {

            return await Mediator.Send(new GetLoginUser.Query());
        }
    }
}
