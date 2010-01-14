using System.Windows.Data;
using System.Windows.Media;

namespace System.Windows.Controls.Custom {
    class AlternateBackgroundConverter : IValueConverter {

        public Brush EvenRowBrush {
            get;
            set;
        }

        public Brush OddRowBrush {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var boxItem = value as ListBoxItem;
            if (boxItem != null) {
                var listCtrl = ItemsControl.ItemsControlFromItemContainer(boxItem);
                if (listCtrl != null) {
                    var index = listCtrl.ItemContainerGenerator.IndexFromContainer(boxItem);

                    return index % 2 == 0
                        ? EvenRowBrush
                        : OddRowBrush;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new InvalidOperationException("Impossible to convert from a Brush to a ListViewItem!");
        }
    }
}
