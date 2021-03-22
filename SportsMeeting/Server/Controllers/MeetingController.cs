using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsMeeting.Server.Services;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SportsMeeting.Client.Pages;
using System.Net.Http;

namespace SportsMeeting.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]  
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;


        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MeetingDto>>> getMeetings()
        {
            return Ok(await _meetingService.getAllMeetings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDto>> getMeeting([FromRoute]int id)
        {
            return Ok(await _meetingService.getMeeting(id));
        }

        [HttpPost("new_meeting")]
        public async Task<IActionResult> createMeeting([FromBody] CreateMeetingDto dto)
        {
            string CurrentUser = User.Identity.Name;
            await _meetingService.createMeeting(dto, CurrentUser);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteMeeting([FromRoute] int id)
        {
            await _meetingService.deleteMeeting(id);
            return Ok();
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> updateMeeting([FromRoute] int id, [FromBody] MeetingDto dto)
        {
            await _meetingService.updateMeeting(id, dto);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task editMeeting([FromBody] MeetingDto dto)
        {
            var meetingId = dto.Id;
            await _meetingService.updateMeeting(meetingId, dto);
        }

        [HttpPost("room/{id}")]
        public async Task joinMeeting([FromRoute] int id, [FromBody] string userName)
        {
            await _meetingService.joinMeeting(id, userName);
        }
    }
}
