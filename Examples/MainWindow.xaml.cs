﻿/*
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
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Examples {

    public partial class MainWindow : Window, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        public IIntelliboxResultsProvider InvertSingleColumnResults {
            get;
            private set;
        }

        public IIntelliboxResultsProvider MultiColumnResults {
            get;
            private set;
        }

        public IIntelliboxResultsProvider MultiColumnResultsRss {
            get;
            private set;
        }

        public IIntelliboxResultsProvider NoSearchResults {
            get;
            private set;
        }

        public IIntelliboxResultsProvider SingleColumnResults {
            get;
            private set;
        }

        public IIntelliboxResultsProvider SqlServerCeProvider {
            get;
            private set;
        }

        public ObservableCollection<string> SearchBeginNotifications {
            get;
            set;
        }

        private string _personFirstNameForTwoWay;
        public string PersonFirstNameForTwoWay {
            get {
                return _personFirstNameForTwoWay;
            }
            set {
                if (_personFirstNameForTwoWay != value) {
                    _personFirstNameForTwoWay = value;
                    OnPropertyChanged("PersonFirstNameForTwoWay");
                }
            }
        }

        private string _personFirstNameForOneWay;
        public string PersonFirstNameForOneWay {
            get {
                return _personFirstNameForOneWay;
            }
            set {
                if (_personFirstNameForOneWay != value) {
                    _personFirstNameForOneWay = value;
                    OnPropertyChanged("PersonFirstNameForOneWay");
                }
            }
        }

        public MainWindow() {
            InvertSingleColumnResults = new InvertedSingleColumnResultsProvider();
            MultiColumnResults = new MultiColumnResultsProvider();
            MultiColumnResultsRss = new RSSFeedResultsProvider();
            NoSearchResults = new NoSearchResultsProvider();
            SingleColumnResults = new SingleColumnResultsProvider();
            SqlServerCeProvider = new LightspeedResultsProvider();
            SearchBeginNotifications = new ObservableCollection<string>();

            InitializeComponent();
        }

        private void OnPropertyChanged(string propertyName) {
            var e = PropertyChanged;
            if (e != null) {
                e(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void searchDelayBox_SearchBeginning(string arg1, int arg2, object arg3) {
            var text = string.Format("New search. Text:{{{0}}}", arg1);
            SearchBeginNotifications.Add(text);
        }
    }
}
