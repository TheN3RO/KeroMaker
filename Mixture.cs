using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class Mixture
    {
        List<Ingredient> mixtureComp = new List<Ingredient>(); //mixture composition

        private string mixtureImageName = "mixture_bottle.svg";

        private Color color = new Color(0, 0, 0);

        public Color FinalColor
        {
            get { return color; }
            set { color = value; }
        }

        public Image mixtureImage()
        {
            var image = new Image();
            image.Source = mixtureImageName;

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
    }
}
