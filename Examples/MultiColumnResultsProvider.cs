using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Custom;

namespace Examples {
    public class MultiColumnResultsProvider : ISearchResultsProvider {
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
