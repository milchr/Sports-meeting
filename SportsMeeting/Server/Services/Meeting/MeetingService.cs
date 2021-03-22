using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private DateTime localDate = DateTime.Now;

        public MeetingService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<MeetingService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            
        }

        public async Task<MeetingDto> getMeeting(int id)
        {
            var meeting = await _dbContext.Meetings.FirstOrDefaultAsync(m => m.Id == id);
            var meetingDto = _mapper.Map<MeetingDto>(meeting);

            return meetingDto;
        }

        public async Task<List<MeetingDto>> getAllMeetings()
        {
            var meetings = await _dbContext.Meetings.ToListAsync();
            List<Meeting> listOfAvailableMeetings = new List<Meeting>();
            foreach (var meeting in meetings)
            {
                if(Convert.ToInt32((meeting.Date - localDate).TotalDays) >= 0)
                {
                    listOfAvailableMeetings.Add(meeting);
                }
            }
            var meetingDto = _mapper.Map<List<MeetingDto>>(listOfAvailableMeetings);
            
            return meetingDto;
        }

        public async Task createMeeting(CreateMeetingDto dto, string user)
        {
            var meeting = _mapper.Map<Meeting>(dto);         
            var u = _dbContext.Users.FirstOrDefault(u => u.Email == user);
            meeting.UserName = u.Id;
            meeting.UserEmail = u.Email; 
            meeting.Category = _dbContext.Category.FirstOrDefault(c => c.Name == dto.categoryName);
            await _dbContext.Meetings.AddAsync(meeting);
            u.Meetings.Add(meeting);
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
