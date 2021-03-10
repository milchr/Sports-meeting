using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;

namespace SportsMeeting.Server.Services
{
    public class MeetingMapper : Profile
    {
        public MeetingMapper()
        {
            CreateMap<Meeting, MeetingDto>();
            CreateMap<CreateMeetingDto, Meeting>();
        }
    }
}
