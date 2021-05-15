using Microsoft.AspNetCore.Mvc;
using SportsMeeting.Server.Services;
using SportsMeeting.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SportsMeeting.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ILogger<MeetingController> _logger;


        public MeetingController(IMeetingService meetingService, ILogger<MeetingController> logger)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MeetingDto>>> getMeetings([FromQuery] MeetingQuery query)
        {
            var meetings = await _meetingService.getAllMeetings(query);   
            return Ok(meetings.Items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingDto>> getMeeting([FromRoute]int id)
        {
            var meeting = await _meetingService.getMeeting(id);

            if(meeting is null)
            {
                return NotFound(meeting);
            }
            return Ok(meeting);
        }

        [HttpPost("new_meeting")]
        public async Task<IActionResult> createMeeting([FromBody] CreateMeetingDto dto)
        {
            string CurrentUser = User.Identity.Name;
            await _meetingService.createMeeting(dto, CurrentUser);
            return StatusCode(201);
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

        [HttpGet("edit/{id}")]
        public async Task<ActionResult<MeetingDto>> getMeetingToEdit([FromRoute] int id)
        {
            var meeting = await _meetingService.getMeeting(id);

            if (meeting is null)
            {
                return NotFound(meeting);
            }
            return Ok(meeting);
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

        [HttpGet("user_meetings")]
        public async Task<ActionResult<List<MeetingDto>>> getUserMeetings()
        {
            string userEmail = User.Identity.Name;
            return Ok(await _meetingService.getAllMeetingsByParticipant(userEmail));
        }
    }
}
