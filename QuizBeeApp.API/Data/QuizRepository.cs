using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Helpers;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DataContext context;

        public QuizRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<QuizItem> CreateQuizItemAsync(CreateQuizItemDto QuizItem,QuestionCategory Category,Event Event)
        {
            var quizItem = new QuizItem()
            {
                TimeLimit = QuizItem.TimeLimit,
                Category = Category,
                Question = QuizItem.Question,
                Answer = QuizItem.Answer,
                Point = QuizItem.Points,
                Event = Event,
                Type = QuizItem.Type
            };

            await context.QuizItems.AddAsync(quizItem);
            if(await context.SaveChangesAsync() > 0)
            {
                if( quizItem.Type == (int)Helpers.Enum.QuestionType.MultipleChoice)
                    await SaveChoices(QuizItem,quizItem);

                return quizItem;

            }
            else
                throw new InvalidOperationException("unable to save quiz item");
            
        }

        private async Task SaveChoices(CreateQuizItemDto QuizItem,QuizItem quizItem)
        {
            if(QuizItem.QuestionChoices == null || QuizItem.QuestionChoices.Count ==0)
                await ReverseSavingOfQuiz(quizItem);
                
            QuizItem.QuestionChoices.ForEach(async choice =>
            {
                await SaveQuestionChoiceAsync(quizItem,choice);
            });

            if(await context.SaveChangesAsync() == 0)
                await ReverseSavingOfQuiz(quizItem);
        }

        private async Task ReverseSavingOfQuiz(QuizItem quizItem)
        {
            context.QuizItems.Remove(quizItem);
            await context.SaveChangesAsync();
            throw new InvalidOperationException("Unable to save choices");
        }

        public async Task<bool> DeleteQuizItemAsync(int QuizItemId)
        {
            var quizItem = await context.QuizItems.FirstOrDefaultAsync(x => x.Id == QuizItemId);
            if(quizItem == null)
                return false;

            context.QuizItems.Remove(quizItem);
            return await context.SaveChangesAsync() > 0;        
        }

        public async Task<QuizItem> GetQuizItemAsync(int QuestionId)
        {
            return await context.QuizItems.Where(x => x.Id == QuestionId)
            .Include(x => x.Event)
            .Include(x => x.QuestionChoices)
            .Include(x => x.Category)
            .FirstOrDefaultAsync();
        }

        public async Task<List<QuizItem>> GetQuizItemsAsync(int EventId)
        {
            return await context.QuizItems.FromSql("SELECT * FROM QUIZITEMS WHERE EVENTID = {0}",EventId)
            .Include(x => x.Event)
            .Include(x => x.QuestionChoices)
            .Include(x => x.Category).ToListAsync();
        }

        public async Task<List<QuizItem>> GetQuizItemsAsync(int EventId, int CategoryId)
        {
            return await context.QuizItems.FromSql("SELECT * FROM QUIZITEMS WHERE EVENTID = {0} AND CATEGORYID = {1}",EventId,CategoryId)
            .Include(x => x.Event)
            .Include(x => x.QuestionChoices)
            .Include(x => x.Category).ToListAsync();
        }

        public async Task<bool> RemoveQuestionChoiceAsync(int questionId)
        {
            var choices = await context.QuestionChoices.FromSql("SELECT * FROM QuestionChoices WHERE QuizItemId = {0}",questionId).ToListAsync();
            context.QuestionChoices.RemoveRange(choices);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<QuestionChoice> SaveQuestionChoiceAsync(QuizItem QuizItem, string Answer)
        {
            var choice = new QuestionChoice
            {
                QuizItem = QuizItem,
                Choice = Answer
            };

            await context.QuestionChoices.AddAsync(choice);
            return choice;
        }

        public async Task<QuizItem> UpdateQuizItemAsync(int QuizId,CreateQuizItemDto QuizItemDto, QuestionCategory Category, Event Event)
        {
            var quizItem = await GetQuizItemAsync(QuizId);
            quizItem.Category = Category;
            quizItem.Event = Event;
            quizItem.Answer = QuizItemDto.Answer;
            quizItem.Point = QuizItemDto.Points;
            quizItem.Question = QuizItemDto.Question;
            quizItem.TimeLimit = QuizItemDto.TimeLimit;
            quizItem.Type = QuizItemDto.Type;

             if(await context.SaveChangesAsync() > 0)
            {
                if( quizItem.Type == (int)Helpers.Enum.QuestionType.MultipleChoice)
                {
                    await RemoveQuestionChoiceAsync(quizItem.Id);
                    await SaveChoices(QuizItemDto,quizItem);
                }
                return quizItem;
            }
            else
                throw new InvalidOperationException("unable to save quiz item");
        }
    }
}