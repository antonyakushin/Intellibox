using System.Collections.Generic;
using System.Linq;

namespace Examples {
    class InvertedSingleColumnResultsProvider : SingleColumnResultsProvider {
        protected override IEnumerable<string> Sort(IEnumerable<string> preResults) {
            return preResults.OrderBy(s => s.Length);
        }
    }
}
