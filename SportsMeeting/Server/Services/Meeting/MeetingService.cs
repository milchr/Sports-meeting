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
        public async Task createMeeting(CreateMeetingDto dto, string user)
        {
            var meeting = _mapper.Map<Meeting>(dto);
            var u = _dbContext.Users.FirstOrDefault(u => u.Email == user);
            u.Meeting = meeting;
            meeting.Category = _dbContext.Category.FirstOrDefault(c => c.Name == dto.categoryName);
            await _dbContext.Meetings.AddAsync(meeting);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteMeeting(int Id)
        {
            var result = await _dbContext.Meetings
                .FirstOrDefaultAsync(r => r.Id == Id);
            if (result != null)
            {
                _dbContext.Meetings.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task updateMeeting(int id, MeetingDto meeting)
        {
            var result = await _dbContext.Meetings
               .FirstOrDefaultAsync(r => r.Id == id);

            if (result != null)
            {
                result.Title = meeting.Title;
                result.Description = meeting.Description;
                result.Place = meeting.Place;
                result.PersonalLimit = meeting.PersonalLimit;

                _dbContext.Meetings.Update(result);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
