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
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using FeserWard.Controls;

namespace Examples {
    public class DelayedResultsProvider : IIntelliboxResultsProvider {

        private static string[] _searchResults = Enumerable.Range(0, 26 * 5)
            .Select(number => {
                var alphaChar = Convert.ToChar((number % 26) + Convert.ToInt32('a'));
                return new StringBuilder(5)
                    .Append(alphaChar, (number / 5) + 1)
                    .ToString();
            })
            .ToArray();

        public int MillisecondDelay {
            get;
            set;
        }

        public DelayedResultsProvider() : this(5000) {
            
        }

        public DelayedResultsProvider(int delayMS) {
            MillisecondDelay = delayMS < 0 ? 1000 : delayMS;
        }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo) {
            Thread.Sleep(MillisecondDelay);
            return from s in _searchResults
                   where s.StartsWith(searchTerm)
                   select s;

        }
    }
}
