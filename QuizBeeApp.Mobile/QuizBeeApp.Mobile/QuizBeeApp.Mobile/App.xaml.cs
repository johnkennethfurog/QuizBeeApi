using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using QuizBeeApp.Mobile.Helpers;
using QuizBeeApp.Mobile.Interfaces;
using QuizBeeApp.Mobile.Services;
using QuizBeeApp.Mobile.ViewModels;
using QuizBeeApp.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace QuizBeeApp.Mobile
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
#if DEBUG
            //LiveReload.Init();
#endif
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start("android=e341c96b-a490-44de-9980-920a9f6570ce;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegistrationPage, RegistrationPageViewModel>();
            containerRegistry.RegisterForNavigation<QuestionPage, QuestionPageViewModel>();

            var apiAccess = new ApiAccess()
            {
                BaseUri = EndpointHelper.ROOT_URI
                //BaseUri = "http://192.168.1.50:10778"
            };
            containerRegistry.RegisterInstance<IApiAccess>(apiAccess);
            containerRegistry.Register<IParticipantService, ParticipantService>();
            containerRegistry.Register<IRequestHandler, RequestHandler>();

            containerRegistry.RegisterForNavigation<VerificationPage, VerificationPageViewModel>();
            containerRegistry.RegisterForNavigation<PointsDisplayPage, PointsDisplayPageViewModel>();
        }
    }
}
