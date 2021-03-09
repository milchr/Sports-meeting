using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string Title { get; set; }

        public virtual Meeting Meeting {get; set;}
        public virtual List<Participant> Participant {get; set;}

        public virtual List<Message> Message { get; set; }
    }
}