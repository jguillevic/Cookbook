using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Tools.UI.View
{
    // https://stackoverflow.com/questions/39055964/how-can-i-set-datatemplate-to-a-contentcontrol-in-a-pageresource-in-windows-univ
    public class AutoDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item) => GetTemplateForItem(item, null);

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) => GetTemplateForItem(item, container);

        private DataTemplate GetTemplateForItem(object item, DependencyObject container)
        {
            if (item != null)
            {
                var viewModelTypeName = string.Format("x{0}DataTemplate", item.GetType().Name);
                var dataTemplateInTree = FindResourceKeyUpTree(viewModelTypeName, container);
                //return or default to Application resource
                return dataTemplateInTree ?? (DataTemplate)Application.Current.Resources[viewModelTypeName];
            }
            return null;
        }

        private DataTemplate FindResourceKeyUpTree(string resourceKey, DependencyObject container)
        {
            var frameworkElement = container as FrameworkElement;
            if (frameworkElement != null)
            {
                if (frameworkElement.Resources.ContainsKey(resourceKey))
                {
                    return frameworkElement.Resources[resourceKey] as DataTemplate;
                }
                else
                {
                    return FindResourceKeyUpTree(resourceKey, VisualTreeHelper.GetParent(frameworkElement));
                }
            }
            return null;
        }
    }
}
