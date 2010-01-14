using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Custom;

namespace Examples {
    public class NoSearchResultsProvider : ISearchResultsProvider {
        
        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object tag, Action<DateTime, IEnumerable<object>> whenDone) {
            whenDone(startTimeUtc, new List<object>());
        }

        public void CancelAllSearches() {
        }
    }
}
