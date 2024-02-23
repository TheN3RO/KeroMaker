using System.Diagnostics;

namespace KeroMaker;

public partial class GamePage : ContentPage
{
    List<Ingredient> ingredients = new List<Ingredient>();

    //Tworzenie obiektu globalnego modyfikowalnego przez gracza
    Mixture playerMixture = GameData.Instance.Mixture;

    TimeCounter timewatch = GameData.Instance.Timewatch;

    public GamePage()
    {
        InitializeComponent();

        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        // Tworzenie obiekt�w pocz�tkowych
        var destylator1 = new Image
        {
            Source = "destylator_1_part.svg",
            Aspect = Aspect.AspectFit
        };
        //Dodawanie obiekt�w do kontenera
        //container.Children.Add(obj1);
        //container.Children.Add(destylator1);

        //Obs�uga klikni�cia objektu
        /*obj1.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => ImageObj1_Tapped())
        });*/
        destylator1.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => ImageDestylator1_Tapped())
        });

        //Inicjalizowanie sk�adnik�w bazowych
        ingredients.Add(new Ingredient("Ropa", new Color(18, 6, 1), "oil_container"));
        ingredients.Add(new Ingredient("H2SO4", new Color(69, 12, 112), "potion"));

        playerMixture.addIngredient(ingredients[0]);
        playerMixture.addIngredient(ingredients[1]);

    }

    //Wydarzenia klikni�cia obiekt�w
    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
    private void ImageBurner_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BurnerPage());
    }
    private void ImageDestylator2_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new IngredientPage(ingredients));
    }
    private void ImageLeftButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
    private void ImageButtonHint_Clicked(object sender, EventArgs e)
    {
        //TODO: System podpowiedzi
    }
    private void ImageDestylator1_Tapped()
    {
        Navigation.PushAsync(new IngredientPage(ingredients));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timewatch.StartDispatcherTimer();
    }
}