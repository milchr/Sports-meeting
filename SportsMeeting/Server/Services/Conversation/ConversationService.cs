using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsMeeting.Server.Data;
using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public class ConversationService : IConversationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ConversationService> _logger;
        public ConversationService(ApplicationDbContext dbContext, ILogger<ConversationService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task createConversation(CreateConversationDto dto)
        {
            var conversation = _mapper.Map<Conversation>(dto);
            await _dbContext.Conversations.AddAsync(conversation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Conversation> deleteConversation(int Id)
        {
            var result = await _dbContext.Conversations
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _dbContext.Conversations.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<List<ConversationDto>> getAllConversations()
        {
            var conversationDto = await _dbContext.Conversations.ToListAsync();
            var conversationsDto = _mapper.Map<List<ConversationDto>>(conversationDto);
            return conversationsDto;
        }

        public async Task<Conversation> updateConversation(Conversation conversation)
        {
            var result = await _dbContext.Conversations
              .FirstOrDefaultAsync(e => e.Id == conversation.Id);

            if (result != null)
            {
                result.Meeting = conversation.Meeting;
                result.MeetingId = conversation.MeetingId;
                result.Message = conversation.Message;
                result.Participant = conversation.Participant;
                result.Title = conversation.Title;

                await _dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
