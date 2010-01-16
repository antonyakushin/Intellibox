using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Windows.Controls {
    class ViewModel {

        public ViewModel() {
            Provider = new AsyncProvider(getresults);
        }

        private IEnumerable<object> getresults(string term, int max, object extra) {
            return null;
        }

        public AsyncProvider Provider {
            get;
            set;
        }
    }

    class AsyncProvider : IIntellibboxResultsProvider {

        private Func<string, int, object, IEnumerable<object>> callback;
        private Dictionary<Guid, BackgroundWorker> activesearches;

        public AsyncProvider(Func<string, int, object, IEnumerable<object>> doesTheActualSearch) {
            if (doesTheActualSearch == null)
                throw new ArgumentNullException("doesTheActualSearch");

            callback = doesTheActualSearch;
        }

        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object extraInfo, Action<DateTime, IEnumerable<object>> whenDone) {

            searchdata data = new searchdata();
            var guid = new Guid();

            BackgroundWorker wrk = new BackgroundWorker();
            wrk.DoWork += new DoWorkEventHandler(wrk_DoWork);
            wrk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(wrk_RunWorkerCompleted);


            activesearches.Add(guid, wrk);

            wrk.RunWorkerAsync(data);
        }

        void wrk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            var data = e.Result as searchdata;
            activesearches.Remove(data.ID);

            if (!e.Cancelled) {
                data.whendone(data.startTimeUtc, data.results);
            }
        }

        void wrk_DoWork(object sender, DoWorkEventArgs e) {
            var data = e.Argument as searchdata;


            data.results = callback(data.searchTerm, data.max, data.extra);

            e.Result = data;
        }

        

        public void CancelAllSearches() {
            var list = activesearches.Keys.ToArray();
            for (int i = 0; i < list.Length; i++) {
                activesearches[list[i]].CancelAsync();
            }
        }

        


        class searchdata {
            public string searchTerm;
            public DateTime startTimeUtc;
            public int max;
            public object extra;
            public Guid ID;
            public Action<DateTime, IEnumerable<object>> whendone;
            public IEnumerable<object> results;
        }
    }
}
