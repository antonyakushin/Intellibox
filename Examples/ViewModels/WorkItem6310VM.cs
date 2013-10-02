using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeserWard.Controls;
using Caliburn.Micro;

namespace Examples.ViewModels {
	public class WorkItem6310VM : PropertyChangedBase {

		private IIntelliboxResultsProvider _queryProvider;
		public IIntelliboxResultsProvider QueryProvider {
			get {
				return _queryProvider;
			}
			private set {
				if (value != _queryProvider) {
					_queryProvider = value;
					this.NotifyOfPropertyChange(() => QueryProvider);
				}
			}
		}

		private bool _openResults;
		public bool SelectSingleResult {
			get {
				return _openResults;
			}
			set {
				if (value != _openResults) {
					_openResults = value;
					this.NotifyOfPropertyChange(() => SelectSingleResult);
				}
			}
		}

		public WorkItem6310VM() {
			_queryProvider = new WorkItem6310Provider();
		}

		private class WorkItem6310Provider : IIntelliboxResultsProvider {

			public System.Collections.IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo) {
				if (string.IsNullOrEmpty(searchTerm)) {
					return new string[0];
				}

				// return a list of searchterm.Length items, up to 26 items max
				var numItems = Math.Min(searchTerm.Length, 26);
				return Enumerable.Range(0, numItems).Select(i => "".PadRight(numItems, Convert.ToChar(i + Convert.ToInt32('a')))).ToArray();
			}
		}

	}
}
