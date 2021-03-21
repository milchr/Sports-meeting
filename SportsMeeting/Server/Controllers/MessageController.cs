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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _MessageService;

        public MessageController(IMessageService MessageService)
        {
            _MessageService = MessageService;
        }

        // GET: api/<MessageController>
        [HttpGet]
        public async Task<ActionResult<List<MessageDto>>> getMessages()
        {
            return Ok(await _MessageService.getAllMessages());
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDto>> getMessage([FromRoute] int id)
        {
            return Ok(await _MessageService.getMessage(id));
        }

        // POST api/<MessageController>
        [HttpPost("new_Message")]
        public async Task<IActionResult> createMessage([FromBody] CreateMessageDto dto)
        {
            await _MessageService.createMessage(dto);
            return StatusCode(201);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> updateMessage([FromRoute] int id, [FromBody] MessageDto dto)
        {
            await _MessageService.updateMessage(id, dto);
            return Ok();
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteMessage([FromRoute] int id)
        {
            await _MessageService.deleteMessage(id);
            return Ok();
        }

        [HttpPost("edit")]
        public async Task<IActionResult> editMessage([FromBody] MessageDto dto)
        {
            var MessageId = dto.Id;
            await _MessageService.updateMessage(MessageId, dto);
            return Ok();
        }
    }
}
