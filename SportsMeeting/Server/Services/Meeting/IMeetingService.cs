using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface IMeetingService
    {
        public Task<MeetingDto> getMeeting(int id);
        public Task<List<MeetingDto>> getAllMeetings();
        public Task createMeeting(CreateMeetingDto dto, string user);
        public Task deleteMeeting(int Id);
        public Task updateMeeting(int id, MeetingDto meeting);
        public Task<List<ParticipantDto>> getAllMeetingParticipants(int id);
        public Task joinMeeting(int meetingId, string userName);
        public Task<List<MeetingDto>> getAllMeetingsByParticipant(string userEmail);
        public Task<List<MeetingDto>> getAllMeetingsByCategory(string category);
    }
}