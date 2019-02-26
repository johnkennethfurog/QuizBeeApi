using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class QuizItem
    {
        public int TimeLimit { get; set; }
        public QuestionCategory Category { get; set; }
        public string  Question{ get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
        [Key]
        public int Id { get; set; }

        public double Point { get; set; }
        public Event Event { get; set; }
        public List<QuestionChoice> QuestionChoices { get; set; }
        public List<ParticipantAnswer> ParticipantAnswers { get; set; }
    }
}