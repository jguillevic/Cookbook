using Cookbook.UI.ViewData.Recipe;
using System;
using System.Linq;
using Tools.UI.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToDifficultyVDConverter : DependencyObject, IValueConverter
    {
        public ObservableRangeCollection<DifficultyVD> Difficulties
        {
            get { return (ObservableRangeCollection<DifficultyVD>)GetValue(DifficultiesProperty); }
            set { SetValue(DifficultiesProperty, value); }
        }

        public static readonly DependencyProperty DifficultiesProperty =
            DependencyProperty.Register("Difficulties",
                                        typeof(ObservableRangeCollection<DifficultyVD>),
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
