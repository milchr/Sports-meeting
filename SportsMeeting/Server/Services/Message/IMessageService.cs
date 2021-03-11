using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface IMessageService
    {
        public Task<List<ConversationDto>> getAllMessages();
        public Task createMessage(CreateMessageDto dto);
    }
}
