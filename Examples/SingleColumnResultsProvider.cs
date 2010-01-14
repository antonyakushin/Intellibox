using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Custom;

namespace Examples {
    public class SingleColumnResultsProvider : ISearchResultsProvider {

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

        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object tag, Action<DateTime, IEnumerable<object>> whenDone) {
            ConstructDataSource();

            whenDone(startTimeUtc,
                _results.Where(term => term.StartsWith(searchTerm)).Take(maxResults).Cast<object>() );
        }

        public void CancelAllSearches() {
            
        }
    }
}
