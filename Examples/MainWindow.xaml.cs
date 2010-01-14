using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Controls.Custom;

namespace Examples {
    public partial class MainWindow : Window {

        public ISearchResultsProvider NoSearchResults {
            get;
            private set;
        }

        public ISearchResultsProvider SingleColumnResults {
            get;
            private set;
        }

        public ISearchResultsProvider MultiColumnResults {
            get;
            private set;
        }


        public ISearchResultsProvider InvertSingleColumnResults {
            get;
            private set;
        }

        public MainWindow() {
            NoSearchResults = new NoSearchResultsProvider();
            SingleColumnResults = new SingleColumnResultsProvider();
            MultiColumnResults = new MultiColumnResultsProvider();
            InvertSingleColumnResults = new InvertedSingleColumnResultsProvider();

            InitializeComponent();
        }
    }
}
