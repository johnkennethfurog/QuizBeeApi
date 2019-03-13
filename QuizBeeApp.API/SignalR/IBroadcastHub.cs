using System.Collections.Generic;
using System.Threading.Tasks;
using QuizBeeApp.API.Dtos;

namespace QuizBeeApp.API.SignalR
{
    public interface IBroadcastHub
    {
         Task ReceiveQuestion(QuizItemDto quizItem);
         Task StartTimer();
         Task ShowAnswer();
         Task CancelQuestion();
         Task StartEvaluationPeriod();
         Task BroadcastVerification(List<JudgeVerdictDto> verdicts);
         Task VerificationEvent(bool isNewVerification);
         Task JudgesVerdict(int answerId,bool isCorrect);

         Task EndQuizBee();
    }
}