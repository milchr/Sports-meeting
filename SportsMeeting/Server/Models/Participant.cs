using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string UserId{ get; set; }
        public int MeetingId { get; set; }
        public string UserEmail { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual Meeting Meeting { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}