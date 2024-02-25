
using System.Xml.Linq;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;

namespace KeroMaker
{
    public partial class MainPage : ContentPage
    {
        MediaElement mediaPlayer;

        TimeCounter timewatch = GameData.Instance.Timewatch;

        public MainPage()
        {
            InitializeComponent();
        }
        public double VolumeMusic
        {
            get { return backgroundMusic.Volume; }
            set { backgroundMusic.Volume = value; }
        }
        public MainPage(GamePage gamePage){
            InitializeComponent();
        }
        public void StopMusic()
        {
          backgroundMusic.Stop();
        }
        private void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            
            Navigation.PushAsync(new GamePage(this));
            timewatch.RestartTimer();
        }

        private void ButtonSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(this));
        }

        private void ButtonTutorial_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }
    }
}
