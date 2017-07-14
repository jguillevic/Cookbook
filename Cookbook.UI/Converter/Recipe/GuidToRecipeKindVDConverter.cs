using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToRecipeKindVDConverter : DependencyObject, IValueConverter
    {
        public List<RecipeKindVD> RecipeKinds
        {
            get { return (List<RecipeKindVD>)GetValue(RecipeKindsProperty); }
            set { SetValue(RecipeKindsProperty, value); }
        }

        public static readonly DependencyProperty RecipeKindsProperty =
            DependencyProperty.Register("RecipeKinds",
                                        typeof(List<RecipeKindVD>),
                                        typeof(GuidToRecipeKindVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return RecipeKinds.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var recipeKind = (RecipeKindVD)value;

            return recipeKind.Id;
        }
    }
}
