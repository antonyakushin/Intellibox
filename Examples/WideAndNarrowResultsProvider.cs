using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeserWard.Controls;

namespace Examples
{
    class WideAndNarrowResultsProvider : IIntelliboxResultsProvider
    {
        public System.Collections.IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return null;

            if (searchTerm.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
            {
                return new WideAndNarrowResult[] {
                    new WideAndNarrowResult("this is a wide column", "narrow"),
                    new WideAndNarrowResult("indeed a very wide column", "nope"),
                    new WideAndNarrowResult("but it will be narrow", null)
                };
            }

            if (searchTerm.StartsWith("z", StringComparison.InvariantCultureIgnoreCase))
            {
                return new WideAndNarrowResult[] {
                    new WideAndNarrowResult("narrow col", "now it is this column's turn to be wide"),
                    new WideAndNarrowResult(null, "indeed we love! wide columns"),
                    new WideAndNarrowResult("very small", "and this is a massively long column")
                };
            }

            return null;
        }

        class WideAndNarrowResult
        {
            public string Name { get; set; }
            public string Address { get; set; }

            public WideAndNarrowResult(string name, string addr)
            {
                Name = name;
                Address = addr;
            }
        }
    }
}
