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
using System.ComponentModel;
using FeserWard.Controls;
using Examples.SearchProviders;

namespace Examples.ViewModels
{
    class Issue5544TestDataContext : INotifyPropertyChanged
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

        private string _dataContextName;
        public string DataContextName {
            get {
                return _dataContextName;
            }
        }

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

        private FeserWard.Controls.IIntelliboxResultsProvider _results;
        public FeserWard.Controls.IIntelliboxResultsProvider queryResults { get { return _results; } }

        private string _value;
        public string valueSelected
        {
            get { return _value; }
            set { if (value != _value) { _value = value; OnPropertyChanged("valueSelected"); } }
        }

        private object _item;
        public object itemSelected
        {
            get { return _item; }
            set { if (value != _item) { _item = value; OnPropertyChanged("itemSelected"); } }
        }

        public Issue5544TestDataContext(string name, IIntelliboxResultsProvider provider = null) {
            _results = provider ?? new Issue5544ResultsProvider();
            _dataContextName = name;
        }

    }
}
