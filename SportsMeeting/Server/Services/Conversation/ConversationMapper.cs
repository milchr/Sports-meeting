using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportsMeeting.Shared.Dto;
using SportsMeeting.Server.Models;

namespace SportsMeeting.Server.Services
{
    public class ConversationMapper : Profile
    {
        public ConversationMapper()
        {
            CreateMap<Conversation, ConversationDto>();
            CreateMap<CreateConversationDto, Conversation>();
        }
    }
}
