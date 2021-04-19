using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMeeting.Shared.Dto
{
   public class ParticipantDto
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string UserId { get; set; }
        public int MeetingId { get; set; }
        public string UserEmail { get; set; }
    }
}
