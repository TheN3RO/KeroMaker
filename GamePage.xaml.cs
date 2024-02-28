using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using MauiToolkitPopupSample.PopUps;
using KeroMaker.PopUps;
using System;
using System.Reflection;

namespace KeroMaker;

public partial class GamePage : ContentPage
{
    List<Ingredient> ingredients = new List<Ingredient>();
    Popup popup;
    //Tworzenie obiektu modyfikowalnego przez gracza
    MainPage mainPage;Mixture playerMixture = GameData.Instance.Mixture;

    TimeCounter timewatch = GameData.Instance.Timewatch;
    private int gamePhase;
    private int currentOilBottlePhase;
    private int currentKeroseneBottlePhase;
    
    public int GamePhase
    {
        get { return gamePhase; }
        set
        {
            switch (value) 
            {
                case 0:
                    gamePhase = value;
                    ChangeOilBottlePhase(0);
                    ChangeKeroseneBottlePhase(0);
                    break;
                case 1:
                    gamePhase = value;
                    ChangeOilBottlePhase(1);
                    ChangeKeroseneBottlePhase(0);
                    break;
                case 2:
                    gamePhase = value;
                    ChangeOilBottlePhase(0);
                    ChangeKeroseneBottlePhase(1);
                    break;
                case 3:
                    gamePhase = value;
                    ChangeOilBottlePhase(0);
                    ChangeKeroseneBottlePhase(1);
                    break;
                case 4:
                    ChangeOilBottlePhase(0);
                    ChangeKeroseneBottlePhase(2);
                    gamePhase = value;
                    break;
                case 5:
                    gamePhase = value;
                    Win();
                    break;
                default:
                    break;

            }
        }
    }
    
    public GamePage(MainPage mainPage)
    {
        
        InitializeComponent();
        this.mainPage = mainPage;
        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        // Tworzenie obiektów pocz¹tkowych
        var destylator1 = new Image
        {
            Source = "destylator_1_part.svg",
            Aspect = Aspect.AspectFit
        };
        destylator1.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() => ImageDestylator1_Tapped())
        });

        //Inicjalizowanie sk³adników bazowych
        ingredients.Add(new Ingredient("Ropa", new Color(18, 6, 1), "oil_container"));
        ingredients.Add(new Ingredient("H2SO4", new Color(69, 12, 112), "potion"));

        playerMixture.addIngredient(ingredients[0]);
        playerMixture.addIngredient(ingredients[1]);
        imageDestylator3Part.Source = "destylator_3_part.svg";
        GamePhase = 0;
        currentKeroseneBottlePhase = 0;
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
    private void ChangeOilBottlePhase (int phase)
    {
        if(phase == 0)
        {
            currentOilBottlePhase = 0;
            imageDestylator2Part.Source = "destylator_2_part.svg";
        }
        else if(phase == 1) 
        {
            currentOilBottlePhase = 1;
            imageDestylator2Part.Source = "destylator_2_part_filled.png";
        }
    }
    private void ChangeKeroseneBottlePhase(int phase)
    {
        if (phase == 0)
        {
            currentKeroseneBottlePhase = 0;
            imageDestylator3Part.Source = "destylator_3_part.svg";
        }
        else if (phase == 1)
        {
            currentKeroseneBottlePhase = 1;
            imageDestylator3Part.Source = "destylator_3_part_refined.png";
        }
        else if (phase == 2)
        {
            currentKeroseneBottlePhase = 2;
            imageDestylator3Part.Source = "destylator_3_part_filled.png";
        }
    }
    //Wydarzenia klikniêcia obiektów
    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage(mainPage));
    }
    private void ImageBurner_Clicked(object sender, EventArgs e)
    {
        if (gamePhase == 1)
        {
            Navigation.PushAsync(new BurnerPage(mainPage, this));
        }
        
    }
    private void ImageDestylator2_Clicked(object sender, EventArgs e)
    {
        if (gamePhase == 0)
        {
            Navigation.PushAsync(new IngredientPage(ingredients,mainPage,this));
        }
        
    }
    private void ImageDestylator3_Clicked(object sender, EventArgs e)
    {
        if (gamePhase == 2)
        {
            GamePhase = 3;
        } else if (gamePhase == 3)
        {
            Navigation.PushAsync(new IngredientPage(ingredients, mainPage, this));
        }


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
    bool isLampClicked = false;
    private void ImageLamp_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new IngredientPage(ingredients, mainPage, this));
        if (gamePhase == 4 && !isLampClicked) 
        {
            isLampClicked = true;
            FinalAnimation();
        }
    }
    

    private void FinalAnimation()
    {
        string desylatorSource;
        string lampSource;
        int destykatorIndex = 6;
        int lampIndex = 1;
        Device.StartTimer(TimeSpan.FromMilliseconds(800), () =>
        {
            desylatorSource = "destylator_3_part_filled_" + Convert.ToString(destykatorIndex) + ".png";
            lampSource = "lamp_filled" + Convert.ToString(destykatorIndex) + ".png";

            imageLamp.Source = lampSource;
            imageDestylator3Part.Source = desylatorSource;

            destykatorIndex--;
            lampIndex++;

            if (destykatorIndex == 0)
            {
                GamePhase = 5;
                return false;
            }
            else
            { 
                return true;
            }
            
        });
    }
    async Task PutTaskDelay()
    {
        await Task.Delay(2000);
    }
    private async void Win()
    {
        await PutTaskDelay();
        string time = timewatch.ElapsedTime;
        timewatch.PauseTimer();
        var popup = new GameWinPopUp(time);
        var result = await Application.Current.MainPage.ShowPopupAsync(popup);
        if (result is null)
        {
            await Navigation.PopAsync();
        }
    }
    private void ImageDestylator1_Tapped()
    {
        Navigation.PushAsync(new IngredientPage(ingredients, mainPage,this));
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