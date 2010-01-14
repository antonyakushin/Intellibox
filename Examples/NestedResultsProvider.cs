using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Custom;

namespace Examples {
    public class NestedResultsProvider :ISearchResultsProvider {

        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object tag, Action<DateTime, IEnumerable<object>> whenDone) {
        }

        public void CancelAllSearches() {
        }
    }
}
