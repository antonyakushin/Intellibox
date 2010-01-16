using System.Collections.ObjectModel;

namespace System.Windows.Controls.Custom {
    /// <summary>
    /// Represents an observable collection of <see cref="DataColumn"/>s.
    /// This class exists becuase XAML pre-2009 spec doesn't support the instantiation of generic types.
    /// </summary>
    public class DataColumnCollection : ObservableCollection<DataColumn> {
    }
}
