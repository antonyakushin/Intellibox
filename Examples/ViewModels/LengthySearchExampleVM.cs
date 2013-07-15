using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using FeserWard.Controls;
using System.Collections;
using Examples.SearchProviders;

namespace Examples.ViewModels {
    class LengthySearchExampleVM {

        public StandardSearchVM LongSearch {
            get;
            private set;
        }

        public ObservableCollection<string> SearchNotifications {
            get;
            private set;
        }

        public LengthySearchExampleVM() {
            LongSearch = new StandardSearchVM(new NotifyingProvider(new DelayedResultsProvider(), HandleSearchNotifications));
            SearchNotifications = new ObservableCollection<string>();
        }

        private void HandleSearchNotifications(string searchTerm, NotifyingProvider.SearchStatus searchStatus) {
            if (SearchNotifications == null)
                SearchNotifications = new ObservableCollection<string>();

            if (searchStatus == NotifyingProvider.SearchStatus.Starting) {
                SearchNotifications.Add(string.Format("Beginning new search at {0:T} with term {1}.", DateTime.Now, searchTerm));
            }
            else if (searchStatus == NotifyingProvider.SearchStatus.Finished) {
                SearchNotifications.Add(string.Format("Finished search with term {1} at {0:T}.", DateTime.Now, searchTerm));
            }
        }
    }
}
