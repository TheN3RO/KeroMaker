namespace KeroMaker.PopUps;
using CommunityToolkit.Maui.Views;

public partial class IngredientsKeroseneWinPopUp : Popup
{
	public IngredientsKeroseneWinPopUp()
	{
		InitializeComponent();
	}

    private void ButtonOk_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}