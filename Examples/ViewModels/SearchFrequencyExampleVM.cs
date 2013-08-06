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
using System.Collections.ObjectModel;
using Examples.SearchProviders;

namespace Examples.ViewModels {
    public class SearchFrequencyExampleVM
    {

        public StandardSearchVM DelayedSearch {
            get;
            private set;
        }

        public ObservableCollection<string> SearchNotifications {
            get;
            private set;
        }

        public SearchFrequencyExampleVM() {
            DelayedSearch = new StandardSearchVM(new NotifyingProvider(new SingleColumnResultsProvider(), HandleSearchNotifications));
            SearchNotifications = new ObservableCollection<string>();
        }

        private void HandleSearchNotifications(string searchTerm, NotifyingProvider.SearchStatus status) {
            if (SearchNotifications == null)
                SearchNotifications = new ObservableCollection<string>();

            if (status == NotifyingProvider.SearchStatus.Starting) {
                SearchNotifications.Add(string.Format("Beginning new search at {0:T} with term {1}.", DateTime.Now, searchTerm));
            }
            else if (status == NotifyingProvider.SearchStatus.Finished) {
                SearchNotifications.Add(string.Format("Finished search with term {1} at {0:T}.", DateTime.Now, searchTerm));
            }
        }

    }
}
