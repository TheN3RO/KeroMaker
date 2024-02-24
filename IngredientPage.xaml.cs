using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

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

    TimeCounter timewatch = GameData.Instance.Timewatch;

    public IngredientPage(List<Ingredient> ingredients)
    {
        InitializeComponent();

        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        this.Ingredients = new ObservableCollection<Ingredient>(ingredients);

        var mixtureImage = playerMixture.mixtureImage();
        mixtureImage.Margin = new Thickness(0, 0, 0, -50);
        mixtureImage.WidthRequest = 470;
        mixtureImage.HeightRequest = 470;

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
                    WidthRequest = 100
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
                                                                      // Przypisanie obrazu do kontekstu, aby mo¿na by³o go póŸniej odzyskaæ.
                dragGestureRecognizer.BindingContext = image;
                // Dodanie dragGestureRecognizer do GestureRecognizers obrazu, aby by³ on "przeci¹galny".
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
        // Pobranie obrazu z kontekstu zwi¹zanego z gestem przeci¹gania.
        var image = dragGestureRecognizer.BindingContext as Image;
        // Jeœli obraz istnieje, jego Ÿród³o jest dodawane do danych przeci¹gania.
        if (image != null)
        {
            e.Data.Properties["Image"] = image;
        }
    }

    // Zdarzenie wywo³ywane, kiedy obraz jest przeci¹gany nad obszarem, gdzie mo¿na go upuœciæ.
    private void OnDragOver(object sender, DragEventArgs e)
    {
        // Okreœlenie operacji, która bêdzie akceptowana (tutaj kopiowanie).
        e.AcceptedOperation = DataPackageOperation.Copy;
    }

    private void OnDrop(object sender, DropEventArgs e)
    {
        if (e.Data.Properties.ContainsKey("Image"))
        {
            var draggedFrame = (Image)e.Data.Properties["Image"];
            var ingredient = (Ingredient)draggedFrame.BindingContext;

            playerMixture.addIngredient(ingredient);

            Debug.Write($"Dodano sk³adnik. Obecny kolor mikstury: {playerMixture.FinalColor}");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timewatch.StartDispatcherTimer();
    }

    private void ImageLeftButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
}