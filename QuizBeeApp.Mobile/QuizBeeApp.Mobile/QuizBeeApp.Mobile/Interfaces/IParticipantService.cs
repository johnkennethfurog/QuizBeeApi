using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.Interfaces
{
    public interface IParticipantService
    {
        Task<Participant> RegisterAsync(Participant participant);
        Task<AnswerReturn> SubmitAnswerAsync(PayloadAnswer answer);
        Task<Participant> SignInAsync(PayloadSignIn participant);
    }
}
