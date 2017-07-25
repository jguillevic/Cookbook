using Cookbook.UI.ViewData.Recipe;
using System;
using System.Linq;
using Tools.UI.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToMeasureVDConverter : DependencyObject, IValueConverter
    {
        public ObservableRangeCollection<MeasureSummaryVD> Measures
        {
            get { return (ObservableRangeCollection<MeasureSummaryVD>)GetValue(MeasuresProperty); }
            set { SetValue(MeasuresProperty, value); }
        }

        public static readonly DependencyProperty MeasuresProperty =
            DependencyProperty.Register("Measures",
                                        typeof(ObservableRangeCollection<MeasureSummaryVD>),
                                        typeof(GuidToMeasureVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return Measures.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var measure = (MeasureSummaryVD)value;

            return measure.Id;
        }
    }
}
