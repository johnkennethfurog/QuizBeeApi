using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;

namespace QuizBeeApp.API.Data
{
    public class JudgeRepository : IJudgeRepository
    {
        private readonly DataContext context;

        public JudgeRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<bool> VerifyJudge(int judgeId)
        {
            var judge = await context.Judges.FirstOrDefaultAsync(x => x.Id == judgeId);
            if(judge == null)
                throw new NullReferenceException("Judge does not exist");
            if(judge.IsVerify)
                throw new InvalidOperationException("Judge is already verified");
            judge.IsVerify = true;
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteJudgeAsync(int judgeId)
        {
            var judge = await GetJudge(judgeId);
            context.Judges.Remove(judge);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Judge> RegisterJudgeAsync(CreateJudgeDto createJudgeDto,Event evnt,bool isVerify)
        {
            var judge = new Judge
            {
                Name = createJudgeDto.Name,
                IsVerify = isVerify,
                Event = evnt,
                EmailAddress = createJudgeDto.EmailAddress
            };

            await context.AddAsync(judge);
            await context.SaveChangesAsync();

            return judge;
        }

        public async Task<Judge> UpdateJudgeAsync(CreateJudgeDto createJudgeDto)
        {
            var judge = await GetJudge(createJudgeDto.Id);
            judge.Name = createJudgeDto.Name;
            judge.EmailAddress = createJudgeDto.EmailAddress;
            await context.SaveChangesAsync();
            return judge;
        }

        public async Task<Judge>GetJudge(int judgeId)
        {
            return await context.Judges.FirstOrDefaultAsync(x => x.Id == judgeId);
        }
    }
}