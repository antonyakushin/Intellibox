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
using FeserWard.Controls;
using Examples.SearchProviders;

namespace Examples.ViewModels
{
    public class WorkItem3944VM : PropertyChangedBase
    {
        private object _person;
        public object PersonObjectForOneWay { get { return _person; } set { if (value != _person) { _person = value; NotifyOfPropertyChange(() => PersonObjectForOneWay); } } }

        private string _name;
        public string PersonFirstNameForOneWay { get { return _name; } set { if (_name != value) { _name = value; NotifyOfPropertyChange(() => PersonFirstNameForOneWay); } } }

        private IIntelliboxResultsProvider _queryProvider;
        public IIntelliboxResultsProvider QueryProvider { get { return _queryProvider; } private set { if (value != _queryProvider) { _queryProvider = value; this.NotifyOfPropertyChange(() => QueryProvider); } } }


        public WorkItem3944VM()
        {
            QueryProvider = new MultiColumnResultsProvider();
        }
    }
}
