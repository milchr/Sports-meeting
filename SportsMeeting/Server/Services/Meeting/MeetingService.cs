using AutoMapper;
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

        public IEnumerable<MeetingDto> getAllMeetings()
        {
            var meetings = _dbContext.Meetings.ToList();
            var meetingDto = _mapper.Map<List<MeetingDto>>(meetings);

            return meetingDto;
        }

        public async Task createMeeting(CreateMeetingDto dto)
        {
            var meeting = _mapper.Map<Meeting>(dto);
            _dbContext.Meetings.Add(meeting);
            _dbContext.SaveChanges();
        }
    }
}
