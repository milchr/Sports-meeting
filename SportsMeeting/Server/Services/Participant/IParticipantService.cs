using SportsMeeting.Server.Models;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Services
{
    public interface IParticipantService
    {
        public Task<List<ParticipantDto>> getAllParticipants();
        public Task<ParticipantDto> getParticipant(int id);
        public Task createParticipant(CreateParticipantDto dto);
        public Task deleteParticipant(int Id);
        public Task updateParticipant(int id, ParticipantDto participant);
        public Task<Participant> getParticipantByUserEmail(string userEmail);

    }
}
