using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMeeting.Shared.Dto
{
   public class ConversationDto
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string Title { get; set; }
    }
}
