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
using System.Windows.Media;

namespace FeserWard.Controls {
    /// <summary>
    /// Provides a simple way to color the even rows of a ListBox with one color and the odd rows with another.
    /// </summary>
    public sealed class IntelliboxAlternateRowColorizer : IntelliboxRowColorizer {

        /// <summary>
        /// The <see cref="Brush"/> to use on even numbered rows (0,2,4,6,...)
        /// </summary>
        public Brush EvenRowBrush {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="Brush"/> to use on odd numbered rows (1,3,5,7,...)
        /// </summary>
        public Brush OddRowBrush {
            get;
            set;
        }

        /// <summary>
        /// Returns the brushes that this object wants to use to colorize the background of each row in a ListBox control.
        /// </summary>
        /// <returns>An array that contains the values of the <see cref="EvenRowBrush"/> and <see cref="OddRowBrush"/> at the time of the call.</returns>
        protected override Brush[] GetBrushes() {
            return new[] { EvenRowBrush, OddRowBrush };
        }
    }
}
