using CommunityToolkit.Maui.Views;

namespace KeroMaker;

public partial class HintPopupPage : Popup
{

	public HintPopupPage()
	{
		InitializeComponent();
	}
	private void ButtonOk_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}