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
using Caliburn.Micro;
using System.ComponentModel;
using Examples.SearchProviders;

namespace Examples.ViewModels
{
    public class WorkItem5544VM : PropertyChangedBase
    {
        private Issue5544TestDataContext _dataContext1;
        private Issue5544TestDataContext _dataContext2;
        private Issue5544TestDataContext _dataContext3;
        private Issue5544TestDataContext _dataContext4;

        private INotifyPropertyChanged _firstContext;

        public INotifyPropertyChanged FirstDataContextSet
        { 
            get { 
                return _firstContext; 
            } 
            private set { 
                if (value != _firstContext) { 
                    _firstContext = value;
                    NotifyOfPropertyChange(() => FirstDataContextSet);

                } 
            } 
        }

        private INotifyPropertyChanged _secondContext;

        public INotifyPropertyChanged SecondDataContextSet {
            get {
                return _secondContext;
            }
            private set {
                if (value != _secondContext) {
                    _secondContext = value;
                    NotifyOfPropertyChange(() => SecondDataContextSet);
                }
            }
        }

        bool _onfirstDataContextSet = false;
        public void SwapDataContexts()
        {
            _onfirstDataContextSet = !_onfirstDataContextSet;
            if (_onfirstDataContextSet) {
                FirstDataContextSet = _dataContext1;
                SecondDataContextSet = _dataContext3;
            }
            else {
                FirstDataContextSet = _dataContext2;
                SecondDataContextSet = _dataContext4;
            }
        }

        public WorkItem5544VM()
        {
            var provider = new Issue5544ResultsProvider();
            _dataContext1 = new Issue5544TestDataContext("Data Context #1", provider);
            _dataContext2 = new Issue5544TestDataContext("Data Context #2", provider);

            _dataContext3 = new Issue5544TestDataContext("Data Context #3", provider);
            _dataContext4 = new Issue5544TestDataContext("Data Context #4", provider);

            SwapDataContexts();
        }
    }
}
