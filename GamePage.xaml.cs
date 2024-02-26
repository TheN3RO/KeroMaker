using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using MauiToolkitPopupSample.PopUps;

namespace KeroMaker;

public partial class GamePage : ContentPage
{
    List<Ingredient> ingredients = new List<Ingredient>();
    Popup popup;
    //Tworzenie obiektu modyfikowalnego przez gracza
    MainPage mainPage;Mixture playerMixture = GameData.Instance.Mixture;

    TimeCounter timewatch = GameData.Instance.Timewatch;
    public GamePage(MainPage mainPage)
    {
        this.mainPage = mainPage;
        InitializeComponent();
        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        // Tworzenie obiektów pocz¹tkowych
        var destylator1 = new Image
        {
            Source = "destylator_1_part.svg",
            Aspect = Aspect.AspectFit
        };
        //Dodawanie obiektów do kontenera
        //container.Children.Add(obj1);
        //container.Children.Add(destylator1);

        //Obs³uga klikniêcia objektu
        /*obj1.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => ImageObj1_Tapped())
        });*/
        destylator1.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => ImageDestylator1_Tapped())
        });

        //Inicjalizowanie sk³adników bazowych
        ingredients.Add(new Ingredient("Ropa", new Color(18, 6, 1), "oil_container"));
        ingredients.Add(new Ingredient("H2SO4", new Color(69, 12, 112), "potion"));

        playerMixture.addIngredient(ingredients[0]);
        playerMixture.addIngredient(ingredients[1]);
        
    }
    private async void ImagePauseButton_Clicked(object sender, EventArgs e)
    {
        var popup = new PausePopUp(mainPage);
        timewatch.PauseTimer();
        var result = await Application.Current.MainPage.ShowPopupAsync(popup);
        if (result is "toMenu")
        {
            await Navigation.PopAsync();
        }
        else
        {
            timewatch.StartDispatcherTimer();
        }
    }
    //Wydarzenia klikniêcia obiektów
    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage(mainPage));
    }
    private void ImageBurner_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BurnerPage(mainPage));
    }
    private void ImageDestylator2_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new IngredientPage(ingredients,mainPage));
    }
    private async void ImageButtonHint_Clicked(object sender, EventArgs e)
    {
        var popup = new HintPopupPage();
        timewatch.PauseTimer();
        var result = await Application.Current.MainPage.ShowPopupAsync(popup);
        if (result is null)
        {
            timewatch.StartDispatcherTimer();
        }
    }
    private void ImageDestylator1_Tapped()
    {
        Navigation.PushAsync(new IngredientPage(ingredients, mainPage));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timewatch.StartDispatcherTimer();
    }

    private async void ShowPopupButton_Clicked(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new HintPopupPage());
    }
}