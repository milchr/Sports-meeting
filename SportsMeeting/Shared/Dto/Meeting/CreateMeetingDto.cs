﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMeeting.Shared.Dto
{
    public class CreateMeetingDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PersonalLimit { get; set; }
        public string Place { get; set; }
    }
}
