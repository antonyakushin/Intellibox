/*
Copyright (c) 2010 Stephen Ward and Joseph E Feser

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
using System.Windows;
using System.Windows.Controls;

namespace System.Windows.Controls.Custom {
    public class DataColumn : GridViewColumn {
        public static readonly DependencyProperty ForPropertyProperty =
            DependencyProperty.Register("ForProperty", typeof(string), typeof(DataColumn), new UIPropertyMetadata(null));

        public static readonly DependencyProperty HideProperty =
            DependencyProperty.Register("Hide", typeof(bool), typeof(DataColumn), new UIPropertyMetadata(false));

        public string ForProperty {
            get {
                return (string)GetValue(ForPropertyProperty);
            }
            set {
                SetValue(ForPropertyProperty, value);
            }
        }

        public bool Hide {
            get {
                return (bool)GetValue(HideProperty);
            }
            set {
                SetValue(HideProperty, value);
            }
        }

        public int? Position {
            get;
            set;
        }
    }
}
