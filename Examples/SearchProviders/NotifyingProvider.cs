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
