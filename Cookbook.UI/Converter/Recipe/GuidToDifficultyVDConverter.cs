using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToDifficultyVDConverter : DependencyObject, IValueConverter
    {
        public List<DifficultyVD> Difficulties
        {
            get { return (List<DifficultyVD>)GetValue(DifficultiesProperty); }
            set { SetValue(DifficultiesProperty, value); }
        }

        public static readonly DependencyProperty DifficultiesProperty =
            DependencyProperty.Register("Difficulties",
                                        typeof(List<DifficultyVD>),
                                        typeof(GuidToDifficultyVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return Difficulties.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var difficulty = (DifficultyVD)value;

            return difficulty.Id;
        }
    }
}
