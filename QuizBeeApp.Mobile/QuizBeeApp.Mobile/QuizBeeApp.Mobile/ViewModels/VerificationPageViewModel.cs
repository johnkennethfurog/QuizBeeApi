using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuizBeeApp.Mobile.DependencyServices;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizBeeApp.Mobile.ViewModels
{
	public class VerificationPageViewModel : ViewModelBase
	{
        private readonly IParticipantService participantService;
        private readonly IMessageService messageService;

        PayloadVerificataion payload;

        private string _remrks;
        public string Remarks
        {
            get { return _remrks; }
            set { SetProperty(ref _remrks, value); }
        }

        public VerificationPageViewModel(INavigationService navigationService,
            IParticipantService participantService,
            IMessageService messageService) : base(navigationService)
        {
            this.participantService = participantService;
            this.messageService = messageService;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            payload = parameters["payload"] as PayloadVerificataion;
        }

        private DelegateCommand _submitRequestCommand;
        public DelegateCommand SubmitRequestCommand =>
            _submitRequestCommand ?? (_submitRequestCommand = new DelegateCommand(ExecuteSubmitRequestCommand));

        async void ExecuteSubmitRequestCommand()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                payload.Remarks = Remarks;
                var result = await participantService.SendVerificationRequest(payload);

                await NavigationService.GoBackAsync();
            }
            catch (Exception)
            {
                messageService.ShowMessage("Unable to send validation request");
                
            }
            finally
            {
                IsBusy = false;
            }
            
        }
    }
}
