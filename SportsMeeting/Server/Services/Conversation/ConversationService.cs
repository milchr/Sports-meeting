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

        public async Task deleteConversation(int Id)
        {
            var result = await _dbContext.Conversations
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _dbContext.Conversations.Remove(result);
                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<List<ConversationDto>> getAllConversations()
        {
            var conversationDto = await _dbContext.Conversations.ToListAsync();
            var conversationsDto = _mapper.Map<List<ConversationDto>>(conversationDto);
            return conversationsDto;
        }

        public async Task<ConversationDto> getConversation(int id)
        {
            var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.Id == id);
            var conversationDto = _mapper.Map<ConversationDto>(conversation);
            return conversationDto;
        }

        public async Task updateConversation(int id, ConversationDto conversation)
        {
            var result = await _dbContext.Conversations
              .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.Id = conversation.Id;
                result.MeetingId = conversation.MeetingId;
                result.Title = conversation.Title;
                _dbContext.Conversations.Update(result);
                await _dbContext.SaveChangesAsync();

            }

        }
    }
}
