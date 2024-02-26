using Microsoft.Maui.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroMaker
{
    public class Mixture : INotifyPropertyChanged
    {
        public ObservableCollection<Ingredient> mixtureComp = new ObservableCollection<Ingredient>(); //mixture composition

        private Color color = new Color(0, 0, 0);

        public event PropertyChangedEventHandler? PropertyChanged;

        public int CollectionCount { get; set; }

        private Image image = new();

        public Image Image
        {
            get { return image; }
            set 
            { 
                image = value;
                OnPropertyChanged("image");
            }
        }

        public Color FinalColor
        {
            get { return color; }
            set { color = value; }
        }

        public Mixture()
        {
            mixtureComp.CollectionChanged += ingredientCollectionChanged;
        }

        private void ingredientCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            // Check if items were added or removed from the collection
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                // Update FreeSlotsLabel.Text with the count of items in the collection
                CollectionCount = mixtureComp.Count;
                OnPropertyChanged("CollectionCount");
            }
        }

        public void addIngredient(Ingredient ingredient)
        {
            FinalColor = MixColors(FinalColor, ingredient.IngColor);
            if (mixtureComp.Count < 4) //max 4 items in mixtures
            {
                mixtureComp.Add(ingredient);
            }
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
