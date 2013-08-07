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
using Examples.SearchProviders;
using FeserWard.Controls;

namespace Examples.ViewModels
{
    public class WorkItem6308VM : PropertyChangedBase
    {

        private WorkItem6308ResultsProvider.AlphabetCharacter _selectedItem;
        public WorkItem6308ResultsProvider.AlphabetCharacter MyItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    this.NotifyOfPropertyChange(() => MyItem);
                }
            }
        }

        private IIntelliboxResultsProvider _queryProvider;
        public IIntelliboxResultsProvider QueryProvider { get { return _queryProvider; } private set { if (value != _queryProvider) { _queryProvider = value; this.NotifyOfPropertyChange(() => QueryProvider); } } }

        public WorkItem6308VM()
        {
            _queryProvider = new WorkItem6308ResultsProvider();
        }

        public void ChangeMyItem()
        {
            // try the letter 'A' first
            var newItem = new WorkItem6308ResultsProvider.AlphabetCharacter(0);
            if (newItem == MyItem)
            {
                // if 'A' is already selected, then use 'Z' instead
                newItem = new WorkItem6308ResultsProvider.AlphabetCharacter(25);
            }

            MyItem = newItem;
        }

    }
}
