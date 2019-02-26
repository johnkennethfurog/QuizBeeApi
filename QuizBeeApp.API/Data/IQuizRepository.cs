using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public interface IQuizRepository
    {
        Task<QuizItem> CreateQuizItemAsync(CreateQuizItemDto QuizItem,QuestionCategory Type,Event Event);
        Task<QuizItem> UpdateQuizItemAsync(CreateQuizItemDto QuizItem,QuestionCategory Type,Event Event);
        Task<bool> DeleteQuizItemAsync(int QuizItemId);
        Task<List<QuizItem>> GetQuizItemsAsync(int EventId);
        Task<List<QuizItem>> GetQuizItemsAsync(int EventId,string Category);
        Task<QuizItem> GetQuizItemAsync(int QuestionId);
        
        Task<QuestionChoice> SaveQuestionChoiceAsync(QuizItem QuizItem,string Answer);
        Task<bool> RemoveQuestionChoiceAsync(int questionId);
    }
}