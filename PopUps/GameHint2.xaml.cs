using CommunityToolkit.Maui.Views;

namespace KeroMaker;

public partial class GameHint2 : Popup
{

	public GameHint2()
	{
		InitializeComponent();
	}
	private void ButtonOk_Clicked(object sender, EventArgs e)
	{
		if (!switchHints.IsToggled)
		{
			Close("offHints");
		}
        else
        {
			Close("onHints");
        }
	}
}