using Xamarin.Forms;

namespace QuizBeeApp.Mobile.Views
{
    public partial class QuestionPage : ContentPage
    {
        Frame _selectedFrame;
        public QuestionPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (_selectedFrame != null)
            {
                _selectedFrame.BorderColor = Color.FromHex("#FFFFFF");
                ToggleIndicator(false);
                

            }

            _selectedFrame = (Frame)sender;
            _selectedFrame.BorderColor = (Color)App.Current.Resources["success"];
            ToggleIndicator(true);
        }

        private void ToggleIndicator(bool isOn)
        {
            var stack = (StackLayout)_selectedFrame.Content;

            if (stack.Children.Count < 1)
                return;

            var indicator = (Label) stack.Children[0];
            indicator.TextColor = (Color)App.Current.Resources[isOn ? "success" : "silver"]; 
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (_selectedFrame == null)
                return;

            _selectedFrame.BorderColor = Color.FromHex("#FFFFFF");
            ToggleIndicator(false);

        }
    }
}
