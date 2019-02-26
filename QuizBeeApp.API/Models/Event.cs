using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
    
        public string Name { get; set; }
    
        public string Code { get; set; }

        public List<QuizItem> QuezItems { get; set; }
        public List<Participant> Participants { get; set; }
        public List<Judge> Judges { get; set; }
    }
}