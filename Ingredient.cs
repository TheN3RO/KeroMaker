using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class Ingredient(string Name = "Ingredient", Color? color = null, string imageUrl = "potion")
    {
        public string Name { get; set; } = Name;
        public Color IngColor { get; set; } = color ?? new Color(0, 0, 0);
        public string ImageUrl { get; set; } = imageUrl;
    }
}