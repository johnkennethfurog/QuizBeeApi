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
         Task<List<JudgeVerdict>> RequestForVerification(ParticipantAnswer participantAnswer, VerificationRequestDto request);
         Task<List<JudgeVerdict>> GetItemsToVerify(int judgeId);
         Task<List<JudgeVerdict>> GetItemsToVerify(int participantsAnswerId,bool getUntouched);
         Task<List<JudgeVerdict>> GetItemsToVerifyForParticipansAnswer(int participantsAnswerId);
         Task<Helpers.Enum.JudgesVerdict> VerifyAnswer(BaseJudgeVerdictDto verdict);
    }
}