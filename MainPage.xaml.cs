using Plugin.Maui.Audio;

namespace KeroMaker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Play();
        }
        public async void Play()
        {
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tall_grass.wav"));
            audioPlayer.Loop = true;
            audioPlayer.Play();
            
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
