using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class QuestionChoice
    {
        [Key]
        public int Id { get; set; }
        public string Choice { get; set; }
        public QuizItem QuizItem { get; set; }
    
    }
}