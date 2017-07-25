using Cookbook.UI.ViewData.Recipe;
using System;
using System.Linq;
using Tools.UI.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToSeasonVDConverter : DependencyObject, IValueConverter
    {
        public ObservableRangeCollection<SeasonVD> Seasons
        {
            get { return (ObservableRangeCollection<SeasonVD>)GetValue(SeasonsProperty); }
            set { SetValue(SeasonsProperty, value); }
        }

        public static readonly DependencyProperty SeasonsProperty =
            DependencyProperty.Register("Seasons",
                                        typeof(ObservableRangeCollection<FeatureVD>),
                                        typeof(GuidToSeasonVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return Seasons.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var season = (SeasonVD)value;

            return season.Id;
        }
    }
}
