using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;
using System.Collections.Generic;
using System.Linq;
using QuizBeeApp.API.Helpers;
using stateEnums = QuizBeeApp.API.Helpers.Enum;

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

        public async Task<List<JudgeVerdict>> RequestForVerification(ParticipantAnswer participantAnswer, VerificationRequestDto request)
        {
            var evntJudges = await context.Events.Where(x => x.Id == request.EventId)
            .Include(x => x.Judges).FirstOrDefaultAsync();

            participantAnswer.RequestedForVerification = true;

            List<JudgeVerdict> verdicts = new List<JudgeVerdict>();
            evntJudges.Judges.ForEach(async judge =>{
                var verdict = new JudgeVerdict()
                {
                    Judge = judge,
                    Status =0,
                    ParticipantAnswer = participantAnswer,
                    Answer = request.Answer,
                    Remarks = request.Remarks

                };
                await context.AddAsync(verdict);
                verdicts.Add(verdict);
            });
            if(await context.SaveChangesAsync() < 1)
                throw new InvalidOperationException("verdict not saved");
            return verdicts;

        }

        public async Task<List<JudgeVerdict>> GetItemsToVerify(int judgeId)
        {
            return await context.JudgeVerdicts.FromSql("SELECT * FROM JudgeVerdicts WHERE JudgeId = {0} AND [Status] = 0",judgeId)
            .Include(x => x.ParticipantAnswer).ToListAsync();
        }

        public async Task<stateEnums.JudgesVerdict> VerifyAnswer(BaseJudgeVerdictDto verdict)
        {
            var defaultVerdict = await context.JudgeVerdicts.FirstOrDefaultAsync(x => x.Id == verdict.Id);
            if(defaultVerdict == null)
                throw new NullReferenceException("Cannot find judge verdict");
        
            defaultVerdict.Status = verdict.Status;

            if(await context.SaveChangesAsync() < 1)
                throw new InvalidOperationException("Unable to update the judges verdict");

            var pendingVerdicts = await GetItemsToVerifyForParticipansAnswer(verdict.ParticipantAnswer);

            if(pendingVerdicts.Where(x => x.Status == (int) stateEnums.JudgesVerdict.Pending).Any())
                return stateEnums.JudgesVerdict.Pending;
            else
            {
                var particpantsAnswer = await context.ParticipantAnswers
                .Where(x => x.Id == verdict.ParticipantAnswer)
                .Include(x => x.QuizItem)
                .Include(x => x.Participant).FirstOrDefaultAsync();
                
                particpantsAnswer.RequestedForVerification = false;

                await context.SaveChangesAsync();

                var correctCount = pendingVerdicts.Count(x => x.Status == (int) stateEnums.JudgesVerdict.Corrent);
                var wrongCount = pendingVerdicts.Count - correctCount;

                if (correctCount >= wrongCount)
                {
                    particpantsAnswer.IsCorrect = true;
                    particpantsAnswer.PointsEarned = particpantsAnswer.QuizItem.Point;
                    particpantsAnswer.Participant.TotalScores += particpantsAnswer.QuizItem.Point;
                    await context.SaveChangesAsync();
                    return stateEnums.JudgesVerdict.Corrent;
                }
                else
                    return stateEnums.JudgesVerdict.Wrong;
            } 
            
        }

        public async Task<List<JudgeVerdict>> GetItemsToVerify(int participantsAnswerId,bool getUntouched)
        {
            return await context.JudgeVerdicts.FromSql("SELECT * FROM JudgeVerdicts WHERE ParticipantAnswerId = {0} AND [Status] = {1}",participantsAnswerId,getUntouched ? 0 : 1).ToListAsync();

        }

        public async Task<List<JudgeVerdict>> GetItemsToVerifyForParticipansAnswer(int participantsAnswerId)
        {
            return await context.JudgeVerdicts.FromSql("SELECT * FROM JudgeVerdicts WHERE ParticipantAnswerId = {0} ",participantsAnswerId).ToListAsync();

        }
    }
}