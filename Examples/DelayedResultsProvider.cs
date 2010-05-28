using System;
using System.Collections.Generic;
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

        public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object extraInfo) {
            Thread.Sleep(MillisecondDelay);
            return from s in _searchResults
                   where s.StartsWith(searchTerm)
                   select s as object;

        }
    }
}
