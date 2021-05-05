using Microsoft.AspNetCore.Mvc;
using SportsMeeting.Server.Services;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _applicationUserService;
        public UserController(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationUserDto>> getApplicationUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(await _applicationUserService.getApplicationUser(User.Identity.Name));
            }
            return BadRequest();
        }
    }
}
