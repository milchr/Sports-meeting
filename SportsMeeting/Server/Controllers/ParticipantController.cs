using Microsoft.AspNetCore.Mvc;
using SportsMeeting.Server.Services;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportsMeeting.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        // GET: api/<ParticipantController>
        [HttpGet]
        public async Task<ActionResult<List<ParticipantDto>>> getParticipants()
        {
            return Ok(await _participantService.getAllParticipants());
        }

        // GET api/<ParticipantController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantDto>> getParticipant([FromRoute] int id)
        {
            return Ok(await _participantService.getParticipant(id));
        }

        // POST api/<ParticipantController>
        [HttpPost("new_participant")]
        public async Task<IActionResult> createParticiapnt([FromBody] CreateParticipantDto dto)
        {
            await _participantService.createParticipant(dto);
            return StatusCode(201);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> updateParticipant([FromRoute] int id, [FromBody] ParticipantDto dto)
        {
            await _participantService.updateParticipant(id, dto);
            return Ok();
        }

        // DELETE api/<ParticipantController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteParticipant([FromRoute] int id)
        {
            await _participantService.deleteParticipant(id);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> editParticiapnt([FromBody] ParticipantDto dto)
        {
            var participantId = dto.Id;
            await _participantService.updateParticipant(participantId,dto);
            return Ok();
        }

        [HttpGet("meeting/{id}")]
        public async Task<ActionResult<List<ParticipantDto>>> getAllParticipantsByMeeting([FromRoute] int id)
        {
            return Ok(await _participantService.getParticipantsByMeeting(id));
        }
    }
}
