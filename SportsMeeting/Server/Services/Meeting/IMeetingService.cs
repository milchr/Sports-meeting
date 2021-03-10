using SportsMeeting.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface IMeetingService
    {
        public Task<List<MeetingDto>> getAllMeetings();
        public Task createMeeting(CreateMeetingDto dto);
    }
}