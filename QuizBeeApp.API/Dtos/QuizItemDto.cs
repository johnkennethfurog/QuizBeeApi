using System.Collections.Generic;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Dtos
{
    public class QuizItemDto
    {
        public int TimeLimit { get; set; }
        public CategoryDto Category { get; set; }
        public string  Question{ get; set; }
        public string Answer { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public double Point { get; set; }
        public BaseEventDto Event { get; set; }
        public List<string> QuestionChoices { get; set; }

    }
}