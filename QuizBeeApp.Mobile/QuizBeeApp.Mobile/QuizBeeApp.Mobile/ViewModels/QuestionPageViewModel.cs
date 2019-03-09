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
using Xamarin.Forms;

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

        private QuestionType _questionType;

        public QuestionType QuestionType
        {
            get { return _questionType; }
            set {
                _questionType = value;
                SetQuestionType();
            }
        }

        private string _time;
        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        private bool _isIdleMode = true;
        public bool IsIdleMode
        {
            get { return _isIdleMode; }
            set { SetProperty(ref _isIdleMode , value); }
        }

        private bool _isAnswerSubmitted;
        public bool IsAnswerSubmitted
        {
            get { return _isAnswerSubmitted; }
            set { SetProperty(ref _isAnswerSubmitted, value); }
        }

        private string _answer;
        public string Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value); }
        }

        private bool _isCorrectAnswer;
        public bool IsCorrectAnswer
        {
            get { return _isCorrectAnswer; }
            set { SetProperty(ref _isCorrectAnswer, value); }
        }

        private bool _isTimerStarted;
        public bool IsTimerStarted
        {
            get { return _isTimerStarted; }
            set { SetProperty(ref _isTimerStarted, value); }
        }

        private bool _isAnswerDisplayed;
        public bool IsAnswerDisplayed
        {
            get { return _isAnswerDisplayed; }
            set { SetProperty(ref _isAnswerDisplayed, value); }
        }

        private int timerSeconds;
        private AnswerReturn answer;
        private bool isDisposing;

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

            RegisterToBroadcast();
            RegisterToCancelleation();
            RetgisterToDispayingOfAnswer();
            RegisterToTimerStart();

            await Connect();
        }

        private async Task _hubConnection_Closed(Exception arg)
        {
            if (!isDisposing)
                await Connect();
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
            answer = null;

            IsTrueOrFalse = false;
            IsMultipleChoice = false;
            IsIdentification = false;

            QuestionItem = null;
            Answer = string.Empty;

            IsIdleMode = true;
            IsTimerStarted = false;
            IsAnswerSubmitted = false;
            IsAnswerDisplayed = false;

        }

        void RegisterToTimerStart()
        {
            _hubConnection.On("StartTimer", () =>
            {
                IsTimerStarted = true;
                StartTimer();
            });

        }

        void RetgisterToDispayingOfAnswer()
        {
            _hubConnection.On("ShowAnswer", () =>
            {
                IsAnswerDisplayed = true;
                IsAnswerCorrect();
            });

        }

        bool IsAnswerCorrect()
        {
            IsCorrectAnswer =  Answer.ToLower() == QuestionItem.Answer.ToLower();
            return IsCorrectAnswer;
        }

        async Task Connect()
        {
            try
            {

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
            timerSeconds = QuestionItem.TimeLimit;
            SetSecondsToTimeFormat();

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
            isDisposing = true;
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

            if(string.IsNullOrWhiteSpace(Answer))
            {
                messageService.ShowMessage("You need to answer");
                return;
            }
            try
            {
                IsBusy = true;
                var answerSubmitted = await participantService.SubmitAnswerAsync(new PayloadAnswer
                {
                    Answer = Answer,
                    ParticipantId = _loggedParticipant.Id,
                    QuestionId = QuestionItem.Id
                });

                answer = answerSubmitted;
                messageService.ShowMessage("Answer submitted");
                IsAnswerSubmitted = true;

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

        void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // Do something
                timerSeconds--;
                SetSecondsToTimeFormat();

                if (timerSeconds == 0)
                    SubmitQuestion.Execute();

                return timerSeconds > 0 && !IsIdleMode && !IsAnswerSubmitted; // True = Repeat again, False = Stop the timer
            });
        }

        void SetSecondsToTimeFormat()
        {
            var time = TimeSpan.FromSeconds(timerSeconds);
            Time = $"{time.Minutes.ToString("00")}:{time.Seconds.ToString("00")}";

        }

        private DelegateCommand _requestEvaluationCommand;
        public DelegateCommand RequestEvaluationCommand =>
            _requestEvaluationCommand ?? (_requestEvaluationCommand = new DelegateCommand(ExecuteRequestEvaluationCommand));

        void ExecuteRequestEvaluationCommand()
        {

        }
    }
}
