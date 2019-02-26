using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Event Event { get; set; }
        public double TotalScores { get; set; }
        public List<ParticipantAnswer> ParticipantAnswers { get; set; }
    }
}