using Windows.System;
using Windows.UI.Xaml;

namespace Tools.Component.Extension.TextBox
{
    public class OnlyIntegerTBExt : DependencyObject
    {
        public static readonly DependencyProperty OnlyIntegerProperty =
        DependencyProperty.RegisterAttached(
          "OnlyInteger",
          typeof(bool),
          typeof(OnlyIntegerTBExt),
          new PropertyMetadata(false, OnOnlyIntegerChanged)
        );

        public static bool GetOnlyInteger(DependencyObject obj)
        {
            return (bool)obj.GetValue(OnlyIntegerProperty);
        }

        public static void SetOnlyInteger(DependencyObject obj, bool value)
        {
            obj.SetValue(OnlyIntegerProperty, value);
        }

        private static void OnOnlyIntegerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as Windows.UI.Xaml.Controls.TextBox;
            if (textBox != null)
            {
                if ((bool)e.NewValue)
                {
                    textBox.KeyDown += TextBox_KeyDown;
                }
            }
        }

        private static void TextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if ((e.Key < VirtualKey.NumberPad0 || e.Key > VirtualKey.NumberPad9)
                & (e.Key < VirtualKey.Number0 || e.Key > VirtualKey.Number9))
            {
                e.Handled = true;
            }
        }
    }
}
