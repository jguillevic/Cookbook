using System.Globalization;
using Windows.System;
using Windows.UI.Xaml;

namespace Tools.Component.Extension.TextBox
{
    public class OnlyDecimalTBExt : DependencyObject
    {
        public static readonly DependencyProperty OnlyDecimalProperty =
        DependencyProperty.RegisterAttached(
          "OnlyDecimal",
          typeof(bool),
          typeof(OnlyDecimalTBExt),
          new PropertyMetadata(false, OnOnlyDecimalChanged)
        );

        public static bool GetOnlyDecimal(DependencyObject obj)
        {
            return (bool)obj.GetValue(OnlyDecimalProperty);
        }

        public static void SetOnlyDecimal(DependencyObject obj, bool value)
        {
            obj.SetValue(OnlyDecimalProperty, value);
        }

        private static void OnOnlyDecimalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
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
                && (e.Key < VirtualKey.Number0 || e.Key > VirtualKey.Number9)
                && e.Key != VirtualKey.Decimal)
            {
                e.Handled = true;
                return;
            }

            if (e.Key == VirtualKey.Decimal)
            {
                if ((sender as Windows.UI.Xaml.Controls.TextBox).Text.IndexOf(CultureInfo.InvariantCulture.NumberFormat.CurrencyDecimalSeparator) != -1)
                    e.Handled = true;
            }
        }
    }
}
