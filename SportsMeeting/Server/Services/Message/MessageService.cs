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
    public class MessageService : IMessageService
    {
        ApplicationDbContext _dbContext;
        IMapper _mapper;
        ILogger<MessageService> _logger;

        public MessageService(ApplicationDbContext dbContext, IMapper mapper, ILogger<MessageService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task createMessage(CreateMessageDto dto)
        {
            var message = _mapper.Map<Message>(dto);
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteMessage(int Id)
        {
            var result = await _dbContext.Messages
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _dbContext.Messages.Remove(result);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<List<MessageDto>> getAllMessages()
        {
            var messages = await _dbContext.Messages.ToListAsync();
            var messageDto = _mapper.Map<List<MessageDto>>(messages);
            return messageDto;
        }

        public async Task updateMessage(int id, MessageDto message)
        {
            var result = await _dbContext.Messages
                .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.ConversationId = message.ConversationId;
                result.UserId = message.UserId;
                result.MessageText = message.MessageText;
                _dbContext.Messages.Update(result);
                await _dbContext.SaveChangesAsync();

            }

        }

        public async Task<MessageDto> getMessage(int id)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
            var messageDto = _mapper.Map<MessageDto>(message);

            return messageDto;
        }
    }
}
