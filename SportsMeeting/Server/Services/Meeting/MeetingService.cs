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
    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MeetingService> _logger;

        public MeetingService(ApplicationDbContext dbContext, ILogger<MeetingService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<MeetingDto>> getAllMeetings()
        {
            var meetings = await _dbContext.Meetings.ToListAsync();
            var meetingDto = _mapper.Map<List<MeetingDto>>(meetings);
            
            return meetingDto;
        }
        public async Task createMeeting(CreateMeetingDto dto)
        {
            var meeting = _mapper.Map<Meeting>(dto);
            await _dbContext.Meetings.AddAsync(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Meeting> deleteMeeting(int Id)
        {
            var result = await _dbContext.Meetings
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (result != null)
            {
                _dbContext.Meetings.Remove(result);
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Meeting> updateMeeting(Meeting meeting)
        {
            var result = await _dbContext.Meetings
               .FirstOrDefaultAsync(e => e.Id == meeting.Id);

            if (result != null)
            {
                result.Title = meeting.Title;
                result.Description = meeting.Description;
                result.Place = meeting.Place;
                result.UserId = meeting.UserId;
                result.PersonalLimit = meeting.PersonalLimit;
                result.Conversation = meeting.Conversation;
                result.Participant = meeting.Participant;
                result.Category = meeting.Category;

                await _dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
