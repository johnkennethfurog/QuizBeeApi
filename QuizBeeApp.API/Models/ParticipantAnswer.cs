using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class ParticipantAnswer
    {
        [Key]
        public int Id { get; set; }
        public Participant Participant { get; set; }
        public QuizItem QuizItem { get; set; }
        public double PointsEarned { get; set; }
        public bool IsCorrect { get; set; }
        public List<JudgeVerdict> JudgeVerdicts { get; set; }
    }
}