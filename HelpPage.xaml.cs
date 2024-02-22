using KeroMaker.HelpPageModels;

namespace KeroMaker;

public partial class HelpPage : ContentPage
{
    public HelpPage()
    {
        InitializeComponent();
    }
    private void LeftButton_Cliked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private void AboutGame_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AboutGame());
    }
    private void AboutUs_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AboutUs());
    }
}