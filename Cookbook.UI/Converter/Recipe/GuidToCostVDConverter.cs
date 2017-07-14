using Cookbook.UI.ViewData.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Cookbook.UI.Converter.Recipe
{
    public class GuidToCostVDConverter : DependencyObject, IValueConverter
    {
        public List<CostVD> Costs
        {
            get { return (List<CostVD>)GetValue(CostsProperty); }
            set { SetValue(CostsProperty, value); }
        }

        public static readonly DependencyProperty CostsProperty =
            DependencyProperty.Register("Costs",
                                        typeof(List<CostVD>),
                                        typeof(GuidToCostVDConverter),
                                        new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var id = (Guid)value;

            return Costs.First(item => item.Id == id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var cost = (CostVD)value;

            return cost.Id;
        }
    }
}
