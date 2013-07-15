using System;
using System.Collections;
using FeserWard.Controls;

namespace Examples.SearchProviders {
    class NotifyingProvider : IIntelliboxResultsProvider {

        public enum SearchStatus {
            Unknown,
            Starting,
            Finished
        }

        private IIntelliboxResultsProvider _provider;
        private Action<string, SearchStatus> _callback;

        public NotifyingProvider(IIntelliboxResultsProvider wrappedProvider, Action<string, SearchStatus> callback = null) {
            if (wrappedProvider == null)
                throw new ArgumentNullException("wrappedProvider");

            _provider = wrappedProvider;
            _callback = callback;
        }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo) {
            if (_callback != null) {
                _callback(searchTerm, SearchStatus.Starting);
            }

            var results = _provider.DoSearch(searchTerm, maxResults, extraInfo);

            if (_callback != null) {
                _callback(searchTerm, SearchStatus.Finished);
            }

            return results;
        }
    }
}
