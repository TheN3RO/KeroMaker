using CommunityToolkit.Maui.Views;
using System.Text;

namespace KeroMaker.PopUps;

public partial class BurnerLosePopUp : Popup
{
    public BurnerLosePopUp()
    {
        InitializeComponent();
    }
    private void ButtonOk_Clicked(object sender, EventArgs e)
    {
        Close();
    }
}