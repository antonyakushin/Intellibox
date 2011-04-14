using FeserWard.Controls;
using System.Collections.Generic;
using System.Collections;

namespace Examples {
    public class ObjectListProvider : IIntelliboxResultsProvider {

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo) {
            return new object[] { 
                new object[] { 1, 2 },
                new object[] { 3, 4 },
                new object[] { 5, 6 }

            };
        }
    }
}