using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;

namespace SportsMeeting.Server.Services
{
    public class ApplicationUserMapper : Profile
    {
        public ApplicationUserMapper()
        {
            CreateMap<IdentityUser, ApplicationUserDto>();
            CreateMap<CreateApplicationUserDto, ApplicationUser>();
        } 
    }
}
