using CommunityToolkit.Maui.Views;
using System.Reflection.Metadata.Ecma335;

namespace KeroMaker;

public partial class BurnerPage : ContentPage
{
    TimeCounter timewatch = GameData.Instance.Timewatch;

    private bool isIncreasingPower = false;
    private bool isPlaying = false;
    private bool isWon = false;
    private double condition;
    private double temperature;
    MainPage mainPage;
    public BurnerPage(MainPage mainPage)
    {
        this.mainPage = mainPage;
        InitializeComponent();

        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        BarLineResistance();
        UpdateTemperatureAndCondition();
        increaseButton.Text = "Start";
        temperature = 20;
        condition = 100;

    }
    private void Start(object sender, EventArgs e)
    {
        increaseButton.Pressed -= Start;
        increaseButton.Pressed += OnIncreasePowerButtonPressed;
        isPlaying = true;
        increaseButton.Text = "Mocniej!";
    }
    //15-340
    private void MoveBarLineLeft()
    {
        double x = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
        double position = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
        double i;

        if (position < 140)
        {
            i = 2;
        }
        else if(position < 210)
        {
            i = 1.75;
        }
        else
        {
            i = 1.5;
        }
        
        if (x > 15)
        {
            x = x - i;
        }

        AbsoluteLayout.SetLayoutBounds(barLineImage, new Rect(x, 5, barLineImage.Width, barLineImage.Height));
    }

    private void MoveBarLineRight()
    {
        double x = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
        double position = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
        double i;
        if (position < 140)
        {
            i = 1.5;
        }
        else if (position < 210)
        {
            i = 3;
        }
        else
        {
            i = 2;
        }

        if (x < 343)
        {
            x = x + i;
        }

        AbsoluteLayout.SetLayoutBounds(barLineImage, new Rect(x, 5, barLineImage.Width, barLineImage.Height));
    }
    private void BarLineResistance()
    {
        Device.StartTimer(TimeSpan.FromMilliseconds(2), () =>
        {
            if (isPlaying) { 
                if (!isIncreasingPower)
                {
                    MoveBarLineLeft();
                    return true; 
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        });
    }

    

    private void DecreaseTemperature(double cold)
    {
        if (temperature > 20)
        {
            temperature -= cold;
        }
        ChangeTemperature();
    }
    private void IncreaseTemperature(double warmth)
    {
        temperature += warmth;
        ChangeTemperature();
        if(temperature >= 250)
        {
            isWon=true;
            End();
        }
    }

    private void ChangeTemperature()
    {
        currentTemperature.Text = Convert.ToString(Convert.ToInt32(temperature)) + "°C z 250°C";
    }

    private void DecreaseCondition(double destroy)
    {
        condition -= destroy;
        currentCondition.Text= "Stan:" + Convert.ToString(Convert.ToInt32(condition)) + "%";
        if (condition <= 0)
        {
            End();
        }
    }

    private void UpdateTemperatureAndCondition()
    {
        // 60,85,110,140,168,190,215,245,275,295,335
        Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
        {
            if (isPlaying) { 
                double position = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
                if (position < 60)
                {
                    DecreaseTemperature(8);
                }
                else if (position < 85)
                {
                    DecreaseTemperature(5);
                }
                else if (position < 110)
                {
                    IncreaseTemperature(0);
                }
                else if (position < 140)
                {
                    IncreaseTemperature(0.75);
                }
                else if (position < 168)
                {
                    IncreaseTemperature(1);
                }
                else if (position < 190)
                {
                    IncreaseTemperature(2.5);
                }
                else if (position < 275)
                {
                    IncreaseTemperature(4.5);
                    DecreaseCondition(5);
                }
                else if (position >= 275)
                {
                    IncreaseTemperature(6);
                    DecreaseCondition(10);
                }
            }
            return true;
        });
    }

    // Event handler for the button press
    private void OnIncreasePowerButtonPressed(object sender, EventArgs e)
    {
        isIncreasingPower = true;

        // Start moving the bar line right
        Device.StartTimer(TimeSpan.FromMilliseconds(2), () =>
        {
            if (isPlaying)
            {
                if (isIncreasingPower)
                {
                    MoveBarLineRight();
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            else
            {
                return true;
            }
            
        });
    }
    private void OnIncreasePowerButtonReleased(object sender, EventArgs e)
    {
        isIncreasingPower = false;
        BarLineResistance();
    }
    private void End()
    {
        isPlaying = false;
    } 
    
    private void ImageLeftButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage(mainPage));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timewatch.StartDispatcherTimer();
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