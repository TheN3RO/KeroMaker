using CommunityToolkit.Maui.Views;
using System.Text;

namespace KeroMaker.PopUps;

public partial class IngredientsWinPopUp : Popup	
{
	public IngredientsWinPopUp()
	{
		InitializeComponent();
	}
	private void ButtonOk_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}