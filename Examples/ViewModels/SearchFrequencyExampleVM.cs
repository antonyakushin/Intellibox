using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
