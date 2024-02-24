using System.Diagnostics;

namespace KeroMaker;

public partial class GamePage : ContentPage
{
    private Stopwatch stopwatch;
    private bool isCounting;
    List<Ingredient> ingredients = new List<Ingredient>();

    //Tworzenie obiektu modyfikowalnego przez gracza
    Mixture playerMixture = new Mixture();
    MainPage mainPage;
    public GamePage(MainPage mainPage)
    {
        this.mainPage = mainPage;
        InitializeComponent();
        // Inicjowanie licznika czasu gry
        stopwatch = new Stopwatch();
        StartDispatcherTimer();
        //Tworzenie kontenera
        Grid container = objContainer;
        // Tworzenie obiektów pocz¹tkowych
        var obj1 = new Image
        {
            Source = "cauldron.png",
            Aspect = Aspect.AspectFit
        };
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
        ingredients.Add(new Ingredient("ropa", new Color(0, 146, 0), "oil_container"));
        ingredients.Add(new Ingredient("gaz", new Color(255, 255, 168), "gas"));

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
        Navigation.PushAsync(new IngredientPage(ingredients));
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
        Navigation.PushAsync(new IngredientPage(ingredients));
    }
    private void StartDispatcherTimer()
    {
        stopwatch.Start();
        isCounting = true;
        Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
        {
            UpdateElapsedTime();
            return isCounting;
        });
    }

    private void UpdateElapsedTime()
    {
        TimeSpan elapsed = stopwatch.Elapsed;
        string elapsedTimeString = $"{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        gameTimeDispatcher.Text = elapsedTimeString;
    }
}