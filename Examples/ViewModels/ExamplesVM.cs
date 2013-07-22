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

namespace Examples.ViewModels {
    public class ExamplesVM {
        public StandardSearchVM NoResultsVM {
            get;
            private set;
        }

        public StandardSearchVM SingleColumnVM {
            get;
            private set;
        }

        public StandardSearchVM LimitingResultsVM {
            get;
            private set;
        }

        public StandardSearchVM MultipleColumnVM {
            get;
            private set;
        }

        public StandardSearchVM CustomizingColumnsVM {
            get;
            private set;
        }

        public StandardSearchVM ChangeColumnPositionVM {
            get;
            private set;
        }

        public StandardSearchVM ORMExampleVM {
            get;
            private set;
        }

        public SearchFrequencyExampleVM SearchFrequencyVM {
            get;
            private set;
        }

        public LengthySearchExampleVM LengthySearchesVM {
            get;
            private set;
        }

        public StandardSearchVM WatermarkVM {
            get;
            private set;
        }

        public ExamplesVM() {
            NoResultsVM = new StandardSearchVM(new NoSearchResultsProvider());
            SingleColumnVM = new StandardSearchVM(new SingleColumnResultsProvider());
            LimitingResultsVM = new StandardSearchVM(new InvertedSingleColumnResultsProvider());
            MultipleColumnVM = new StandardSearchVM(new MultiColumnResultsProvider());
            CustomizingColumnsVM = new StandardSearchVM(new MultiColumnResultsProvider());
            ChangeColumnPositionVM = new StandardSearchVM(new MultiColumnResultsProvider());
            ORMExampleVM = new StandardSearchVM(new LightspeedResultsProvider());
            SearchFrequencyVM = new SearchFrequencyExampleVM();
            LengthySearchesVM = new LengthySearchExampleVM();
            WatermarkVM = new StandardSearchVM(new SingleColumnResultsProvider());
        }
    }
}
