using Cookbook.UI.ViewData.Recipe;
using System;
using System.Linq;
using Tools.UI.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToIngredientVDConverter : DependencyObject, IValueConverter
    {
        public ObservableRangeCollection<IngredientSummaryVD> Ingredients
        {
            get { return (ObservableRangeCollection<IngredientSummaryVD>)GetValue(IngredientsProperty); }
            set { SetValue(IngredientsProperty, value); }
        }

        public static readonly DependencyProperty IngredientsProperty =
            DependencyProperty.Register("Ingredients",
                                        typeof(ObservableRangeCollection<IngredientSummaryVD>),
                                        typeof(GuidToIngredientVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return Ingredients.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var ingredient = (IngredientSummaryVD)value;

            return ingredient.Id;
        }
    }
}
