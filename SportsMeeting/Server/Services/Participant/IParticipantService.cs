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
        public Task createParticipant(CreateParticipantDto dto);
    }
}
