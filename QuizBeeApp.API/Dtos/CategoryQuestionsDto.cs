using System.Collections.Generic;

namespace QuizBeeApp.API.Dtos
{
    public class CategoryQuestionsDto
    {
        public List<QuizItemDto> Questions { get; set; }
        public CategoryDto Category { get; set; }
   
    }
}