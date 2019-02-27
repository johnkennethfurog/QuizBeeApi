using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
    }
}