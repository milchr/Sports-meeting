using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMeeting.Shared.Dto
{
    public class CreateMeetingDto
    {
        [Required(ErrorMessage = "Wpisz tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Wpisz opis spotkania")]
        public string Description { get; set; }
        [Range(1,100, ErrorMessage = "Ustal limit osób")]
        public int PersonalLimit { get; set; }
        [Required(ErrorMessage = "Wpisz miejsce spotkania")]
        public string Place { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Wybierz kategorię")]
        public string CategoryName { get; set; }
    }
}

