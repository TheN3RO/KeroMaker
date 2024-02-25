using CommunityToolkit.Maui.Views;
using KeroMaker;
namespace MauiToolkitPopupSample.PopUps;

public partial class PausePopUp : Popup
{
	MainPage mainPage;
	public PausePopUp(MainPage mainPage)
	{
		this.mainPage = mainPage;	
		InitializeComponent();
	}
	private void ButtonReturn_Clicked(object sender,EventArgs e)
	{
		Close();
	}
    private  void ButtonMenu_Clicked(object sender, EventArgs e)
    {
		Close("toMenu");
    }
}