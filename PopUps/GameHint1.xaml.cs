using CommunityToolkit.Maui.Views;

namespace KeroMaker;

public partial class GameHint1 : Popup
{

	public GameHint1()
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