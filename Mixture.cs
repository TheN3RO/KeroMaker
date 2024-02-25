using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class Mixture : INotifyPropertyChanged
    {
        List<Ingredient> mixtureComp = new List<Ingredient>(); //mixture composition

        private string mixtureImageName = "mixture_bottle.svg";
        private string mixtureFullBottleName = "mixture_full_bottle.svg";

        private Color color = new Color(0, 0, 0);

        public event PropertyChangedEventHandler? PropertyChanged;

        public Color FinalColor
        {
            get { return color; }
            set { color = value; }
        }

        public Image mixtureImage()
        {
            var image = new Image();
            image.Source = mixtureImageName;

            OnPropertyChanged("image");

            return image;
        }

        public void addIngredient(Ingredient ingredient)
        {
            mixtureComp.Add(ingredient);
            FinalColor = MixColors(FinalColor, ingredient.IngColor);
        }

        private static Color MixColors(Color color1, Color color2)
        {
            float mixedRed = (float)((color1.Red + color2.Red) / 2.0);
            float mixedGreen = (float)((color1.Green + color2.Green) / 2.0);
            float mixedBlue = (float)((color1.Blue + color2.Blue) / 2.0);

            return new Color(mixedRed, mixedGreen, mixedBlue);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
