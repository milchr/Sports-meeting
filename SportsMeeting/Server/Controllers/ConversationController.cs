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
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        // GET: api/<ConversationController>
        [HttpGet]
        public async Task<ActionResult<List<ConversationDto>>> getConversations()
        {
            return Ok(await _conversationService.getAllConversations());
        }

        // GET api/<ConversationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDto>> getConversation([FromRoute] int id)
        {
            return Ok(await _conversationService.getConversation(id));
        }

        // POST api/<ConversationController>
        [HttpPost("new_conversation")]
        public async Task<IActionResult> createParticiapnt([FromBody] CreateConversationDto dto)
        {
            await _conversationService.createConversation(dto);
            return StatusCode(201);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> updateConversation([FromRoute] int id, [FromBody] ConversationDto dto)
        {
            await _conversationService.updateConversation(id, dto);
            return Ok();
        }

        // DELETE api/<ConversationController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteConversation([FromRoute] int id)
        {
            await _conversationService.deleteConversation(id);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> editParticiapnt([FromBody] ConversationDto dto)
        {
            var conversationId = dto.Id;
            await _conversationService.updateConversation(conversationId, dto);
            return Ok();
        }
    }
}
