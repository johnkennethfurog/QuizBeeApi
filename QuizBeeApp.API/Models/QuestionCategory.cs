using System.ComponentModel.DataAnnotations;

namespace QuizBeeApp.API.Models
{
    public class QuestionCategory
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int DefaultTimeLimit { get; set; }
    }
}