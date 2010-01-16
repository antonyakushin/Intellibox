﻿/*
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
using System.Windows.Data;
using System.Windows.Media;

namespace System.Windows.Controls.Custom {
    /// <summary>
    /// Provides an abstract implementation of an <see cref="IValueConverter"/> that converts a ListBoxItem
    /// into a Brush based on the index of the ListBoxItem in its parent collection.
    /// </summary>
    public abstract class AbstractRowColorizer : IValueConverter {

        protected abstract Brush[] GetBrushes();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var boxItem = value as ListBoxItem;
            if (boxItem != null) {
                var listCtrl = ItemsControl.ItemsControlFromItemContainer(boxItem);
                if (listCtrl != null) {
                    var index = listCtrl.ItemContainerGenerator.IndexFromContainer(boxItem);

                    var brushes = GetBrushes();
                    if (brushes == null || brushes.Length < 1)
                        return null;

                    return brushes[index % brushes.Length];
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new InvalidOperationException("Impossible to convert from a Brush to a ListViewItem!");
        }
    }
}