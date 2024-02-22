namespace KeroMaker.HelpPageModels;

public partial class AboutGame : ContentPage
{
    public AboutGame()
    {
        InitializeComponent();
    }
    private void LeftButton_Cliked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}