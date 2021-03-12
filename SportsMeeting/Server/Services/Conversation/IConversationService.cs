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
        public Task createConversation(CreateConversationDto dto);
        public Task<Conversation> deleteConversation(int Id);


    }
}
