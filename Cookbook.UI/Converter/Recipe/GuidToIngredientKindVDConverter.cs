using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToIngredientKindVDConverter : DependencyObject, IValueConverter
    {
        public List<IngredientKindVD> IngredientKinds
        {
            get { return (List<IngredientKindVD>)GetValue(IngredientKindsProperty); }
            set { SetValue(IngredientKindsProperty, value); }
        }

        public static readonly DependencyProperty IngredientKindsProperty =
            DependencyProperty.Register("IngredientKinds",
                                        typeof(List<IngredientKindVD>),
                                        typeof(GuidToIngredientKindVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return IngredientKinds.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var ingredientKind = (IngredientKindVD)value;

            return ingredientKind.Id;
        }
    }
}
