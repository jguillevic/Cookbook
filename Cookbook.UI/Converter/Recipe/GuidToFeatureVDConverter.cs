using Cookbook.UI.ViewData.Recipe;
using System;
using System.Linq;
using Tools.UI.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToFeatureVDConverter : DependencyObject, IValueConverter
    {
        public ObservableRangeCollection<FeatureVD> Features
        {
            get { return (ObservableRangeCollection<FeatureVD>)GetValue(FeaturesProperty); }
            set { SetValue(FeaturesProperty, value); }
        }

        public static readonly DependencyProperty FeaturesProperty =
            DependencyProperty.Register("Features",
                                        typeof(ObservableRangeCollection<FeatureVD>),
                                        typeof(GuidToFeatureVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return Features.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var season = (FeatureVD)value;

            return season.Id;
        }
    }
}
