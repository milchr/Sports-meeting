using AutoMapper;
using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public class ParticipantMapper : Profile
    {
        public ParticipantMapper()
        {
            CreateMap<Participant, ParticipantDto>();
            CreateMap<CreateParticipantDto, Participant>();
        }
    }
}
