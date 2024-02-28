using CommunityToolkit.Maui.Views;
using System.Text;

namespace KeroMaker.PopUps;

public partial class GameWinPopUp : Popup	
{
	public GameWinPopUp(string time)
	{
		
		InitializeComponent();
		timeLabel.Text = "w " + time;
	}
	private void ButtonOk_Clicked(object sender, EventArgs e)
	{	
		Close();
	}
}