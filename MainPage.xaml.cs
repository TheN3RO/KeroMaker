namespace KeroMaker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GamePage());
        }

        private void ButtonSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void ButtonTutorial_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }
    }
}
