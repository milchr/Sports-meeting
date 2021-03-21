using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface IConversationService
    {
        public Task<List<ConversationDto>> getAllConversations();
        public Task<ConversationDto> getConversation(int id);
        public Task createConversation(CreateConversationDto dto);
        public Task deleteConversation(int Id);
        public Task updateConversation(int id, ConversationDto conversation);


    }
}
