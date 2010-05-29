using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Text;

namespace FeserWard.Controls {
    /// <summary>
    /// Takes as input multiple boolean parameters and ORs them together for the final result.
    /// ( will AND them together if the parameter is 'invert' )
    /// </summary>
    public sealed class WatermarkTextVisibilityConverter : IMultiValueConverter {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (values == null || values.Length != 2)
                return DependencyProperty.UnsetValue;

            var text = values[0].ToString();
            var hasFocus = (bool)values[1];
            var hasText = !string.IsNullOrEmpty(text);

            return (hasFocus || hasText)
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException("The boolean OR conversion is a one-way conversion");
        }
    }
}
