using System.Windows;
using System.Windows.Controls;

namespace System.Windows.Controls.Custom {
    public class DataColumn : GridViewColumn {
        public static readonly DependencyProperty ForPropertyProperty =
            DependencyProperty.Register("ForProperty", typeof(string), typeof(DataColumn), new UIPropertyMetadata(null));

        public static readonly DependencyProperty HideProperty =
            DependencyProperty.Register("Hide", typeof(bool), typeof(DataColumn), new UIPropertyMetadata(false));

        public string ForProperty {
            get {
                return (string)GetValue(ForPropertyProperty);
            }
            set {
                SetValue(ForPropertyProperty, value);
            }
        }

        public bool Hide {
            get {
                return (bool)GetValue(HideProperty);
            }
            set {
                SetValue(HideProperty, value);
            }
        }

        public int? Position {
            get;
            set;
        }
    }
}
