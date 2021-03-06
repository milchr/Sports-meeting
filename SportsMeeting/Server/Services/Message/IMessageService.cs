using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface IMessageService
    {
        public Task<List<MessageDto>> getAllMessages();
        public Task<MessageDto> getMessage(int id);
        public Task createMessage(CreateMessageDto dto);
        public Task deleteMessage(int Id);
        public Task updateMessage(int id, MessageDto message);

    }
}
