using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public string MessageText { get; set; }

        public virtual ApplicationUser AppUser { get; set; }
        public virtual Conversation Conversation { get; set; }
        
    }
}