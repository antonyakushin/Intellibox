/*
Copyright (c) 2010 Stephen Ward and Joseph E Feser

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
        public static readonly DependencyProperty DataProviderProperty =
            DependencyProperty.Register("DataProvider", typeof(ISearchResultsProvider), typeof(IntelliBox), new UIPropertyMetadata(null));

        public static readonly DependencyProperty HideColumnHeadersProperty =
            DependencyProperty.Register("HideColumnHeaders", typeof(bool), typeof(IntelliBox), new UIPropertyMetadata(false));

        public static readonly DependencyProperty HighlightedItemProperty =
            DependencyProperty.Register("HighlightedItem", typeof(object), typeof(IntelliBox), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<object>), typeof(IntelliBox), new UIPropertyMetadata(null));

        public static readonly DependencyProperty MaxResultsProperty =
            DependencyProperty.Register("MaxResults", typeof(int), typeof(IntelliBox), new UIPropertyMetadata(10));

        public static readonly DependencyProperty ResultListMaxHeightProperty =
            DependencyProperty.Register("ResultListMaxHeight", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(150d));

        public static readonly DependencyProperty ResultListMaxWidthProperty =
            DependencyProperty.Register("ResultListMaxWidth", typeof(double), typeof(IntelliBox), new UIPropertyMetadata(400d));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(IntelliBox), new UIPropertyMetadata(null));

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(IntelliBox), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ShowResultsProperty =
            DependencyProperty.Register("ShowResults", typeof(bool), typeof(IntelliBox), new UIPropertyMetadata(false));

        private static Type[] _baseTypes = new[] {
            typeof(bool), typeof(byte), typeof(sbyte), typeof(char), typeof(decimal),
            typeof(double), typeof(float),
            typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(string)
        };

        private DateTime _lastTimeSearchRecievedUtc;
        private Type _previousResultType;
        private string _lastTextValue;
        private Binding _selectedValueBinding;

        public ObservableCollection<DataColumn> Columns {
            get;
            set;
        }

        public ISearchResultsProvider DataProvider {
            get {
                return (ISearchResultsProvider)GetValue(DataProviderProperty);
            }
            set {
                SetValue(DataProviderProperty, value);
            }
        }

        public Binding DisplayedValueBinding {
            get;
            set;
        }

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

        public bool HideColumnHeaders {
            get {
                return (bool)GetValue(HideColumnHeadersProperty);
            }
            set {
                SetValue(HideColumnHeadersProperty, value);
            }
        }

        private object HighlightedItem {
            get {
                return (object)GetValue(HighlightedItemProperty);
            }
            set {
                SetValue(HighlightedItemProperty, value);
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

        public int MaxResults {
            get {
                return (int)GetValue(MaxResultsProperty);
            }
            set {
                SetValue(MaxResultsProperty, value);
            }
        }

        public double ResultListMaxHeight {
            get {
                return (double)GetValue(ResultListMaxHeightProperty);
            }
            set {
                SetValue(ResultListMaxHeightProperty, value);
            }
        }

        public double ResultListMaxWidth {
            get {
                return (double)GetValue(ResultListMaxWidthProperty);
            }
            set {
                SetValue(ResultListMaxWidthProperty, value);
            }
        }

        public bool SelectAllOnFocus {
            get;
            set;
        }

        public object SelectedItem {
            get {
                return (object)GetValue(SelectedItemProperty);
            }
            set {
                SetValue(SelectedItemProperty, value);
            }
        }

        public object SelectedValue {
            get {
                return (object)GetValue(SelectedValueProperty);
            }
            set {
                SetValue(SelectedValueProperty, value);
            }
        }


        public Binding SelectedValueBinding {
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

        private void OnSelectedValueBindingChanged() {
            if (SelectedValueBinding == null) {
                BindingOperations.ClearBinding(this, SelectedValueProperty);
            }
            else {
                this.SetBinding(SelectedValueProperty, ConstructBindingForSelected(SelectedValueBinding));
            }
        }

        private bool ShowResults {
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

        public IntelliBox() {
            _lastTimeSearchRecievedUtc = DateTime.Now.ToUniversalTime(); // make sure the field is never null
            Columns = new ObservableCollection<DataColumn>();
            InitializeComponent();
        }

        private void ClearSelectedItem() {
            SetSelectedItem(null);
            CloseSearchResults();
        }

        private Binding Clone(Binding template) {
            var bind = new Binding() {
                BindingGroupName = template.BindingGroupName,
                BindsDirectlyToSource = template.BindsDirectlyToSource,
                Converter = template.Converter,
                ConverterCulture = template.ConverterCulture,
                ConverterParameter = template.ConverterParameter,
                FallbackValue = template.FallbackValue,
                IsAsync = template.IsAsync,
                Mode = template.Mode,
                NotifyOnSourceUpdated = template.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = template.NotifyOnTargetUpdated,
                NotifyOnValidationError = template.NotifyOnValidationError,
                Path = template.Path,
                StringFormat = template.StringFormat,
                TargetNullValue = template.TargetNullValue,
                UpdateSourceExceptionFilter = template.UpdateSourceExceptionFilter,
                UpdateSourceTrigger = template.UpdateSourceTrigger,
                ValidatesOnDataErrors = template.ValidatesOnDataErrors,
                ValidatesOnExceptions = template.ValidatesOnExceptions,
                XPath = template.XPath

            };

            if (!string.IsNullOrEmpty(template.ElementName))
                bind.ElementName = template.ElementName;

            if (template.RelativeSource != null)
                bind.RelativeSource = template.RelativeSource;

            if (template.Source != null)
                bind.Source = template.Source;

            for (int i = 0; i < template.ValidationRules.Count; i++) {
                bind.ValidationRules.Add(template.ValidationRules[i]);
            }

            return bind;
        }

        private GridViewColumn Clone(DataColumn original) {
            //TODO need to copy bindings over, if any exist
            var copy = new GridViewColumn() {
                CellTemplate = original.CellTemplate, //DP
                CellTemplateSelector = original.CellTemplateSelector, //DP
                DisplayMemberBinding = original.DisplayMemberBinding,
                Header = original.Header, //DP
                HeaderContainerStyle = original.HeaderContainerStyle, //DP
                HeaderStringFormat = original.HeaderStringFormat, //DP
                HeaderTemplate = original.HeaderTemplate, //DP
                HeaderTemplateSelector = original.HeaderTemplateSelector, //DP
                Width = original.Width //DP
            };

            return copy;
        }

        private void CloseSearchResults() {
            ShowResults = false;
            noResultsPopup.IsOpen = false;
        }

        private Binding ConstructBindingForHighlighted(Binding template) {
            var bind = Clone(template);
            if (bind.ElementName != null)
                bind.ElementName = null;

            if (bind.RelativeSource != null)
                bind.RelativeSource = null;

            bind.Path = new PropertyPath("HighlightedItem." + bind.Path.Path, bind.Path.PathParameters);
            bind.Source = this;
            return bind;
        }

        private Binding ConstructBindingForSelected(Binding template) {
            var bind = Clone(template);
            if (bind.ElementName != null)
                bind.ElementName = null;

            if (bind.RelativeSource != null)
                bind.RelativeSource = null;

            bind.Path = new PropertyPath("SelectedItem." + bind.Path.Path, bind.Path.PathParameters);
            bind.Source = this;
            return bind;
        }

        private GridView ConstructGridView(object item) {
            if (IsBaseType(item))
                return null; // do the default thing

            var view = new GridView();
            if (HideColumnHeaders) {
                view.ColumnHeaderContainerStyle = ZeroHeightColumnHeader;
            }

            if (ExplicitlyIncludeColumns && Columns != null && Columns.Count > 0) {
                foreach (var col in Columns.Where(c => !c.Hide).OrderBy(c => c.Position ?? int.MaxValue)) {
                    view.Columns.Add(Clone(col));
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
                        var gvc = Clone(p.Col);

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
                UpdateSearchBoxText();

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

        private bool IsNavigationKey(Key pressed) {
            return pressed == Key.Down
                || pressed == Key.Up
                || pressed == Key.NumPad8
                || pressed == Key.NumPad2
                || pressed == Key.PageUp    //TODO need to handle navigation keys that skip items
                || pressed == Key.PageDown;
        }

        private bool IsSelectCurrentItemKey(Key pressed) {
            return pressed == Key.Enter || pressed == Key.Return || pressed == Key.Tab;
        }

        private void SelectCurrentItem() {
            SetSelectedItem(HighlightedItem);
            CloseSearchResults();
            _lastTextValue = null;

            BindingOperations.ClearBinding(PART_EDITFIELD, TextBox.TextProperty);
            UpdateSearchBoxText(true);
            var text = PART_EDITFIELD.Text;
            BindingOperations.ClearBinding(PART_EDITFIELD, TextBox.TextProperty); // so that it can bind to the Highlighted item if the user starts typing again
            PART_EDITFIELD.Text = text;

            if (Items != null) {
                Items = null;
            }
        }

        private void SetSelectedItem(object value) {
            this.SelectedItem = value;
            var exp = BindingOperations.GetBindingExpression(this, SelectedValueProperty);
            if (exp != null) {
                exp.UpdateTarget();
            }
        }

        private void OnListItemMouseUp(object sender, MouseButtonEventArgs e) {
            SelectCurrentItem();
            CloseSearchResults();
        }

        private void OnTextBoxKeyUp(object sender, KeyEventArgs e) {
            if (!HasDataProvider)
                return;

            if (IsCancelKey(e.Key) || IsSelectCurrentItemKey(e.Key) || IsNavigationKey(e.Key)) {
                return;
            }

            var field = sender as TextBox;
            if (field != null && _lastTextValue != field.Text) {
                _lastTextValue = field.Text;

                if (string.IsNullOrEmpty(_lastTextValue)) {
                    ClearSelectedItem();
                }
                else {
                    DataProvider.BeginSearchAsync(_lastTextValue, DateTime.Now.ToUniversalTime(), MaxResults, Tag, ProcessSearchResults);
                }
            }
        }

        private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e) {
            if (!HasDataProvider || !ShowResults)
                return;

            if (IsCancelKey(e.Key)) {
                CloseSearchResults();
                return;
            }

            if (IsSelectCurrentItemKey(e.Key)) {
                SelectCurrentItem();
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

            ShowResults = false;

            var list = results.ToList();
            noResultsPopup.IsOpen = list.Count < 1;
            Items = list;

            if (list.Count > 0) {
                if (_previousResultType != Items[0].GetType()) {
                    _previousResultType = Items[0].GetType();

                    lstSearchItems.View = ConstructGridView(Items[0]);
                }
                lstSearchItems.SelectedIndex = 0;
                ShowResults = true;
            }
        }

        private void UpdateSearchBoxText() {
            UpdateSearchBoxText(false);
        }

        private void UpdateSearchBoxText(bool useSelectedItem) {
            //this is hacky, but the other alternative is to evaluate a propery path manually
            if (DisplayedValueBinding == null)
                return;

            var exp = BindingOperations.GetBindingExpressionBase(PART_EDITFIELD, TextBox.TextProperty);
            if (exp == null) {
                var bind = useSelectedItem
                    ? ConstructBindingForSelected(DisplayedValueBinding)
                    : ConstructBindingForHighlighted(DisplayedValueBinding);
                bind.Mode = BindingMode.OneTime; //setting to OneTime means that it updates only when I want it to -- despite the normal meaning of 'Explicit'
                bind.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;

                BindingOperations.SetBinding(PART_EDITFIELD, TextBox.TextProperty, bind);
            }
            else {
                exp.UpdateTarget();
            }

            PART_EDITFIELD.CaretIndex = PART_EDITFIELD.Text.Length;
        }

        private void PART_EDITFIELD_GotFocus(object sender, RoutedEventArgs e) {
            System.Diagnostics.Trace.WriteLine("PART_EDITFIELD_GotFocus");
            if (SelectAllOnFocus) {
                PART_EDITFIELD.SelectAll();
            }
        }

        private void lstSearchItems_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (IsCancelKey(e.Key)) {
                CloseSearchResults();
                return;
            }
        }
    }
}
