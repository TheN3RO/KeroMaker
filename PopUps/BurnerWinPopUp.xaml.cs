using CommunityToolkit.Maui.Views;
using System.Text;

namespace KeroMaker.PopUps;

public partial class BurnerWinPopUp : Popup	
{
	public BurnerWinPopUp()
	{
		InitializeComponent();
	}
	private void ButtonOk_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}