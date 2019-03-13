using QuizBeeApp.Mobile.Helpers;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.Services
{
    class ParticipantService : IParticipantService
    {
        private readonly IRequestHandler requestHandler;

        public ParticipantService(IRequestHandler requestHandler)
        {
            this.requestHandler = requestHandler;
            this.requestHandler.Init(this);
        }
        public async Task<Participant> RegisterAsync(Participant participant)
        {
            return await requestHandler.PostAsync<Participant, Participant>(EndpointHelper.REGISTER,participant);
        }

        public async Task<bool> SendVerificationRequest(PayloadVerificataion verificataion)
        {
            return await requestHandler.PostAsync<bool, PayloadVerificataion>(EndpointHelper.VERIFY, verificataion);
        }

        public async Task<Participant> SignInAsync(PayloadSignIn payload)
        {
            return await requestHandler.PostAsync<Participant, PayloadSignIn>(EndpointHelper.SIGN_IN, payload);
        }

        public async Task<AnswerReturn> SubmitAnswerAsync(PayloadAnswer answer)
        {
            return await requestHandler.PostAsync<AnswerReturn, PayloadAnswer>(EndpointHelper.SUBMIT_ANSWER, answer);
        }
    }
}
