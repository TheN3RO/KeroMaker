using System.ComponentModel;

namespace KeroMaker;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(MainPage mainPage)
    {
        InitializeComponent();
        BindingContext = new VolumeViewModel();

        if (BindingContext is VolumeViewModel viewModel)
        {
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Volume")
                {
                    double newVolume = viewModel.Volume;
                    double music =  viewModel.Music;
                    newVolume = newVolume / 100;
                    music = music / 100;
                    mainPage.VolumeMusic = music * newVolume;
                }
                else if (e.PropertyName == "Music")
                {
                    double newMusic = viewModel.Music;
                    double volume = viewModel.Volume;
                    volume = volume / 100;
                    newMusic = newMusic / 100;
                    mainPage.VolumeMusic = newMusic * volume;
                }
            };
        }
    }

    private void LeftButton_Cliked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
public class VolumeViewModel : INotifyPropertyChanged
{
    private int volume;

    public int Volume
    {
        get { return volume; }
        set
        {
            volume = value;
            Preferences.Set("overallVolume", value);
            OnPropertyChanged(nameof(Volume));
        }
    }
    private int music;

    public int Music
    {
        get { return music; }
        set
        {
            music = value;
            Preferences.Set("musicVolume", value);
            OnPropertyChanged(nameof(Music));
        }
    }
    private int sound;

    public int Sound
    {
        get { return sound; }
        set
        {
            sound = value;
            Preferences.Set("soundsVolume", value);
            OnPropertyChanged(nameof(Sound));
        }
    }
    public VolumeViewModel()
    {
        if (!Preferences.ContainsKey("overallVolume"))
        {
            Preferences.Set("overallVolume", 100);
        }
        if (!Preferences.ContainsKey("musicVolume"))
        {
            Preferences.Set("musicVolume", 100);
        }
        if (!Preferences.ContainsKey("soundsVolume"))
        {
            Preferences.Set("soundsVolume", 100);
        }

        Volume = Preferences.Get("overallVolume", 0);
        Music = Preferences.Get("musicVolume", 0);
        Sound = Preferences.Get("soundsVolume", 0);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
