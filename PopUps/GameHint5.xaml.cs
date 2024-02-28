using CommunityToolkit.Maui.Views;

namespace KeroMaker;

public partial class GameHint5 : Popup
{

	public GameHint5()
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