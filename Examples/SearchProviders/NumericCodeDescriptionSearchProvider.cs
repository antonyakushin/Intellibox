using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FeserWard.Controls;

namespace Examples.SearchProviders
{
    /// <summary>
    /// Search provider that demonstrates Issue #4 on GitHub.
    /// 
    /// The result set is composed by maxResults 4 digit nuemrical codes and descriptions
    /// 
    /// 
    /// Use this in the Examples VM
    /// 
    ///  SingleColumnVM = new StandardSearchVM(new NumericCodeDescriptionSearchProvider());
    /// 
    /// </summary>
    public class NumericCodeDescriptionSearchProvider : IIntelliboxResultsProvider
    {
        private List<string> _results;
        private const int maxResults = 5000;
        private Random _random = new Random();

        private void ConstructDataSource()
        {
            if (_results == null)
            {
                // results are strings with numeric 4 digit code and decription
                // i.e.:
                // 1244 - My #1244 search result
                List<string> temp = new List<string>();
                for (int i = 0; i < maxResults; ++i)
                {
                    var code = _random.Next(1000, 9999).ToString("D4");
                    var description = string.Concat("My #", code, " search result");
                    temp.Add(string.Concat(code, " - ", description));
                }

                _results = Sort(temp).ToList();
            }
        }
        protected virtual IEnumerable<string> Sort(IEnumerable<string> preResults)
        {
            return preResults.OrderByDescending(s => s.Length);
        }



        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            ConstructDataSource();
            return _results.Where(term => term.StartsWith(searchTerm)).Take(maxResults);
        }
    }
}
