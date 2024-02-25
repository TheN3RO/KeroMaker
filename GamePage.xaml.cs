using CommunityToolkit.Maui.Views;
using System.Diagnostics;

namespace KeroMaker;

public partial class GamePage : ContentPage
{
    List<Ingredient> ingredients = new List<Ingredient>();

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
    private void ImageLeftButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private void ImageButtonHint_Clicked(object sender, EventArgs e)
    {
        //TODO: System podpowiedzi
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