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

namespace System.Windows.Controls.Custom {
    /// <summary>
    /// Represents a column that displays data from an <see cref="ISearchResultsProvider"/> result set.
    /// </summary>
    public class DataColumn : GridViewColumn {
        /// <summary>
        /// Associates this column with a property on a result set data row. The data row property name that 
        /// matches this string will be hidden or positioned based on the <seealso cref="Hide"/> and <seealso cref="Position"/> values.
        /// </summary>
        public string ForProperty {
            get;
            set;
        }

        /// <summary>
        /// When <see cref="True"/>, this column will not be shown in the result set. This property is useful
        /// if you only want to hide a few columns of the result set; otherwise you're
        /// probably better off just listing all the columns explicitly.
        /// </summary>
        public bool Hide {
            get;
            set;
        }

        /// <summary>
        /// When set, controls the left-to-right position of this column in the result set.
        /// Lower numbers sort farther to the left. NULL values sort to the right.
        /// </summary>
        public int? Position {
            get;
            set;
        }
    }
}
