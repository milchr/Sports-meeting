using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    interface IApplicationUserService
    {
        public Task<List<ApplicationUserDto>> getAllApplicationUsers();
        public Task createApplicationUser(CreateApplicationUserDto dto);
    }
}
