﻿using AutoMapper;
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
            var meetingDto = _mapper.Map<MeetingDto>(meeting);

            return meetingDto;
        }

        public async Task<List<MeetingDto>> getAllMeetings()
        {
            var meetings = await _dbContext.Meetings
                                    .Include(x=>x.Participants)
                                    .Include(x=>x.Category).ToListAsync();
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
            var result = await _dbContext.Meetings
                .FirstOrDefaultAsync(r => r.Id == Id);
            var result2 = await _dbContext.Participants
                .FirstOrDefaultAsync(m => m.MeetingId == Id && m.ConversationId != 0);
            if (result != null & result2 != null)
            {
                _dbContext.Participants.Remove(result2);
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

        public async Task joinMeeting(int meetingId, string userName)
        {
            var meeting = await _dbContext.Meetings.FirstOrDefaultAsync(m => m.Id == meetingId);
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
            var participants = meeting.Participants;
            var participantsDto = _mapper.Map<List<ParticipantDto>>(participants);
            return participantsDto;
        }

        public async Task<List<MeetingDto>> getAllMeetingsByParticipant(string userEmail)
        {
            var meetings = await _dbContext.Meetings.Include(x => x.Participants)
                                              .Include(x => x.Category)
                                              .ToListAsync();
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
    }
}
