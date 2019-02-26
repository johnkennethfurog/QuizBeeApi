using System.Collections.Generic;

namespace QuizBeeApp.API.Dtos
{
    public class CreateQuizItemDto
    {
        public int TimeLimit { get; set; }   
        public int CategoryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
        public double Points { get; set; }
        public int EventId { get; set; }
        public List<string> Choices { get; set; }
    }
}