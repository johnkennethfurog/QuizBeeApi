using Android.App;
using Android.Content.PM;
using Android.OS;
using Lottie.Forms.Droid;
using Prism;
using Prism.Ioc;
using QuizBeeApp.Mobile.DependencyServices;
using QuizBeeApp.Mobile.Droid.DependencyServices;
using Xamarin.Forms;

namespace QuizBeeApp.Mobile.Droid
{
    [Activity(Label = "QuizBeeApp.Mobile", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            AnimationViewRenderer.Init();
            FormsMaterial.Init(this, bundle);

            //FormsMaterial.Init(this, bundle);


            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMessageService, MessageService>();
            // Register any platform specific implementations
        }
    }
}

