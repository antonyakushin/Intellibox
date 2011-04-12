using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Text;

namespace FeserWard.Controls {
    /// <summary>
    /// Determines whether to show or hide watermark text of a control based on
    /// whether the control has input or focus.
    /// </summary>
    public sealed class WatermarkTextVisibilityConverter : IMultiValueConverter {

        /// <summary>
        /// Determines whether to show or hide watermark text based on whether the
        /// input control has focus or has a value. The first value passed to this
        /// multi-value converter is assumed to be the Text value of the input control,
        /// and the second value whether or not the control has focus.
        /// </summary>
        /// <param name="values">The boolean values to combine</param>
        /// <param name="targetType">The <see cref="System.Type"/> of the return value. Should be <see cref="System.Boolean"/>.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Ignored.</param>
        /// <returns><see cref="Visibility.Collapsed"/> if either of the parameters is true, otherwise <see cref="Visibility.Visible"/>.</returns>
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

        /// <summary>
        /// This function is not supported. The <see cref="WatermarkTextVisibilityConverter"/> is
        /// a one-way conversion. Calling this function will throw a <see cref="NotImplementedException"/>.
        /// </summary>
        /// <param name="value">Not Used.</param>
        /// <param name="targetTypes">Not Used.</param>
        /// <param name="parameter">Not Used.</param>
        /// <param name="culture">Not Used.</param>
        /// <returns>Not Used.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException("Cannot ConvertBack() a WatermarkTextVisibilityConverter.");
        }
    }
}
