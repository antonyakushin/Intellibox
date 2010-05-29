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
using System.Collections.Generic;
using System.Linq;
using FeserWard.Controls;
using System.Collections;

namespace Examples {

    public class SingleColumnResultsProvider : IIntelliboxResultsProvider {

        private List<string> _results;
        private int _numEach = 10;

        private void ConstructDataSource() {
            if (_results == null) {

                var temp = Enumerable.Range(0, 26 * _numEach).Select(i => {
                    var count = i % _numEach + 1;
                    var charNum = (i / _numEach) % 26;
                    char ch = Convert.ToChar(charNum + Convert.ToInt32('a'));
                    return "".PadRight(count, ch);
                });

                _results = Sort(temp).ToList();
            }
        }

        protected virtual IEnumerable<string> Sort(IEnumerable<string> preResults) {
            return preResults.OrderByDescending(s => s.Length);
        }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object tag) {
            ConstructDataSource();
            return _results.Where(term => term.StartsWith(searchTerm)).Take(maxResults);
        }
    }
}
