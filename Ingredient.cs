using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class Ingredient
    {
        public string Name { get; set; }
        public Color ingColor { get; set; }
        public string ImageUrl { get; set; }

        public Ingredient(string Name = "Ingredient", Color color = default, string imageUrl = "potion")
        {
            this.Name = Name;
            this.ingColor = color;
            this.ImageUrl = imageUrl;
        }
    }
}
