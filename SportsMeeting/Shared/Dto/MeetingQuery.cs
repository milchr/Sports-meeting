using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMeeting.Shared.Dto
{
    public class MeetingQuery
    {
        public string Category { get; set; }
        public int Page { get; set; }
        public int Quantity { get; set; }
    }
}
