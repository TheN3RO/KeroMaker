using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using KeroMaker.PopUps;

namespace KeroMaker;

public partial class IngredientPage : ContentPage
{
    private ObservableCollection<Ingredient> _ingredients;

    public ObservableCollection<Ingredient> Ingredients
    {
        get => _ingredients;
        set
        {
            if (_ingredients != value)
            {
                _ingredients = value;
            }
        }
    }

    //Tworzenie obiektu globalnego modyfikowalnego przez gracza
    Mixture playerMixture = GameData.Instance.Mixture;
    MainPage mainPage;
    GamePage gamePage;
    private TimeCounter timewatch = GameData.Instance.Timewatch;

    public IngredientPage(List<Ingredient> ingredients, MainPage mainPage, GamePage gamePage)
    {
        InitializeComponent();
        this.mainPage = mainPage;
        this.gamePage = gamePage;
        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        this.Ingredients = new ObservableCollection<Ingredient>(ingredients);

        var mixtureImage = playerMixture.Image;
        mixtureImage.Margin = new Thickness(0, 0, 0, -50);
        mixtureImage.WidthRequest = 470;
        mixtureImage.HeightRequest = 470;
        if (gamePage.GamePhase == 0)
        {
            playerMixture.Image.Source = "mixture_bottle.svg";
        }
        else if (gamePage.GamePhase == 3)
        {
            playerMixture.Image.Source = "mixture_in_bottle.svg";
        }

        AbsoluteLayout.SetLayoutBounds(mixtureImage, new Rect(0.5, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
        AbsoluteLayout.SetLayoutFlags(mixtureImage, AbsoluteLayoutFlags.PositionProportional);

        DropZone.Add(mixtureImage);

        // Tworzenie CollectionView
        var collectionView = new CollectionView
        {
            ItemsSource = this.Ingredients,
            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid { Padding = new Thickness(10) };
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var frame = new Frame
                {
                    BackgroundColor = new Color(0, 0, 0, 128),
                    CornerRadius = 10,
                    WidthRequest = 120,
                    Margin = 3,
                };

                var verticalStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical
                };

                var image = new Image { Aspect = Aspect.AspectFill };
                image.SetBinding(Image.SourceProperty, "ImageUrl");

                var nameLabel = new Label
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    TextColor = new Color(173, 173, 173),
                    FontAttributes = FontAttributes.Bold
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                verticalStackLayout.Children.Add(image);
                verticalStackLayout.Children.Add(nameLabel);

                frame.Content = verticalStackLayout;
                var dragGestureRecognizer = new DragGestureRecognizer();
                dragGestureRecognizer.DragStarting += OnDragStarting; // Subskrypcja zdarzenia DragStarting.
                                                                      // Przypisanie obrazu do kontekstu, aby mo�na by�o go p�niej odzyska�.
                dragGestureRecognizer.BindingContext = image;
                // Dodanie dragGestureRecognizer do GestureRecognizers obrazu, aby by� on "przeci�galny".
                frame.GestureRecognizers.Add(dragGestureRecognizer);
                grid.Children.Add(frame);

                Grid.SetRow(frame, 0);

                return new Grid { Children = { frame } };
            })
        };
        collectionView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal);

        items.Add(collectionView);

        var dropGestureRecognizer = new DropGestureRecognizer();
        dropGestureRecognizer.DragOver += OnDragOver; // Subskrypcja zdarzenia DragOver.
        dropGestureRecognizer.Drop += OnDrop; // Subskrypcja zdarzenia Drop.
                                              // Dodanie dropGestureRecognizer do GestureRecognizers obszaru upuszczania.
        DropZone.GestureRecognizers.Add(dropGestureRecognizer);
    }

    private void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        var dragGestureRecognizer = (DragGestureRecognizer)sender;
        // Pobranie obrazu z kontekstu zwi�zanego z gestem przeci�gania.
        var image = dragGestureRecognizer.BindingContext as Image;
        // Je�li obraz istnieje, jego �r�d�o jest dodawane do danych przeci�gania.
        if (image != null)
        {
            e.Data.Properties["Image"] = image;
        }
    }

    // Zdarzenie wywo�ywane, kiedy obraz jest przeci�gany nad obszarem, gdzie mo�na go upu�ci�.
    private void OnDragOver(object sender, DragEventArgs e)
    {
        // Okre�lenie operacji, kt�ra b�dzie akceptowana (tutaj kopiowanie).
        e.AcceptedOperation = DataPackageOperation.Copy;
    }

    private void OnDrop(object sender, DropEventArgs e)
    {
        if (e.Data.Properties.ContainsKey("Image"))
        {
            var draggedFrame = (Image)e.Data.Properties["Image"];
            var ingredient = (Ingredient)draggedFrame.BindingContext;

            if (gamePage.GamePhase == 0 && ingredient.Name == "Ropa") 
            {
                playerMixture.addIngredient(ingredient);

                playerMixture.Image.Source = "mixture_in_bottle.svg";
                Win();
            } else if (gamePage.GamePhase == 3 && ingredient.Name == "H2SO4")
            {
                playerMixture.addIngredient(ingredient);

                playerMixture.Image.Source = "refined_kerosene_in_bottle.svg";
                Win();
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timewatch.StartDispatcherTimer();
    }

    private async void Win()
    {
        if (gamePage.GamePhase == 0)
        {
            gamePage.GamePhase = 1;
            var popup = new IngredientsWinPopUp();
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);
            if (result is null)
            {
                await Navigation.PopAsync();
                timewatch.StartDispatcherTimer();
            }
        } else if (gamePage.GamePhase == 3) 
        { 
            gamePage.GamePhase = 4;
            await Navigation.PopAsync();
        }
    }

    private void ImageLeftButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage(mainPage));
    }

    private async void ShowPopupButton_Clicked(object sender, EventArgs e)
    {
        var popup = new HintPopupPage();
        timewatch.PauseTimer();
        var result = await Application.Current.MainPage.ShowPopupAsync(popup);
        if (result is null)
        {
            timewatch.StartDispatcherTimer();
        }
    }
}