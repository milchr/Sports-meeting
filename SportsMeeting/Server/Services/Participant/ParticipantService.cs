using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsMeeting.Server.Data;
using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System.Collections.Generic;
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

        public async Task<ParticipantDto> getParticipant(int id)
        {
            var participant = await _dbContext.Participants.FirstOrDefaultAsync(p => p.Id == id);
            var participantDto = _mapper.Map<ParticipantDto>(participant);

            return participantDto;
        }
        public async Task<Participant> getParticipantByUserEmail(string userEmail)
        {
            var participant = await _dbContext.Participants.FirstOrDefaultAsync(p => p.UserEmail == userEmail);
            return participant;
        }

        public async Task<List<ParticipantDto>> getAllParticipants()
        {
            var participants = await _dbContext.Participants.ToListAsync();
            var particiapntDto = _mapper.Map<List<ParticipantDto>>(participants);
            return particiapntDto;
        }

        public async Task deleteParticipant(int Id)
        {
            var result = await _dbContext.Participants
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _dbContext.Participants.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task updateParticipant(int id, ParticipantDto participant)
        {
            var result = await _dbContext.Participants
                .FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                result.ConversationId = participant.ConversationId;
                result.UserEmail = participant.UserEmail;

                _dbContext.Participants.Update(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ParticipantDto>> getParticipantsByMeeting(int meetingId)
        {
            var meeting = await _dbContext.Meetings
                .Include(x => x.Participants)
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == meetingId);
            List<ParticipantDto> participants = new List<ParticipantDto>();
            foreach(var p in meeting.Participants)
            {
                ParticipantDto participantDto = new ParticipantDto();
                participantDto.Id = p.Id;
                participantDto.ConversationId = p.ConversationId;
                participantDto.UserId = p.UserId;
                participantDto.MeetingId = p.MeetingId;
                participantDto.UserEmail = p.UserEmail;
                participantDto.FirstName = p.User.FirstName;
                participantDto.LastName = p.User.LastName;

                participants.Add(participantDto);
            }
            return participants;
        }
    }
}