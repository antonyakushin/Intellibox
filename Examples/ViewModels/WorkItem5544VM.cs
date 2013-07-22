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
using Caliburn.Micro;
using System.ComponentModel;

namespace Examples.ViewModels
{
    public class WorkItem5544VM : PropertyChangedBase
    {
        private Issue5544TestDataContext1 _dataContext1;
        private Issue5544TestDataContext2 _dataContext2;
        private INotifyPropertyChanged _currentContext;

        public INotifyPropertyChanged CurrentDataContext
        { 
            get { 
                return _currentContext; 
            } 
            private set { 
                if (value != _currentContext) { 
                    _currentContext = value;
                    NotifyOfPropertyChange(() => CurrentDataContext);

                } 
            } 
        }

        public bool CanSwapDataContexts { get { return true; } }

        public void SwapDataContexts()
        {
            CurrentDataContext = (CurrentDataContext == _dataContext1)
                ? (INotifyPropertyChanged)_dataContext2
                : _dataContext1;
        }

        public WorkItem5544VM()
        {
            _dataContext1 = new Issue5544TestDataContext1();
            _dataContext2 = new Issue5544TestDataContext2();

            CurrentDataContext = _dataContext1;
        }
    }
}
