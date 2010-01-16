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
using System.Reflection;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace System.Windows.Controls.Custom {

    public partial class IntelliBox : UserControl {
        /// <summary>
        /// Identifies the <see cref="DataProviderProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty DataProviderProperty =
            DependencyProperty.Register("DataProvider", typeof(ISearchResultsProvider), typeof(IntelliBox), new UIPropertyMetadata(null));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="DisplayTextFromHighlightedItemProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty DisplayTextFromHighlightedItemProperty =
            DependencyProperty.Register("DisplayTextFromHighlightedItem", typeof(string), typeof(IntelliBox), new UIPropertyMetadata(null));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="DisplayTextFromSelectedItemProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty DisplayTextFromSelectedItemProperty =
            DependencyProperty.Register("DisplayTextFromSelectedItem", typeof(string), typeof(IntelliBox), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="HideColumnHeadersProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty HideColumnHeadersProperty =
            DependencyProperty.Register("HideColumnHeaders", typeof(bool), typeof(IntelliBox), new UIPropertyMetadata(false));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="ItemsProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<object>), typeof(IntelliBox), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="MaxResultsProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty MaxResultsProperty =
            DependencyProperty.Register("MaxResults", typeof(int), typeof(IntelliBox), new UIPropertyMetadata(10));

        /// <summary>
        /// Identifies the <see cref="ResultsHeightProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsHeightProperty =
            DependencyProperty.Register("ResultsHeight", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(double.NaN));

        /// <summary>
        /// Identifies the <see cref="ResultsMaxHeightProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMaxHeightProperty =
            DependencyProperty.Register("ResultsMaxHeight", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Identifies the <see cref="ResultsMaxWidthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMaxWidthProperty =
            DependencyProperty.Register("ResultsMaxWidth", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Identifies the <see cref="ResultsMinHeightProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMinHeightProperty =
            DependencyProperty.Register("ResultsMinHeight", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ResultsMinWidthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMinWidthProperty =
                    DependencyProperty.Register("ResultsMinWidth", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ResultsWidthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsWidthProperty =
            DependencyProperty.Register("ResultsWidth", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(double.NaN));

        /// <summary>
        /// Identifies the <see cref="SelectedItemProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(IntelliBox), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="SelectedValueProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(IntelliBox), new UIPropertyMetadata(null));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="ShowResultsProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ShowResultsProperty =
            DependencyProperty.Register("ShowPopup", typeof(bool), typeof(IntelliBox), new UIPropertyMetadata(false));


        private static Type[] _baseTypes = new[] {
            typeof(bool), typeof(byte), typeof(sbyte), typeof(char), typeof(decimal),
            typeof(double), typeof(float),
            typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(string)
        };

        private DateTime _lastTimeSearchRecievedUtc;
        private Type _previousResultType;
        private string _lastTextValue;
        private BindingBase _selectedValueBinding;
        private BindingBase _displayedValueBinding;
        private AbstractRowColorizer _rowColorizer;

        /// <summary>
        /// The columns in the search result set to display. When <see cref="ExplicitlyIncludeColumns"/>
        /// is set to true, then only the <see cref="DataColumn"/>s in this collection will be shown.
        /// Setting <see cref="HideColumnHeaders"/> to true will prevent column headers from being shown.
        /// </summary>
        public DataColumnCollection Columns {
            get;
            set;
        }

        /// <summary>
        /// This is the <see cref="ISearchResultsProvider"/> that the <see cref="IntelliBox"/> uses
        /// to ask for search results. This is a Dependancy Property.
        /// </summary>
        public ISearchResultsProvider DataProvider {
            get {
                return (ISearchResultsProvider)GetValue(DataProviderProperty);
            }
            set {
                SetValue(DataProviderProperty, value);
            }
        }

        /// <summary>
        /// When True, the text in the text field will NOT be trimmed for
        /// whitespace prior to being passed to the <see cref="DataProvider"/>.
        /// </summary>
        public bool DisableWhitespaceTrim {
            get;
            set;
        }

        /// <summary>
        /// A binding expression that determines which column in the search result set
        /// displays its value in the text field. Typically, the value displayed should
        /// correspond to the column the <see cref="DataProvider"/> searches on. This binding
        /// expression can be different from the on in the <see cref="SelectedValueBinding"/>.
        /// If this property is NULL, then an entire row from the search result set displays
        /// its value in the text field.
        /// This is a Dependancy Property.
        /// </summary>
        public BindingBase DisplayedValueBinding {
            get {
                return _displayedValueBinding;
            }
            set {
                if (value != _displayedValueBinding) {
                    _displayedValueBinding = value;
                    //the call is commented out so that people can type w/o the displayed value overwriting what they're trying to do
                    OnDisplayedValueBindingChanged();
                }
            }
        }

        private string DisplayTextFromHighlightedItem {
            get {
                return (string)GetValue(DisplayTextFromHighlightedItemProperty);
            }
            set {
                SetValue(DisplayTextFromHighlightedItemProperty, value);
            }
        }

        private string DisplayTextFromSelectedItem {
            get {
                return (string)GetValue(DisplayTextFromSelectedItemProperty);
            }
            set {
                SetValue(DisplayTextFromSelectedItemProperty, value);
            }
        }

        /// <summary>
        /// When True, only the <see cref="DataColumn"/>s in the <see cref="Columns"/> collection
        /// will display in the search results set. When False, all the columns in the search result set
        /// will show, but any columns in the <see cref="Columns"/> collection then override specific columns.
        /// </summary>
        public bool ExplicitlyIncludeColumns {
            get;
            set;
        }

        private bool HasDataProvider {
            get {
                return DataProvider != null;
            }
        }

        private bool HasItems {
            get {
                return Items != null && Items.Count > 0;
            }
        }

        /// <summary>
        /// When True, columns in the search result set will not have headers. This is a Dependancy Property.
        /// </summary>
        public bool HideColumnHeaders {
            get {
                return (bool)GetValue(HideColumnHeadersProperty);
            }
            set {
                SetValue(HideColumnHeadersProperty, value);
            }
        }

        private List<object> Items {
            get {
                return (List<object>)GetValue(ItemsProperty);
            }
            set {
                SetValue(ItemsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of results that the <see cref="IntelliBox"/> asks
        /// its <see cref="ISearchResultsProvider"/> for. This is a Dependancy Property.
        /// </summary>
        public int MaxResults {
            get {
                return (int)GetValue(MaxResultsProperty);
            }
            set {
                SetValue(MaxResultsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the suggested height that the search results popup.
        /// The default value is 200.
        /// This is a Dependancy Property.
        /// </summary>
        public double ResultsHeight {
            get {
                return (double)GetValue(ResultsHeightProperty);
            }
            set {
                SetValue(ResultsHeightProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum height that the search results popup is allowed to have.
        /// This is a Dependancy Property.
        /// </summary>
        public double ResultsMaxHeight {
            get {
                return (double)GetValue(ResultsMaxHeightProperty);
            }
            set {
                SetValue(ResultsMaxHeightProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum width that the search results popup is allowed to have.
        /// This is a Dependancy Property.
        /// </summary>
        public double ResultsMaxWidth {
            get {
                return (double)GetValue(ResultsMaxWidthProperty);
            }
            set {
                SetValue(ResultsMaxWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum height that the search results popup is allowed to have.
        /// This is a Dependancy Property.
        /// </summary>
        public double ResultsMinHeight {
            get {
                return (double)GetValue(ResultsMinHeightProperty);
            }
            set {
                SetValue(ResultsMinHeightProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum width that the search results popup is allowed to have.
        /// This is a Dependancy Property.
        /// </summary>
        public double ResultsMinWidth {
            get {
                return (double)GetValue(ResultsMinWidthProperty);
            }
            set {
                SetValue(ResultsMinWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the suggested width that the search results popup is allowed to have.
        /// The default value is 400.
        /// This is a Dependancy Property.
        /// </summary>
        public double ResultsWidth {
            get {
                return (double)GetValue(ResultsWidthProperty);
            }
            set {
                SetValue(ResultsWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AbstractRowColorizer"/> used to color each row of the search result set.
        /// Set to an instance of <see cref="AlternateRowColorizer"/> by default.
        /// </summary>
        public AbstractRowColorizer RowColorizer {
            get {
                return _rowColorizer;
            }
            set {
                if (value != _rowColorizer) {
                    _rowColorizer = value;
                    OnRowColorizerChanged();
                }
            }
        }

        /// <summary>
        /// When true, all of the text in the field will be selected when the control gets focus.
        /// </summary>
        public bool SelectAllOnFocus {
            get;
            set;
        }

        /// <summary>
        /// The data row from the search result set that the user has most recently selected and confirmed.
        /// This is a Dependancy Property.
        /// </summary>
        public object SelectedItem {
            get {
                return (object)GetValue(SelectedItemProperty);
            }
            set {
                SetValue(SelectedItemProperty, value);
            }
        }

        /// <summary>
        /// A value out of the <see cref="SelectedItem"/>. The exact value depends on
        /// the <see cref="SelectedValueBinding"/> property. This is a Dependancy Property.
        /// </summary>
        public object SelectedValue {
            get {
                return (object)GetValue(SelectedValueProperty);
            }
            set {
                SetValue(SelectedValueProperty, value);
            }
        }

        /// <summary>
        /// A binding expression that determines what <see cref="SelectedValue"/>
        /// will be chosen out of the <see cref="SelectedItem"/>. If this property is
        /// NULL, then the entire <see cref="SelectedItem"/> is chosen as the <see cref="SelectedValue"/>.
        /// This property exists so that the <see cref="SelectedValue" /> can differ from the
        /// value displayed in the text field.
        /// This is a Dependancy Property.
        /// </summary>
        public BindingBase SelectedValueBinding {
            get {
                return _selectedValueBinding;
            }
            set {
                if (value != _selectedValueBinding) {
                    _selectedValueBinding = value;
                    OnSelectedValueBindingChanged();
                }
            }
        }

        private bool ShowPopup {
            get {
                return (bool)GetValue(ShowResultsProperty);
            }
            set {
                SetValue(ShowResultsProperty, value);
            }
        }

        private Style ZeroHeightColumnHeader {
            get {
                var noHeader = new Style(typeof(GridViewColumnHeader));
                noHeader.Setters.Add(new Setter(GridViewColumnHeader.HeightProperty, 0.0));
                return noHeader;
            }
        }

        /// <summary>
        /// Initializes the <see cref="IntelliBox" />, preparing it to accept data entry
        /// and retrieve results from the <see cref="DataProvider"/>.
        /// </summary>
        public IntelliBox() {
            _lastTimeSearchRecievedUtc = DateTime.Now.ToUniversalTime(); // make sure the field is never null
            Columns = new DataColumnCollection();

            //set up default bindings
            OnSelectedValueBindingChanged();
            OnDisplayedValueBindingChanged();

            InitializeComponent();

            RowColorizer = new AlternateRowColorizer() {
                OddRowBrush = Brushes.Gainsboro
            };
        }

        private void CancelSelection() {
            _lastTextValue = null;
            UpdateSearchBoxText(true);
            
            CloseSearchResults();

            if (Items != null) {
                Items = null;
            }
        }

        private void ChooseCurrentItem() {
            SetSelectedItem(lstSearchItems.SelectedItem);
            _lastTextValue = null;
            UpdateSearchBoxText(true);
            
            CloseSearchResults();

            if (Items != null) {
                Items = null;
            }
        }

        private void ClearSelectedItem() {
            SetSelectedItem(null);
            CloseSearchResults();
        }

        private void CloseSearchResults() {
            ShowPopup = false;
            noResultsPopup.IsOpen = false;

            if (DataProvider != null) {
                DataProvider.CancelAllSearches();
            }
        }

        private GridView ConstructGridView(object item) {
            var view = new GridView();
            if (HideColumnHeaders || IsBaseType(item)) {
                view.ColumnHeaderContainerStyle = ZeroHeightColumnHeader;
            }

            if (IsBaseType(item)) {
                var gvc = new GridViewColumn();
                gvc.Header = item.GetType().Name;

                gvc.DisplayMemberBinding = new Binding();
                view.Columns.Add(gvc);

                return view;
            }

            if (ExplicitlyIncludeColumns && Columns != null && Columns.Count > 0) {
                foreach (var col in Columns.Where(c => !c.Hide).OrderBy(c => c.Position ?? int.MaxValue)) {
                    view.Columns.Add(CloneHelper.Clone(col));
                }
                return view;
            }

            var temp = from p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                       where p.CanRead && p.CanWrite
                       select new {
                           Name = p.Name,
                           Col = Columns.FirstOrDefault(c => p.Name.Equals(c.ForProperty))
                       };

            var left = temp.Where(a => a.Col != null && a.Col.Position != null).OrderBy(a => a.Col.Position);
            var right = temp.Except(left);

            var props = left.Concat(right);

            foreach (var p in props) {
                if (p.Col != null) {
                    if (!p.Col.Hide) {
                        var gvc = CloneHelper.Clone(p.Col);

                        if (gvc.Header == null) { // TODO check if this is bound to anything
                            gvc.Header = p.Name;
                        }

                        if (gvc.DisplayMemberBinding == null) {
                            gvc.DisplayMemberBinding = new Binding(p.Name);
                        }

                        view.Columns.Add(gvc);
                    }
                }
                else {
                    var gvc = new GridViewColumn();
                    gvc.Header = p.Name;

                    gvc.DisplayMemberBinding = new Binding(p.Name);
                    view.Columns.Add(gvc);
                }
            }

            return view;
        }

        private int GetIncrementValueForKey(Key pressed) {
            switch (pressed) {
                case Key.Down:
                case Key.Up:
                case Key.NumPad8:
                case Key.NumPad2:
                    return 1;
                default:
                    return 0;
            }
        }

        private bool HighlightNextItem(Key pressed) {
            if (lstSearchItems != null && HasItems) {
                var goDown = pressed == Key.Tab || pressed == Key.Down || pressed == Key.NumPad2 || pressed == Key.PageDown;
                var nextIndex = goDown
                    ? lstSearchItems.SelectedIndex + GetIncrementValueForKey(pressed)
                    : lstSearchItems.SelectedIndex - GetIncrementValueForKey(pressed);

                int maxIndex = this.Items.Count - 1; //dangerous, since the list could be really large

                if (nextIndex < 0) {
                    if (lstSearchItems.SelectedIndex != 0)
                        nextIndex = 0;
                    else
                        nextIndex = maxIndex;
                }

                if (nextIndex >= maxIndex) {
                    if (lstSearchItems.SelectedIndex != maxIndex)
                        nextIndex = maxIndex;
                    else
                        nextIndex = 0;
                }

                lstSearchItems.SelectedIndex = nextIndex;
                lstSearchItems.ScrollIntoView(lstSearchItems.SelectedItem);

                return true;
            }

            return false;
        }

        private bool IsBaseType(object item) {
            var type = item.GetType();
            return _baseTypes.Any(i => i == type);
        }

        private bool IsCancelKey(Key key) {
            return key == Key.Escape;
        }

        private bool IsChooseCurrentItemKey(Key pressed) {
            return pressed == Key.Enter || pressed == Key.Return || pressed == Key.Tab;
        }

        private bool IsNavigationKey(Key pressed) {
            return pressed == Key.Down
                || pressed == Key.Up
                || pressed == Key.NumPad8
                || pressed == Key.NumPad2
                || pressed == Key.PageUp    //TODO need to handle navigation keys that skip items
                || pressed == Key.PageDown;
        }

        private void SetSelectedItem(object value) {
            this.SelectedItem = value;
            var exp = BindingOperations.GetBindingExpression(this, SelectedValueProperty);
            if (exp != null) {
                exp.UpdateTarget();
            }
        }

        private void OnDisplayedValueBindingChanged() {
            if (lstSearchItems != null) {
                this.SetBinding(IntelliBox.DisplayTextFromHighlightedItemProperty,
                    BindingBaseFactory.ConstructBindingForHighlighted(this, DisplayedValueBinding));
            }

            this.SetBinding(IntelliBox.DisplayTextFromSelectedItemProperty,
                BindingBaseFactory.ConstructBindingForSelected(this, DisplayedValueBinding));
        }

        private void OnListItemMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            ChooseCurrentItem();
            CloseSearchResults();
        }

        private void OnRowColorizerChanged() {
            if (IsInitialized) {
                var bind = new Binding() {
                    RelativeSource = RelativeSource.Self,
                    Converter = RowColorizer
                };

                var style = new Style(typeof(ListViewItem));
                style.Setters.Add(new Setter(ListViewItem.BackgroundProperty, bind));
                
                var sett = new EventSetter(ListViewItem.MouseDoubleClickEvent, new MouseButtonEventHandler(OnListItemMouseDoubleClick));
                style.Setters.Add(sett);

                Resources[typeof(ListViewItem)] = style;
            }
        }

        private void OnSelectedValueBindingChanged() {
            var bind = BindingBaseFactory.ConstructBindingForSelected(this, SelectedValueBinding);
            this.SetBinding(SelectedValueProperty, bind);
        }

        private void OnTextBoxKeyUp(object sender, KeyEventArgs e) {
            if (!HasDataProvider)
                return;

            if (IsCancelKey(e.Key) || IsChooseCurrentItemKey(e.Key) || IsNavigationKey(e.Key)) {
                return;
            }

            var field = sender as TextBox;
            if (field != null && _lastTextValue != field.Text) {
                _lastTextValue = field.Text;

                if (string.IsNullOrEmpty(_lastTextValue)) {
                    ClearSelectedItem();
                }
                else {
                    var search = DisableWhitespaceTrim
                        ? _lastTextValue
                        : _lastTextValue.Trim();
                    DataProvider.BeginSearchAsync(search, DateTime.Now.ToUniversalTime(), MaxResults, Tag, ProcessSearchResults);
                }
            }
        }

        private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e) {
            if (!HasDataProvider || !ShowPopup)
                return;

            if (IsCancelKey(e.Key)) {
                CancelSelection();
                return;
            }

            if (IsChooseCurrentItemKey(e.Key)) {
                ChooseCurrentItem();
                return;
            }

            if (IsNavigationKey(e.Key)) {
                HighlightNextItem(e.Key);
                e.Handled = true;
            }
        }

        private void ProcessSearchResults(DateTime startTimeUtc, IEnumerable<object> results) {
            if (_lastTimeSearchRecievedUtc > startTimeUtc)
                return; // this result set isn't fresh, so don't bother processing it

            _lastTimeSearchRecievedUtc = startTimeUtc;

            ShowPopup = false;

            var list = results.ToList();
            noResultsPopup.IsOpen = list.Count < 1;
            Items = list;

            if (list.Count > 0) {
                if (_previousResultType != Items[0].GetType()) {
                    _previousResultType = Items[0].GetType();

                    lstSearchItems.View = ConstructGridView(Items[0]);
                }
                lstSearchItems.SelectedIndex = 0;
                ShowPopup = true;
            }
        }

        private void UpdateSearchBoxText(bool useSelectedItem) {

            var text = useSelectedItem
                ? this.DisplayTextFromSelectedItem
                : this.DisplayTextFromHighlightedItem;

            PART_EDITFIELD.Text = text;
            if (!string.IsNullOrEmpty(text)) {
                PART_EDITFIELD.CaretIndex = text.Length;
            }
        }

        private void PART_EDITFIELD_GotFocus(object sender, RoutedEventArgs e) {
            if (SelectAllOnFocus) {
                PART_EDITFIELD.SelectAll();
            }
        }

        private void lstSearchItems_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (IsCancelKey(e.Key)) {
                CancelSelection();
                return;
            }
        }

        private void Popup_PreviewMouseButton(object sender, MouseButtonEventArgs e) {
            var pop = sender as System.Windows.Controls.Primitives.Popup;
            if (pop != null && pop.IsOpen == false) {
                CancelSelection();
            }
        }
    }
}
