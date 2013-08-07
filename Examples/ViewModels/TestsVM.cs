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

using Examples.SearchProviders;
namespace Examples.ViewModels
{
    public class TestsVM
    {
        public StandardSearchVM EmptyPathTestVM { get; private set; }
        public StandardSearchVM ValueConvertersTestVM { get; private set; }

        public WorkItem3944VM OneWayToSourceTestVM { get; private set; }

        public StandardSearchVM WorkItem4391TestVM { get; private set; }
        public WorkItem5544VM WorkItem5544TestVM { get; private set; }

        public StandardSearchVM WorkItem6309TestVM { get; private set; }
        public WorkItem6036VM WorkItem6036TestVM { get; private set; }

        public WorkItem6308VM WorkItem6308TestVM { get; private set; }

        public TestsVM()
        {
            EmptyPathTestVM = new StandardSearchVM(new SingleColumnResultsProvider());
            ValueConvertersTestVM = new StandardSearchVM(new SingleColumnResultsProvider());
            OneWayToSourceTestVM = new WorkItem3944VM();
            WorkItem4391TestVM = new StandardSearchVM(new ObjectListProvider());
            WorkItem5544TestVM = new WorkItem5544VM();
            WorkItem6309TestVM = new StandardSearchVM(new WideAndNarrowResultsProvider());
            WorkItem6036TestVM = new WorkItem6036VM();
            WorkItem6308TestVM = new WorkItem6308VM();
        }
    }
}
