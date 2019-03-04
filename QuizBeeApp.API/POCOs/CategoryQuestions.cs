using System.Collections.Generic;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.POCOs
{
    public class CategoryQuestions
    {
        public List<QuizItem> Questions { get; set; }
        public QuestionCategory Category { get; set; }
    }
}