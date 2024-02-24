
using System.Xml.Linq;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Plugin.Maui.Audio;

namespace KeroMaker
{
    public partial class MainPage : ContentPage
    {
        MediaElement mediaPlayer;

        
        public MainPage()
        {
            InitializeComponent();
            PlayMusic();
        }
        public double VolumeMusic
        {
            get { return backgroundMusic.Volume; }
            set { backgroundMusic.Volume = value; }
        }
        private void PlayMusic()
        {
          
            
        }
        private void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GamePage(this));
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
