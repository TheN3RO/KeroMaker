using CommunityToolkit.Maui.Views;

namespace KeroMaker;

public partial class GameHint6 : Popup
{

	public GameHint6()
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