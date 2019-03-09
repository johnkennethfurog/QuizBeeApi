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
    }
}