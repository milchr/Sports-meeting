using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsMeeting.Server.Data;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services.ApplicationUser
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public ApplicationUserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApplicationUserDto> getApplicationUser(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            ApplicationUserDto userDto = new ApplicationUserDto();
            userDto.Email = user.Email;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.Meetings = null;
            return userDto;
        }
    }
}
