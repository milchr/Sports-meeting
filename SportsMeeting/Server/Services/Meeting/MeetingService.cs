using Microsoft.Extensions.Logging;
using SportsMeeting.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MeetingService> _logger;

        public MeetingService(ApplicationDbContext dbContext, ILogger<MeetingService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
    }
}
