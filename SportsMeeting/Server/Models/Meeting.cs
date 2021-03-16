using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PersonalLimit { get; set; }
        public string Place { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public virtual Conversation Conversation { get; set; }
        public virtual List<Participant> Participant { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
