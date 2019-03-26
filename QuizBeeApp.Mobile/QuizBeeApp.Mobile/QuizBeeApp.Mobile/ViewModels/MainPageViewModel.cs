using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuizBeeApp.Mobile.DependencyServices;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuizBeeApp.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IParticipantService participantService;
        private readonly IMessageService messageService;

        public MainPageViewModel(INavigationService navigationService,
            IParticipantService participantService,
            IMessageService messageService
            )
            : base(navigationService)
        {
            Title = "Main Page";
            this.participantService = participantService;
            this.messageService = messageService;
        }

        private string _eventCode;
        public string EventCode
        {
            get { return _eventCode; }
            set { SetProperty(ref _eventCode, value); }
        }

        private string _referenceCode;
        public string ReferenceCode
        {
            get { return _referenceCode; }
            set { SetProperty(ref _referenceCode, value); }
        }

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand));

        async void ExecuteSignInCommand()
        {
            try
            {
                IsBusy = true;
                var participant = await participantService.SignInAsync(new PayloadSignIn
                {
                    EventCode = EventCode,
                    ReferenceNumber = ReferenceCode
                });

                if (participant == null)
                    messageService.ShowMessage("Unable to login");

                var param = new NavigationParameters
                {
                    {"participant",participant }
                };

                await NavigationService.NavigateAsync($"app:///NavigationPage/{nameof(Views.QuestionPage)}",param, animated: false);
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
