using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Custom;

namespace Examples {
    public partial class BasicSearchResultsCtrl : UserControl {
        public static readonly DependencyProperty ProviderProperty =
            DependencyProperty.Register("Provider", typeof(ISearchResultsProvider), typeof(BasicSearchResultsCtrl), new UIPropertyMetadata(null));

        public ISearchResultsProvider Provider {
            get {
                return (ISearchResultsProvider)GetValue(ProviderProperty);
            }
            set {
                SetValue(ProviderProperty, value);
            }
        }

        public BasicSearchResultsCtrl() {
            InitializeComponent();
        }
    }
}
