using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface ICategoryRepository
    {
        Task<QuestionCategory> CreateQuestionCategoryAsync(CreateQuestionCategoryDto QuestionCategory);
        Task<QuestionCategory> UpdateQuestionCategoryAsync(int QuestionCategoryId,CreateQuestionCategoryDto QuestionCategory);
        Task<bool> DeleteQuestionCategoryAsync(int QuestionCategoryId);
        Task<List<QuestionCategory>> GetQuestionCategorysAsync();
        Task<QuestionCategory> GetQuestionCategory(int QuestionCategoryId);
        Task<bool> IsCategoryExist(string description);
    }
}