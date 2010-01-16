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
using System.Windows.Controls;

namespace Examples {
    public class MultiColumnResultsProvider : IIntellibboxResultsProvider {
        private List<Person> _results;

        private void ConstructResultSet() {
            _results = Enumerable.Range(0, 30).Select(i => {
                var r = new Random(i);

                return new Person() {
                    PersonID = i,
                    FirstName = r.Next().ToString() + "'s firstname",
                    LastName = r.Next().ToString() + "'s lastname",
                    Age = r.Next(10, 200),
                    NetWorth = r.Next(10000, 10000000),
                    Weight = r.Next(100, 400)
                };
            }).ToList();
        }


        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object tag, Action<DateTime, IEnumerable<object>> whenDone) {
            ConstructResultSet();
            whenDone(startTimeUtc, _results.Cast<object>());
        }

        public void CancelAllSearches() {
            
        }

        private class Person {
            public int PersonID {
                get;
                set;
            }

            public string FirstName {
                get;
                set;
            }

            public string LastName {
                get;
                set;
            }

            public int Age {
                get;
                set;
            }

            public decimal NetWorth {
                get;
                set;
            }

            public int Weight {
                get;
                set;
            }

            public override string ToString() {
                return string.Format("ID:{0}, FName:{1}, LName:{2}, Age:{3}, NetWorth:{4}, Weight:{5}",
                    PersonID,
                    FirstName ?? string.Empty,
                    LastName ?? string.Empty,
                    Age,
                    NetWorth,
                    Weight);
            }
        }

    }
}
