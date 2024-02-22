namespace KeroMaker.HelpPageModels;

public partial class AboutUs : ContentPage
{
    public AboutUs()
    {
        InitializeComponent();
    }
    private void LeftButton_Cliked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}