using CommunityToolkit.Maui.Views;

namespace KeroMaker;

public partial class GameHint4 : Popup
{

	public GameHint4()
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