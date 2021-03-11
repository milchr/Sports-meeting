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
    public class ParticipantService : IParticipantService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ParticipantService> _logger;

        public ParticipantService(ApplicationDbContext dbContext, IMapper mapper, ILogger<ParticipantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task createParticipant(CreateParticipantDto dto)
        {
           var participant = _mapper.Map<Participant>(dto);
           await _dbContext.Participants.AddAsync(participant);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ParticipantDto>> getAllParticipants()
        {
            var participants = await _dbContext.Participants.ToListAsync();
            var particiapntDto = _mapper.Map<List<ParticipantDto>>(participants);
            return particiapntDto;
        }
    }
}
