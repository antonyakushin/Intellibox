using System.Collections.Generic;

namespace System.Windows.Controls.Custom {
    public interface ISearchResultsProvider {
        void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object extraInfo, Action<DateTime, IEnumerable<object>> whenDone);
        void CancelAllSearches();
    }
}
