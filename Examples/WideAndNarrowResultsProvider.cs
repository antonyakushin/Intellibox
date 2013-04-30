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
using System.Text;
using FeserWard.Controls;

namespace Examples
{
    class WideAndNarrowResultsProvider : IIntelliboxResultsProvider
    {
        public System.Collections.IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return null;

            if (searchTerm.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
            {
                return new WideAndNarrowResult[] {
                    new WideAndNarrowResult("this is a wide column", "narrow"),
                    new WideAndNarrowResult("indeed a very wide column", "nope"),
                    new WideAndNarrowResult("but it will be narrow", null)
                };
            }

            if (searchTerm.StartsWith("z", StringComparison.InvariantCultureIgnoreCase))
            {
                return new WideAndNarrowResult[] {
                    new WideAndNarrowResult("narrow col", "now it is this column's turn to be wide"),
                    new WideAndNarrowResult(null, "indeed we love! wide columns"),
                    new WideAndNarrowResult("very small", "and this is a massively long column")
                };
            }

            return null;
        }

        class WideAndNarrowResult
        {
            public string Name { get; set; }
            public string Address { get; set; }

            public WideAndNarrowResult(string name, string addr)
            {
                Name = name;
                Address = addr;
            }
        }
    }
}
