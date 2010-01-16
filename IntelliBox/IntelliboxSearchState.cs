using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Windows.Controls {

    /// <summary>
    /// Search state used for Async queries
    /// </summary>
    /// <remarks>
    /// This object is used by the BackgroundWorker to pass state around and make the
    /// required callback to Intellibox
    /// </remarks>
    public class IntelliBoxSearchState {

        /// <summary>
        /// The StartTime passed to the provider from Intellibox
        /// </summary>
        public DateTime StartTimeUtc {
            get;
            set;
        }

        /// <summary>
        /// The Max Results to return to the Intellibox
        /// </summary>
        public int MaxResults {
            get;
            set;
        }

        /// <summary>
        /// State that is provided for the search results.
        /// </summary>
        public object ExtraInfo {
            get;
            set;
        }

        /// <summary>
        /// The results to return to Intellibox
        /// </summary>
        private IEnumerable<object> Results {
            get;
            set;
        }

        /// <summary>
        /// The Callback handler that must be called on Intellibox
        /// </summary>
        public Action<DateTime, IEnumerable<object>> ResultsCallBack {
            get;
            set;
        }

        /// <summary>
        /// The Callback handler that must be called on Intellibox
        /// </summary>
        private Action<IntelliBoxSearchState> SearchCallBack {
            get;
            set;
        }

        /// <summary>
        /// The Search Term
        /// </summary>
        public string SearchTerm {
            get;
            set;
        }

        internal BackgroundWorker Worker {
            get;
            set;
        }

        /// <summary>
        /// Call Search with a given callback
        /// </summary>
        /// <param name="searchCallback">The Delegate to call to perform the search</param>
        public void Search(Action<IntelliBoxSearchState> searchCallback) {

            SearchCallBack = searchCallback;
            BackgroundWorker bw = new BackgroundWorker() {
                WorkerSupportsCancellation = true
            };

            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync(this);

        }

        /// <summary>
        /// To be called with the results when you are done.
        /// </summary>
        /// <param name="results"></param>
        public void Done(IEnumerable<object> results) {
            //do not call the Results callback here since we are still on the background thread
            //right after this is called, the RunWorkerCompleted event will be fired.
            if (results != null) {
                Results = results;
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //if someone did not cancel the search
            if (!e.Cancelled) {
                IntelliBoxSearchState result = e.Result as IntelliBoxSearchState;
                result.ResultsCallBack(result.StartTimeUtc, result.Results);
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e) {
            IntelliBoxSearchState arg = e.Argument as IntelliBoxSearchState;
            arg.SearchCallBack(arg);
            e.Result = this;
        }
    }
}
