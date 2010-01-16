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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Windows.Controls;
using System.ComponentModel;

namespace Examples {

    public class RSSFeedResultsProvider : IIntellibboxResultsProvider {

        #region Data "Access" of results

        private XmlDataDocument _XmlDoc;

        private XmlDataDocument XmlDoc {
            get {
                if (_XmlDoc == null) {
                    try {
                        //we are going to show how we can use an rss feed to obtain data and then filter based on the name
                        string url = "http://maps.yahoo.com/traffic.rss?csz=10101&mag=5&minsev=1";

                        WebRequest req = WebRequest.Create(url);
                        WebResponse res = req.GetResponse();

                        Stream rsstream = res.GetResponseStream();
                        System.Xml.XmlDataDocument rssdoc = new System.Xml.XmlDataDocument();

                        rssdoc.Load(rsstream);
                        _XmlDoc = rssdoc;
                    }
                    catch (Exception) {
                        //do nothing, bad stuff happened.
                    }
                }
                return _XmlDoc;
            }
        }

        /// <summary>
        /// Take the data from the RSS feed and create the result objects
        /// </summary>
        /// <returns></returns>
        private List<Result> getRSSfeed() {

            List<Result> retVal = new List<Result>();

            System.Xml.XmlNodeList rssitems = XmlDoc.SelectNodes("rss/channel/item");

            for (int i = 0; i < rssitems.Count; i++) {
                System.Xml.XmlNode rssdetail;

                rssdetail = rssitems.Item(i).SelectSingleNode("title");
                //this was aquired from a micosoft example on msdn.
                if (rssdetail != null) {
                    var result = new Result();
                    retVal.Add(result);

                    result.Title = rssdetail.InnerText;

                    rssdetail = rssitems.Item(i).SelectSingleNode("description");
                    result.Description = rssdetail.InnerText;

                    rssdetail = rssitems.Item(i).SelectSingleNode("link");
                    result.Link = (rssdetail != null) ? rssdetail.InnerText : "";

                    rssdetail = rssitems.Item(i).SelectSingleNode("category");
                    result.Category = (rssdetail != null) ? rssdetail.InnerText : "";

                    rssdetail = rssitems.Item(i).SelectSingleNode("severity");
                    result.Severity = (rssdetail != null) ? rssdetail.InnerText : "";

                    //just trim for now.
                    result.Category = (result.Category ?? string.Empty).Trim();
                    result.Description = (result.Description ?? string.Empty).Trim();
                    result.Link = (result.Link ?? string.Empty).Trim();
                    result.Severity = (result.Severity ?? string.Empty).Trim();
                    result.Title = (result.Title ?? string.Empty).Trim();
                }
            }
            return retVal;
        }

        #endregion Data "Access" of results

        #region ISearchResultsProvider Members

        public void BeginSearchAsync(string searchTerm, DateTime startTimeUtc, int maxResults,
            object extraInfo, Action<DateTime, IEnumerable<object>> whenDone) {

            //This is the object that I am thinking we pass into the BeginSearchAsync Method
            var state = new IntelliBoxSearchState() {
                ResultsCallBack = whenDone,
                ExtraInfo = extraInfo,
                MaxResults = maxResults,
                SearchTerm = searchTerm,
                StartTimeUtc = startTimeUtc
            };

            //Call Search with the local callback that will be called on a background thread.
            state.Search(SearchCallback);

            //BackgroundWorker bw = new BackgroundWorker() {
            //    WorkerSupportsCancellation = true
            //};

            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //bw.RunWorkerAsync(state);
        }

        /// <summary>
        /// Search for the results and Call Done
        /// </summary>
        /// <param name="state"></param>
        public void SearchCallback(IntelliBoxSearchState state) {
            var results = getRSSfeed()
                .Where(term => term.Title.IndexOf(state.SearchTerm, StringComparison.OrdinalIgnoreCase) > -1
                    || term.Description.IndexOf(state.SearchTerm, StringComparison.OrdinalIgnoreCase) > -1)
                    .Take(state.MaxResults).Cast<object>();
            //Tell the State that we are done and pass in the results.
            state.Done(results);
        }

        //void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
        //    //if someone did not cancel the search
        //    if (!e.Cancelled) {
        //        IntelliBoxSearchState result = e.Result as IntelliBoxSearchState;
        //        result.CallBack(result.StartTimeUtc, result.Results); 
        //    }
        //}

        //void bw_DoWork(object sender, DoWorkEventArgs e) {

        //    IntelliBoxSearchState arg = e.Argument as IntelliBoxSearchState;
        //    var results = getRSSfeed();
        //    arg.Results =results
        //            .Where(term => term.Title.IndexOf(arg.SearchTerm, StringComparison.OrdinalIgnoreCase) > -1
        //            || term.Description.IndexOf(arg.SearchTerm, StringComparison.OrdinalIgnoreCase) > -1)
        //            .Take(arg.MaxResults).Cast<object>();
        //    e.Result = arg;
        //}

        public void CancelAllSearches() {

        }

        #endregion

        #region Result Class

        private class Result {

            public string Title {
                get;
                set;
            }

            public string Description {
                get;
                set;
            }

            public string Link {
                get;
                set;
            }

            public string Category {
                get;
                set;
            }

            public string Severity {
                get;
                set;
            }

            public override string ToString() {
                return Title;
            }
        }

        #endregion Result Class
    }
}
