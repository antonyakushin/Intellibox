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
using FeserWard.Controls;

namespace Examples.SearchProviders {
	public class Issue5544ResultsProvider : IIntelliboxResultsProvider {

		public System.Collections.IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo) {
			return new testresult[] {
					new testresult { Name ="name 2-1", Salutation= "salutation 2-1"},
					new testresult { Name ="name 2-2", Salutation= "salutation 2-2"},
					new testresult { Name ="name 2-3", Salutation= "salutation 2-3"}
				};
		}

		private class testresult {
			public string Name {
				get;
				set;
			}
			public string Salutation {
				get;
				set;
			}

			public override string ToString() {
				return string.Format("Name: '{0}', Salutation: '{1}'", Name, Salutation);
			}
		}

	}
}
