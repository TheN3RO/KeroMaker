using System.Reflection.Metadata.Ecma335;

namespace KeroMaker;

public partial class BurnerPage : ContentPage
{
    TimeCounter timewatch = GameData.Instance.Timewatch;

    private bool isIncreasingPower = false;
    private int temperature;
    public BurnerPage()
    {
        InitializeComponent();

        // Inicjowanie licznika czasu gry
        BindingContext = timewatch;

        BarLineResistance();
        UpdateTemperature();
        temperature = 20;
    }
    private void ImageLeftButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private void ImageButtonSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
    //15-340
    private void MoveBarLineLeft()
    {
        double x = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
        if (x > 15)
        {
            x = x - 2;
        }

        AbsoluteLayout.SetLayoutBounds(barLineImage, new Rect(x, 5, barLineImage.Width, barLineImage.Height));
    }

    private void MoveBarLineRight()
    {

        double x = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
        if (x < 343)
        {
            x = x + 2;
        }

        AbsoluteLayout.SetLayoutBounds(barLineImage, new Rect(x, 5, barLineImage.Width, barLineImage.Height));
    }
    private void BarLineResistance()
    {
        Device.StartTimer(TimeSpan.FromMilliseconds(2), () =>
        {
            if (!isIncreasingPower)
            {
                MoveBarLineLeft(); // Call the method to move the bar line left
                return true; // Return true to keep the timer running
            }
            else
            {
                return false;
            }
        });
    }



    // Event handler for the button press
    private void OnIncreasePowerButtonPressed(object sender, EventArgs e)
    {
        isIncreasingPower = true;

        // Start moving the bar line right
        Device.StartTimer(TimeSpan.FromMilliseconds(2), () =>
        {
            if (isIncreasingPower)
            {
                MoveBarLineRight();
                return true; // Return true to keep the timer running
            }
            else
            {
                return false; // Stop the timer
            }
        });
    }

    // Event handler for the button release
    private void OnIncreasePowerButtonReleased(object sender, EventArgs e)
    {
        isIncreasingPower = false;
        BarLineResistance();
    }
    private void DecreaseTemperature(int cold)
    {
        if (temperature > 20)
        {
            temperature -= cold;
        }
    }

    private void IncreaseTemperature(int warmth)
    {
        temperature += warmth;
    }

    private void UpdateTemperature()
    {
        // 60,85,110,140,168,190,215,245,275,295,335
        Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
        {
            double position = AbsoluteLayout.GetLayoutBounds(barLineImage).X;
            if (position < 60)
            {
                DecreaseTemperature(8);
            }
            else if (position < 85)
            {
                DecreaseTemperature(5);
            }
            else if (position > 110 && position < 140)
            {
                IncreaseTemperature(1);
            }
            else if (position < 168)
            {
                IncreaseTemperature(3);
            }
            else if (position < 190)
            {
                IncreaseTemperature(5);
            }
            else if (position < 275)
            {
                IncreaseTemperature(8);
            }
            else if (position >= 275)
            {
                IncreaseTemperature(10);
            }
            currentTemperature.Text = Convert.ToString(temperature) + "°C";
            return true;
        });
    }
    private void MoveLeftButton_Clicked(object sender, EventArgs e)
    {
        MoveBarLineLeft();
    }

    private void MoveRightButton_Clicked(object sender, EventArgs e)
    {
        MoveBarLineRight();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timewatch.StartDispatcherTimer();
    }
}