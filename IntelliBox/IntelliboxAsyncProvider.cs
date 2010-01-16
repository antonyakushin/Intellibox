using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Windows.Controls {

    /// <summary>
    /// Provide a wrapper method so that people do not need to implement an async provider
    /// </summary>
    internal class IntelliboxAsyncProvider {

        private Func<string, int, object, IEnumerable<object>> callback;

        private Dictionary<searchdata, BackgroundWorker> activesearches
            = new Dictionary<searchdata, BackgroundWorker>();
        
        private object LockObject = new object();

        public IntelliboxAsyncProvider(Func<string, int, object, IEnumerable<object>> doesTheActualSearch) {
            if (doesTheActualSearch == null)
                throw new ArgumentNullException("doesTheActualSearch");

            callback = doesTheActualSearch;
        }

        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults, object extraInfo,
            Action<DateTime, IEnumerable<object>> whenDone) {

            var data = new searchdata() {
                extra = extraInfo,
                max = maxResults,
                searchTerm = searchTerm,
                startTimeUtc = startTimeUtc,
                whendone = whenDone
            };
            lock (LockObject) {
                BackgroundWorker wrk = new BackgroundWorker();
                wrk.DoWork += new DoWorkEventHandler(wrk_DoWork);
                wrk.RunWorkerCompleted += new RunWorkerCompletedEventHandler(wrk_RunWorkerCompleted);
                activesearches.Add(data, wrk);
                wrk.RunWorkerAsync(data);
            }
        }

        void wrk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            var data = e.Result as searchdata;
            
            lock (LockObject) {
                activesearches.Remove(data);
            }
            
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

        private class searchdata {
            public string searchTerm;
            public DateTime startTimeUtc;
            public int max;
            public object extra;
            public Action<DateTime, IEnumerable<object>> whendone;
            public IEnumerable<object> results;
        }
    }
}
