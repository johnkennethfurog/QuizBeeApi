using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;

        public CategoryRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<QuestionCategory> CreateQuestionCategoryAsync(CreateQuestionCategoryDto QuestionCategory)
        {
             var category = new QuestionCategory{
                Description = QuestionCategory.Description,
                DefaultTimeLimit = QuestionCategory.TimeLimit
            };

            await context.QuestionCategories.AddAsync(category);
            if(await context.SaveChangesAsync() ==0)
                throw new InvalidOperationException();

            return category;
        }

        public async Task<bool> DeleteQuestionCategoryAsync(int QuestionCategoryId)
        {
            var category = await GetQuestionCategory(QuestionCategoryId);
            context.Remove(category);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<QuestionCategory> GetQuestionCategory(int QuestionCategoryId)
        {
            var category = await context.QuestionCategories.FirstOrDefaultAsync(x => x.Id == QuestionCategoryId);
            if(category == null)
                throw new NullReferenceException();
            return category;
        }

        public async Task<List<QuestionCategory>> GetQuestionCategorysAsync()
        {
            return await context.QuestionCategories.ToListAsync();
        }

        public async Task<bool> IsCategoryExist(string description)
        {
            return await context.QuestionCategories.AnyAsync(x => x.Description == description);
        }

        public async Task<QuestionCategory> UpdateQuestionCategoryAsync(int QuestionCategoryId, CreateQuestionCategoryDto QuestionCategory)
        {
             
            var category = await GetQuestionCategory(QuestionCategoryId);
            
            category.Description = QuestionCategory.Description;
            category.DefaultTimeLimit = QuestionCategory.TimeLimit;
            
            if(await context.SaveChangesAsync() ==0)
                throw new InvalidOperationException();

            return category;        
        }
    }
}