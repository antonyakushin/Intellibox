using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Examples
{
	class Issue5544TestDataContext1 : INotifyPropertyChanged
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

		public string DataContextName { get { return "Data Context #1"; } }

		private string _greeting;
		public string greetingTxt {
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
				return new result[] {
					new result { Name ="name 1-1", Salutation= "salutation 1-1"},
					new result { Name ="name 1-2", Salutation= "salutation 1-2"},
					new result { Name ="name 1-3", Salutation= "salutation 1-3"}
				};
			}

			private class result
			{
				public string Name { get; set; }
				public string Salutation { get; set; }
			}
		}
	}
}
