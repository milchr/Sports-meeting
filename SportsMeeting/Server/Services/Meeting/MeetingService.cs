using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportsMeeting.Server.Data;
using SportsMeeting.Server.Exceptions;
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
        private readonly IParticipantService _participantService;
        private readonly IConversationService _conversationService;
        private DateTime localDate = DateTime.Now;

        public MeetingService(ApplicationDbContext dbContext, ILogger<MeetingService> logger, IMapper mapper, IParticipantService participantService, IConversationService conversationService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _participantService = participantService;
            _conversationService = conversationService;
        }

        public async Task<MeetingDto> getMeeting(int id)
        {
            var meeting = await _dbContext.Meetings.FirstOrDefaultAsync(m => m.Id == id);

            if (meeting is null)
            {
                throw new NotFoundException("Meeting not found");
            }

            var meetingDto = _mapper.Map<MeetingDto>(meeting);

            return meetingDto;
        }

        public async Task<PageResult<MeetingDto>> getAllMeetings(MeetingQuery query)
        {
            var meetingsQuery = await _dbContext.Meetings
                                    .Include(x => x.Participants)
                                    .Include(x => x.Category)
                                    .Where(m => query.Category == null || (m.CategoryName.ToLower() == query.Category.ToLower())).ToListAsync();

            List<Meeting> listOfAvailableMeetings = new List<Meeting>();
            foreach (var meeting in meetingsQuery)
            {
                if (Convert.ToInt32((meeting.Date - localDate).TotalDays) >= 0)
                {
                    listOfAvailableMeetings.Add(meeting);
                }
            }
            var totalItems = listOfAvailableMeetings.Count();
            var meetings = listOfAvailableMeetings
                                 .OrderBy(m => m.Date)
                                 .Skip(query.Quantity * (query.Page - 1))
                                 .Take(query.Quantity)
                                 .ToList();
                                 
            var meetingDto = _mapper.Map<List<MeetingDto>>(meetings);
            
            var result = new PageResult<MeetingDto>(meetingDto, totalItems, query.Quantity, query.Page);

            return result;
        }

        public async Task createMeeting(CreateMeetingDto dto, string user)
        {
            var meeting = _mapper.Map<Meeting>(dto);         
            var u = _dbContext.Users.FirstOrDefault(u => u.Email == user);
            CreateConversationDto conversationDto = new CreateConversationDto();
            meeting.UserName = u.Id;
            meeting.UserEmail = u.Email; 
            meeting.Category = _dbContext.Category.FirstOrDefault(c => c.Name == dto.CategoryName);
            await _dbContext.Meetings.AddAsync(meeting);
            u.Meetings.Add(meeting);          
            await _dbContext.SaveChangesAsync();
            conversationDto.MeetingId = meeting.Id;
            conversationDto.Title = meeting.Title;
            await _conversationService.createConversation(conversationDto);

            var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.MeetingId == meeting.Id);
            CreateParticipantDto participantDto = new CreateParticipantDto();
            participantDto.ConversationId = conversation.Id;
            participantDto.UserId = u.Id;
            participantDto.MeetingId = meeting.Id;
            participantDto.UserEmail = u.Email;
            await _participantService.createParticipant(participantDto);
            var participant = await _participantService.getParticipantByUserEmail(u.Email);
            u.Participants.Add(participant);
            meeting.Participants.Add(participant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteMeeting(int Id)
        {
            var meeting = await _dbContext.Meetings
                .FirstOrDefaultAsync(m => m.Id == Id);
                
            if (meeting is null)
            {
                throw new NotFoundException("Meeting not found");
            }

            var participants = await _dbContext.Participants
                .Include(x=>x.Meeting)
                .Where(p => p.MeetingId == Id).ToListAsync();

            var conversation = await _dbContext.Conversations
                .FirstOrDefaultAsync(c => c.MeetingId == Id);

            if (participants != null && conversation != null)
            {
               _dbContext.Conversations.Remove(conversation);
                foreach(var participant in participants)
                {
                    _dbContext.Participants.Remove(participant);
                }
               _dbContext.Meetings.Remove(meeting);
               await _dbContext.SaveChangesAsync();
            }
        }

        public async Task updateMeeting(int id, MeetingDto meeting)
        {
            var result = await _dbContext.Meetings
               .FirstOrDefaultAsync(r => r.Id == id);

            if (result is null)
            {
                throw new NotFoundException("Meeting not found");
            }

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

        public async Task joinMeeting(int meetingId, string userName)
        {
            var meeting = await _dbContext.Meetings.FirstOrDefaultAsync(m => m.Id == meetingId);

            if (meeting is null)
            {
                throw new NotFoundException("Meeting not found");
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == userName);
            var conversation = await _dbContext.Conversations.FirstOrDefaultAsync(c => c.MeetingId == meeting.Id);
            CreateParticipantDto participantDto = new CreateParticipantDto();
            participantDto.ConversationId = conversation.Id;
            participantDto.UserId = user.Id;
            participantDto.MeetingId = meeting.Id;
            participantDto.UserEmail = user.Email;
            await _participantService.createParticipant(participantDto);
            var participant = await _participantService.getParticipantByUserEmail(user.Email);  
            user.Participants.Add(participant);
            meeting.Participants.Add(participant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ParticipantDto>> getAllMeetingParticipants(int id)
        {
            var meeting = await _dbContext.Meetings.FirstOrDefaultAsync(m => m.Id == id);
            
            if (meeting is null)
            {
                throw new NotFoundException("Meeting not found");
            }

            var participants = meeting.Participants;
            var participantsDto = _mapper.Map<List<ParticipantDto>>(participants);
            return participantsDto;
        }

        public async Task<List<MeetingDto>> getAllMeetingsByParticipant(string userEmail)
        {
            var meetings = await _dbContext.Meetings.Include(x => x.Participants)
                                              .Include(x => x.Category)
                                              .ToListAsync();

            if (meetings is null)
            {
                throw new NotFoundException("Meetings not found");
            }

            List<Meeting> userMeetings = new List<Meeting>();

            foreach (var m in meetings)
            {
                    if(m.Participants.Exists(p => p.UserEmail == userEmail))
                    {
                        if (Convert.ToInt32((m.Date - localDate).TotalDays) >= 0)
                        {
                            userMeetings.Add(m);
                        }
                    }           
            }
            var meetingsDto = _mapper.Map<List<MeetingDto>>(userMeetings);
            return meetingsDto;
        }

        public async Task<List<MeetingDto>> getAllMeetingsByCategory(string category)
        {
            var meetings = await _dbContext.Meetings
                                                .Where(m => m.CategoryName.ToLower() == category.ToLower())
                                                .Include(x => x.Participants)
                                                .Include(x => x.Category)
                                                .ToListAsync();
            if (meetings is null)
            {
                throw new NotFoundException("Meetings not found");
            }

            List<Meeting> meetingsByCategory = new List<Meeting>();

            foreach (var m in meetings)
            {
                if (Convert.ToInt32((m.Date - localDate).TotalDays) >= 0)
                {
                   meetingsByCategory.Add(m);
                }
            }
            var meetingsDto = _mapper.Map<List<MeetingDto>>(meetingsByCategory);
            return meetingsDto;
        }
    }
}
