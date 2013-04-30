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
using System.ComponentModel;

namespace Examples
{
	class Issue5544TestDataContext2 : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			var e = PropertyChanged;
			if (e != null)
			{
				var args = new PropertyChangedEventArgs(propertyName);
				e.Invoke(this, args);
			}
		}

		public string DataContextName { get { return "Data Context #2"; } }

		private string _greeting;
		public string greetingTxt
		{
			get { return _greeting; }
			set { if (value != _greeting) { _greeting = value; OnPropertyChanged("greetingTxt"); } }
		}

		private string _name;
		public string nameTxt
		{
			get { return _name; }
			set { if (value != _name) { _name = value; OnPropertyChanged("nameTxt"); } }
		}

		private bool _spam;
		public bool sendSpam
		{
			get { return _spam; }
			set { if (value != _spam) { _spam = value; OnPropertyChanged("sendSpam"); } }
		}

		private FeserWard.Controls.IIntelliboxResultsProvider _results = new queryProvider();
		public FeserWard.Controls.IIntelliboxResultsProvider queryResults { get { return _results; } }

		private string _value;
		public string valueSelected
		{
			get { return _value; }
			set { if (value != _value) { _value = value; OnPropertyChanged("valueSelected"); } }
		}

		private string _item;
		public string itemSelected
		{
			get { return _item; }
			set { if (value != _item) { _item = value; OnPropertyChanged("itemSelected"); } }
		}

		private class queryProvider : FeserWard.Controls.IIntelliboxResultsProvider
		{

			public System.Collections.IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
			{
				return new testresult[] {
					new testresult { Name ="name 2-1", Salutation= "salutation 2-1"},
					new testresult { Name ="name 2-2", Salutation= "salutation 2-2"},
					new testresult { Name ="name 2-3", Salutation= "salutation 2-3"}
				};
			}

			private class testresult
			{
				public string Name { get; set; }
				public string Salutation { get; set; }
			}
		}
	}
}
