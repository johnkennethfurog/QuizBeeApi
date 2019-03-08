using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using QuizBeeApp.Mobile.DependencyServices;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizBeeApp.Mobile.ViewModels
{
	public class RegistrationPageViewModel : ViewModelBase
	{
        private readonly IParticipantService participantService;
        private readonly IMessageService messageService;
        private readonly IPageDialogService pageDialogService;

        public RegistrationPageViewModel(INavigationService navigationService,
            IParticipantService participantService,
            IMessageService messageService,
            IPageDialogService pageDialogService) : base(navigationService)
        {
            this.participantService = participantService;
            this.messageService = messageService;
            this.pageDialogService = pageDialogService;
        }

        private string _schoolName;
        public string SchoolName
        {
            get { return _schoolName; }
            set { SetProperty(ref _schoolName, value); }
        }

        private string _eventCode;
        public string EventCode
        {
            get { return _eventCode; }
            set { SetProperty(ref _eventCode, value); }
        }

        private DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new DelegateCommand(ExecuteRegisterCommand));

        async void ExecuteRegisterCommand()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var participant = await participantService.RegisterAsync(new Participant
                {
                    EventCode = EventCode,
                    IsVerify = false,
                    Name = SchoolName,
                    ReferenceNumber = ""
                });
                await pageDialogService.DisplayAlertAsync(string.Empty, $"Your reference number is : {participant.ReferenceNumber}.{Environment.NewLine}Kindly take note of it","Okay");

                var param = new NavigationParameters
                {
                    {"participant",participant }
                };
                IsBusy = false;

                await NavigationService.NavigateAsync($"app:///NavigationPage/{nameof(Views.QuestionPage)}", param, animated: false);
            }
            catch(ParticipantServiceException ex)
            {
                messageService.ShowMessage(ex.ExceptionMessage);
                IsBusy = false;
            }
        }
    }
}
