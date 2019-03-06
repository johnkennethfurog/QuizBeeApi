using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using QuizBeeApp.API.Dtos;

namespace QuizBeeApp.API.SignalR
{
    public class StrongTypeHub : Hub<IBroadcastHub>
    {
        public async Task BroadcastQuestion(QuizItemDto quizItem){
            await Clients.All.ReceiveQuestion(quizItem);
        }

        public async Task StartTimer(){
            await Clients.All.StartTimer();
        }

        public async Task ShowAnswer(){
            await Clients.All.ShowAnswer();
        }
        
        public async Task CancelQuestion(){
            await Clients.All.CancelQuestion();
        }
    }
}