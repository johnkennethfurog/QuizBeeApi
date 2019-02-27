using System.Threading.Tasks;

namespace QuizBeeApp.API.Data
{
    public interface IJudgeRepository
    {
         Task<bool> VerifyJudge(int judgeId);    
    }
}