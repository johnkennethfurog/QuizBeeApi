using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;
using QuizBeeApp.API.Models;
using System.Collections.Generic;

namespace QuizBeeApp.API.Data
{
    public interface IJudgeRepository
    {
         Task<bool> VerifyJudge(int judgeId);    
        Task<Judge> RegisterJudgeAsync(CreateJudgeDto createJudgeDto,Event evnt,bool isVerify);
         Task<Judge> UpdateJudgeAsync(CreateJudgeDto createJudgeDto);
         Task<bool> DeleteJudgeAsync(int judgeId);
         Task<List<JudgeVerdict>> RequestForVerification(ParticipantAnswer participantAnswer, string answer,int eventId);
    }
}