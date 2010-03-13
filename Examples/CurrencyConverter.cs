/*
Copyright (c) 2010 Stephen P Ward and Joseph E Feser

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Windows.Data;

namespace IntelliBox.Examples {

    public class CurrencyConverter : IValueConverter {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || System.Convert.IsDBNull(value)) {
                return null;
            }

            int param = 0;
            if (parameter != null) {
                int.TryParse(parameter.ToString(), out param);
            }

            if (value is decimal || value is float || value is double) {
                var retVal = System.Convert.ToDecimal(value);
                if (param == 1)
                    return retVal.ToString("N2");
                return retVal.ToString("C");
            }

            decimal rt;
            if (decimal.TryParse(value.ToString(), out rt)) {
                if (param == 1)
                    return rt.ToString("N2");
                return rt.ToString("C");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || System.Convert.IsDBNull(value)) {
                return null;
            }
            decimal rt;
            if (decimal.TryParse(value.ToString(), System.Globalization.NumberStyles.Currency, null, out rt)) {
                return rt;
            }
            return null;

        }

        #endregion
    }
}
