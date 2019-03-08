using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuizBeeApp.Mobile.DependencyServices;
using QuizBeeApp.Mobile.Helpers;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizBeeApp.Mobile.ViewModels
{
	public class QuestionPageViewModel : ViewModelBase
	{
        private readonly IParticipantService participantService;
        private readonly IMessageService messageService;

        private Participant _loggedParticipant;
        private HubConnection _hubConnection;

        private QuestionItem _questionItem;
        public QuestionItem QuestionItem
        {
            get { return _questionItem; }
            set { SetProperty(ref _questionItem, value); }
        }

        private bool _isTrueOrFalse;
        public bool IsTrueOrFalse
        {
            get { return _isTrueOrFalse; }
            set { SetProperty(ref _isTrueOrFalse, value); }
        }

        private bool _isMultipleChoice;
        public bool IsMultipleChoice
        {
            get { return _isMultipleChoice; }
            set { SetProperty(ref _isMultipleChoice, value); }
        }

        private bool _isIdentification;
        public bool IsIdentification
        {
            get { return _isIdentification; }
            set { SetProperty(ref _isIdentification, value); }
        }

        private bool _showAnswer;
        public bool ShowAnswer
        {
            get { return _showAnswer; }
            set { SetProperty(ref _showAnswer, value); }
        }

        private QuestionType _questionType;

        public QuestionType QuestionType
        {
            get { return _questionType; }
            set {
                _questionType = value;
                SetQuestionType();
            }
        }

        private bool _isIdleMode = true;
        public bool IsIdleMode
        {
            get { return _isIdleMode; }
            set { SetProperty(ref _isIdleMode , value); }
        }

        public string Answer { get; set; }

        void SetQuestionType()
        {
            IsIdentification = false;
            IsMultipleChoice = false;
            IsTrueOrFalse = false;

            switch(QuestionType)
            {
                case QuestionType.Identification:
                    IsIdentification = true;
                    break;
                case QuestionType.MultipleChoice:
                    IsMultipleChoice = true;
                    break;
                case QuestionType.TrueOrFalse:
                    IsTrueOrFalse = true;
                    break;
            }
        }


        public QuestionPageViewModel(
            INavigationService navigationService,
            IParticipantService participantService,
            IMessageService messageService) : base(navigationService)
        {
            this.participantService = participantService;
            this.messageService = messageService;
            InitializeConnection();
        }

        async void InitializeConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(EndpointHelper.ROOT_URI + "/broadcast")
                .Build();

            _hubConnection.Closed += _hubConnection_Closed;
            
           await Connect();
        }

        private async Task _hubConnection_Closed(Exception arg)
        {

        }

        void RegisterToBroadcast()
        {
            _hubConnection.On<QuestionItem>("ReceiveQuestion", (question) =>
            {
                SetQuestion(question);
            });
        }

        void RegisterToCancelleation()
        {
            _hubConnection.On("CancelQuestion", () =>
            {
                Reset();
            });

        }

        void Reset()
        {
            IsTrueOrFalse = false;
            IsMultipleChoice = false;
            IsIdentification = false;
            ShowAnswer = false;

            QuestionItem = null;
            Answer = string.Empty;

            IsIdleMode = true;

        }

        void RegisterToTimerStart()
        {
            _hubConnection.On("StartTimer", () =>
            {
            });

        }

        void RetgisterToDispayingOfAnswer()
        {
            _hubConnection.On("ShowAnswer", () =>
            {
                ShowAnswer = true;
            });

        }

        async Task Connect()
        {
            try
            {
                RegisterToBroadcast();
                RegisterToCancelleation();
                RetgisterToDispayingOfAnswer();
                RegisterToTimerStart();

                await _hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                messageService.ShowMessage("Unable to connect to server, trying to reconnect ...");
                await Connect();
            }
        }

        void SetQuestion(QuestionItem question)
        {
            IsIdleMode = false;
            QuestionItem = question;

            QuestionType = (QuestionType)QuestionItem.Type;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            _loggedParticipant = parameters["participant"] as Participant;

        }

        public async override void Destroy()
        {
            base.Destroy();
            await _hubConnection.DisposeAsync();
        }

        private DelegateCommand<string> _answerSelectedCommand;
        public DelegateCommand<string> AnswerSelectedCommand =>
            _answerSelectedCommand ?? (_answerSelectedCommand = new DelegateCommand<string>(ExecuteAnswerSelectedCommand));

        void ExecuteAnswerSelectedCommand(string answer)
        {
            Answer = answer;
        }

        private DelegateCommand _submitQuestion;
        public DelegateCommand SubmitQuestion =>
            _submitQuestion ?? (_submitQuestion = new DelegateCommand(ExecuteSubmitQuestion));

        async void ExecuteSubmitQuestion()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var isSubmitted = await participantService.SubmitAnswerAsync(new PayloadAnswer
                {
                    Answer = Answer,
                    ParticipantId = _loggedParticipant.Id,
                    QuestionId = QuestionItem.Id
                });

                messageService.ShowMessage("Answer submitted");
                IsIdleMode = true;

            }
            catch (ParticipantServiceException ex)
            {
                messageService.ShowMessage(ex.ExceptionMessage);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
