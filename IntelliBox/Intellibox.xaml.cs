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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Collections;
using System.Windows.Threading;
using System.Windows;
using System;
using System.Windows.Controls;

namespace FeserWard.Controls {
    /// <summary>
    /// An implementation of the MS Access 'Lookup' field for WPF that uses the Provider pattern.
    /// <list type="bullet">
    ///     <listheader>
    ///         <description>Features</description>
    ///     </listheader>
    ///     <item>
    ///         <description>Text shown in the search field can be different from the value selected by the user.</description>
    ///     </item>
    ///     <item>
    ///         <description>Fully supports syncronous and asyncronous searches.</description>
    ///     </item>
    ///     <item>
    ///         <description>Displayed columns can be fully customized.</description>
    ///     </item>
    ///     <item>
    ///         <description>Supports search cancelation.</description>
    ///     </item>
    /// </list>
    /// </summary>
    public partial class Intellibox : UserControl {

        private const int MinimumSearchDelayMS = 125;

        /// <summary>
        /// Identifies the <see cref="DataProviderProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty DataProviderProperty =
            DependencyProperty.Register("DataProvider", typeof(IIntelliboxResultsProvider), typeof(Intellibox),
            new UIPropertyMetadata(new PropertyChangedCallback(OnDataProviderChanged)));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="DisplayTextFromHighlightedItemProperty"/> Dependancy Property.
        /// </summary>
        protected static readonly DependencyProperty DisplayTextFromHighlightedItemProperty =
            DependencyProperty.Register("DisplayTextFromHighlightedItem", typeof(string), typeof(Intellibox), new UIPropertyMetadata(null));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="DisplayTextFromSelectedItemProperty"/> Dependancy Property.
        /// </summary>
        protected static readonly DependencyProperty DisplayTextFromSelectedItemProperty =
            DependencyProperty.Register("DisplayTextFromSelectedItem", typeof(string), typeof(Intellibox), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="HideColumnHeadersProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty HideColumnHeadersProperty =
            DependencyProperty.Register("HideColumnHeaders", typeof(bool), typeof(Intellibox), new UIPropertyMetadata(false));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="ItemsProperty"/> Dependancy Property.
        /// </summary>
        protected static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(IList), typeof(Intellibox), new UIPropertyMetadata(null));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="IntermediateSelectedValueProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty IntermediateSelectedValueProperty =
            DependencyProperty.Register("IntermediateSelectedValue", typeof(object), typeof(Intellibox), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="MaxResultsProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty MaxResultsProperty =
            DependencyProperty.Register("MaxResults", typeof(int), typeof(Intellibox), new UIPropertyMetadata(10));

        /// <summary>
        /// Identifies the <see cref="MinimumPrefixLengthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty MinimumPrefixLengthProperty =
            DependencyProperty.Register("MinimumPrefixLength", typeof(int), typeof(Intellibox),
            new UIPropertyMetadata(1, null, CoerceMinimumPrefixLengthProperty));

        /// <summary>
        /// Identifies the <see cref="MinimumSearchDelayProperty"/> Dependancy Property. Default is 250 milliseconds.
        /// </summary>
        public static readonly DependencyProperty MinimumSearchDelayProperty =
            DependencyProperty.Register("MinimumSearchDelay", typeof(int), typeof(Intellibox),
            new UIPropertyMetadata(250, null, CoerceMinimumSearchDelayProperty));

        /// <summary>
        /// Identifies the <see cref="TimeBeforeWaitNotification"/> Dependancy Property. Default is 125 milliseconds.
        /// </summary>
        public static readonly DependencyProperty TimeBeforeWaitNotificationProperty =
            DependencyProperty.Register("TimeBeforeWaitNotification", typeof(int), typeof(Intellibox),
            new UIPropertyMetadata(125,null, CoerceTimeBeforeWaitNotificationProperty));

        /// <summary>
        /// Identifies the <see cref="ResultsHeightProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsHeightProperty =
            DependencyProperty.Register("ResultsHeight", typeof(double), typeof(Intellibox), new UIPropertyMetadata(double.NaN));

        /// <summary>
        /// Identifies the <see cref="ResultsMaxHeightProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMaxHeightProperty =
            DependencyProperty.Register("ResultsMaxHeight", typeof(double), typeof(Intellibox), new UIPropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Identifies the <see cref="ResultsMaxWidthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMaxWidthProperty =
            DependencyProperty.Register("ResultsMaxWidth", typeof(double), typeof(Intellibox), new UIPropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Identifies the <see cref="ResultsMinHeightProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMinHeightProperty =
            DependencyProperty.Register("ResultsMinHeight", typeof(double), typeof(Intellibox), new UIPropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ResultsMinWidthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsMinWidthProperty =
                    DependencyProperty.Register("ResultsMinWidth", typeof(double), typeof(Intellibox), new UIPropertyMetadata(0d));

        /// <summary>
        /// Identifies the <see cref="ResultsWidthProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty ResultsWidthProperty =
            DependencyProperty.Register("ResultsWidth", typeof(double), typeof(Intellibox), new UIPropertyMetadata(double.NaN));

        /// <summary>
        /// Identifies the <see cref="SelectedItemProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(Intellibox), new UIPropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="SelectedValueProperty"/> Dependancy Property.
        /// </summary>
        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(Intellibox), new UIPropertyMetadata(null));

        /// <summary>
        /// For Internal Use Only. Identifies the <see cref="ShowResultsProperty"/> Dependancy Property.
        /// </summary>
        protected static readonly DependencyProperty ShowResultsProperty =
            DependencyProperty.Register("ShowResults", typeof(bool), typeof(Intellibox), new UIPropertyMetadata(false));

        private static Type[] _baseTypes = new[] {
            typeof(bool), typeof(byte), typeof(sbyte), typeof(char), typeof(decimal),
            typeof(double), typeof(float),
            typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(string)
        };

        private ICommand _cancelAllSearches;
        private DateTime _lastTimeSearchRecievedUtc;
        private Type _previousResultType;
        private string _lastTextValue;
        private BindingBase _selectedValueBinding;
        private BindingBase _displayedValueBinding;
        private IntelliboxRowColorizer _rowColorizer;

        /// <summary>
        /// This event is fired immediately before a new search is started.
        /// Note that not every <see cref="SearchBeginning"/> event has a matching <see cref="SearchCompleted"/> event.
        /// </summary>
        public event Action<string, int, object> SearchBeginning;

        /// <summary>
        /// This event is fired once a search has completed and the search results have been processed.
        /// Note that not every <see cref="SearchBeginning"/> event has a matching <see cref="SearchCompleted"/> event.
        /// </summary>
        public event Action SearchCompleted;

        /// <summary>
        /// Cancel all pending searches for the provider.
        /// </summary>
        public ICommand CancelAllSearches {
            get {
                if (_cancelAllSearches == null) {
                    _cancelAllSearches = new DelegateCommand(CancelSelection);
                }
                return _cancelAllSearches;
            }
        }

        /// <summary>
        /// The columns in the search result set to display. When <see cref="ExplicitlyIncludeColumns"/>
        /// is set to true, then only the <see cref="IntelliboxColumn"/>s in this collection will be shown.
        /// Setting <see cref="HideColumnHeaders"/> to true will prevent column headers from being shown.
        /// </summary>
        public IntelliboxColumnCollection Columns {
            get;
            set;
        }

        /// <summary>
        /// This is the <see cref="IIntelliboxResultsProvider"/> that the <see cref="Intellibox"/> uses
        /// to ask for search results. This is a Dependancy Property.
        /// </summary>
        public IIntelliboxResultsProvider DataProvider {
            get {
                return (IIntelliboxResultsProvider)GetValue(DataProviderProperty);
            }
            set {
                SetValue(DataProviderProperty, value);
            }
        }

        /// <summary>
        /// When True, the text in the search field will NOT be trimmed for
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
                if (_displayedValueBinding != value) {
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
        /// When True, only the <see cref="IntelliboxColumn"/>s in the <see cref="Columns"/> collection
        /// will display in the search results set. When False, all the columns in the search result set
        /// will show, but any columns in the <see cref="Columns"/> collection then override specific columns.
        /// </summary>
        public bool ExplicitlyIncludeColumns {
            get;
            set;
        }

        private bool HasDataProvider {
            get {
                return DataProvider != null && SearchProvider != null;
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

        /// <summary>
        /// When true, means that the control is in 'Search' mode.
        /// i.e. that it is firing searches as the user types and waiting for results.
        /// </summary>
        private bool IsSearchInProgress {
            get {
                return SearchTimer != null;
            }
        }

        /// <summary>
        /// This is the binding target of the <see cref="SelectedValueBinding"/> property,
        /// so that users of the control can place their own bindings on the <see cref="SelectedValue"/> property.
        /// </summary>
        private object IntermediateSelectedValue {
            get {
                return (object)GetValue(IntermediateSelectedValueProperty);
            }
            set {
                SetValue(IntermediateSelectedValueProperty, value);
            }
        }

        private IList Items {
            get {
                return (IList)GetValue(ItemsProperty);
            }
            set {
                SetValue(ItemsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of results that the <see cref="Intellibox"/> asks
        /// its <see cref="IIntelliboxResultsProvider"/> for. This is a Dependancy Property.
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
        /// The minimum number of characters to wait for the user to enter before starting the first search.
        /// After the first search has been started, the <see cref="MinimumSearchDelay"/> property controls how often
        /// additional searches are performed (assumming that additional text has been entered).
        /// Minimum value is 1 (one). Defaults to 1 (one);
        /// </summary>
        public int MinimumPrefixLength {
            get {
                return (int)GetValue(MinimumPrefixLengthProperty);
            }
            set {
                SetValue(MinimumPrefixLengthProperty, value);
            }
        }

        /// <summary>
        /// The number of milliseconds the <see cref="Intellibox"/> control will wait between searches
        /// when the user is rapidly entering text. Minimum is 125 milliseconds. Defaults to 250 milliseconds.
        /// </summary>
        public int MinimumSearchDelay {
            get {
                return (int)GetValue(MinimumSearchDelayProperty);
            }
            set {
                SetValue(MinimumSearchDelayProperty, value);
            }
        }

        /// <summary>
        /// Internal Use Only. Do Not Use. This property exists so that the <see cref="Intellibox"/>
        /// can run in partial-trust;
        /// </summary>
        public ListView ResultsList {
            get {
                return lstSearchItems;
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
        /// Gets or sets the <see cref="IntelliboxAlternateRowColorizer"/> used to color each row of the search result set.
        /// Set to an instance of <see cref="IntelliboxRowColorizer"/> by default.
        /// </summary>
        public IntelliboxRowColorizer RowColorizer {
            get {
                return _rowColorizer;
            }
            set {
                if (_rowColorizer != value) {
                    _rowColorizer = value;
                    OnRowColorizerChanged();
                }
            }
        }

        /// <summary>
        /// The Search provider that will actually perform the search
        /// </summary>
        private IntelliboxAsyncProvider SearchProvider {
            get;
            set;
        }

        /// <summary>
        /// Using a dispatcher timer so that the 'Tick' event gets posted on the UI thread and
        /// we don't have to worry about exceptions throwing when accessing UI controls.
        /// </summary>
        private DispatcherTimer SearchTimer {
            get;
            set;
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
                if (_selectedValueBinding != value) {
                    _selectedValueBinding = value;
                    OnSelectedValueBindingChanged();
                }
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

        /// <summary>
        /// The amount of time (in milliseconds) that the <see cref="Intellibox"/> control
        /// will wait for results to come back before showing the user a "Waiting for results" message.
        /// Minimum: 0ms, Default: 125ms
        /// </summary>
        public int TimeBeforeWaitNotification {
            get {
                return (int)GetValue(TimeBeforeWaitNotificationProperty);
            }
            set {
                SetValue(TimeBeforeWaitNotificationProperty, value);
            }
        }

        private DispatcherTimer WaitNotificationTimer {
            get;
            set;
        }

        private Style ZeroHeightColumnHeader {
            get {
                var noHeader = new Style(typeof(GridViewColumnHeader));
                noHeader.Setters.Add(new Setter(GridViewColumnHeader.HeightProperty, 0.0));
                return noHeader;
            }
        }

        /// <summary>
        /// Applies the <see cref="DisableWhitespaceTrim"/> property to the <paramref name="input"/> text.
        /// The return value is always non-null.
        /// </summary>
        /// <param name="input">the string to which <see cref="DisableWhitespaceTrim"/> should be applied.</param>
        /// <returns>
        /// If <see cref="DisableWhitespaceTrim"/> is true, returns <paramref name="input"/> unmodified.
        /// Otherwise the function returns the result of input.Trim(), or string.Empty if input is null.
        /// </returns>
        private string ApplyDisableWhitespaceTrim(string input) {
            // if the entered text isn't supposed to be trimmed, then use it as-is
            // otherwise Trim() it if it's not null, or set to string.Empty if it is null
            return DisableWhitespaceTrim
                    ? input
                    : (string.IsNullOrEmpty(input) ? string.Empty : input.Trim());
        }

        /// <summary>
        /// Initializes the <see cref="Intellibox" />, preparing it to accept data entry
        /// and retrieve results from the <see cref="DataProvider"/>.
        /// </summary>
        public Intellibox() {
            InitializeComponent();

            _lastTimeSearchRecievedUtc = DateTime.Now.ToUniversalTime(); // make sure the field is never null
            Columns = new IntelliboxColumnCollection();

            //setting up the default bindings in case the user doesn't set bindings themselves
            OnSelectedValueBindingChanged();
            OnDisplayedValueBindingChanged();

            RowColorizer = new IntelliboxAlternateRowColorizer() {
                OddRowBrush = Brushes.Gainsboro
            };
        }

        private void CancelSelection() {
            _lastTextValue = UpdateSearchBoxText(true);

            OnUserEndedSearchEvent();

            if (Items != null) {
                Items = null;
            }
        }

        private static object CoerceMinimumPrefixLengthProperty(DependencyObject reciever, object val) {
            var intval = (int)val;
            if (intval < 1)
                intval = 1;

            return intval;
        }

        private static object CoerceMinimumSearchDelayProperty(DependencyObject reciever, object val) {
            var intval = (int)val;
            if (intval < MinimumSearchDelayMS)
                intval = MinimumSearchDelayMS;

            return intval;
        }

        private static object CoerceTimeBeforeWaitNotificationProperty(DependencyObject reciever, object val) {
            return (int)val < 0 ? 0 : val;
        }

        private void ChooseCurrentItem() {
            this.SelectedItem = ResultsList.SelectedItem;
            
            // have to set this after the SelectedItem property is set
            this.SelectedValue = IntermediateSelectedValue;

            _lastTextValue = UpdateSearchBoxText(true);

            OnUserEndedSearchEvent();

            if (Items != null) {
                Items = null;
            }
        }

        private void ClearSelectedItem() {
            this.SelectedItem = null;
            OnUserEndedSearchEvent();
        }

        private GridView ConstructGridView(object item) {
            var view = new GridView();

            bool isBaseType = IsBaseType(item);

            if (isBaseType || HideColumnHeaders) {
                view.ColumnHeaderContainerStyle = ZeroHeightColumnHeader;
            }

            if (isBaseType) {
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

            var typeProperties = (from p in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  where p.CanRead && p.CanWrite
                                  select new {
                                      Name = p.Name,
                                      Column = Columns.FirstOrDefault(c => p.Name.Equals(c.ForProperty))
                                  }).ToList();

            //This is a shortcut to sort the nulls to the back of the list instead of the front
            //we did this instead of creating an IComparer.
            var typesWithPositions = typeProperties
                .Where(a => a.Column != null && a.Column.Position != null).OrderBy(a => a.Column.Position);

            var typesWithoutPositions = typeProperties.Except(typesWithPositions);

            var sortedProperties = typesWithPositions.Concat(typesWithoutPositions);

            foreach (var currentProperty in sortedProperties) {
                if (currentProperty.Column != null) {
                    if (!currentProperty.Column.Hide) {
                        var gvc = CloneHelper.Clone(currentProperty.Column);

                        if (gvc.Header == null) { // TODO check if this is bound to anything
                            gvc.Header = currentProperty.Name;
                        }

                        if (gvc.DisplayMemberBinding == null) {
                            gvc.DisplayMemberBinding = new Binding(currentProperty.Name);
                        }

                        view.Columns.Add(gvc);
                    }
                }
                else {
                    var gvc = new GridViewColumn();
                    gvc.Header = currentProperty.Name;

                    gvc.DisplayMemberBinding = new Binding(currentProperty.Name);
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

        private void HighlightNextItem(Key pressed) {
            if (ResultsList != null && HasItems) {
                //I used this solution partially
                //http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=324064
                //the only way I have been able to solve the lockups is to use the background priority
                //the default still causes lockups.
                //be very careful changing this line
                Dispatcher.BeginInvoke(new Action<Key>(HighlightNewItem), DispatcherPriority.Background, pressed);
            }
        }

        /// <summary>
        /// Because of a bug in .NET, this method should only ever be called from the dispatcher,
        /// and only ever with 'DispatcherPriority.Background'
        /// <para>
        /// See the following link for more details.
        /// http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=324064
        /// </para>
        /// </summary>
        private void HighlightNewItem(Key pressed) {
            var goDown = pressed == Key.Tab || pressed == Key.Down || pressed == Key.NumPad2 || pressed == Key.PageDown;
            var nextIndex = goDown
                ? ResultsList.SelectedIndex + GetIncrementValueForKey(pressed)
                : ResultsList.SelectedIndex - GetIncrementValueForKey(pressed);

            int maxIndex = Items.Count - 1; //dangerous, since the list could be really large

            if (nextIndex < 0) {
                if (ResultsList.SelectedIndex != 0)
                    nextIndex = 0;
                else
                    nextIndex = maxIndex;
            }

            if (nextIndex >= maxIndex) {
                if (ResultsList.SelectedIndex != maxIndex)
                    nextIndex = maxIndex;
                else
                    nextIndex = 0;
            }

            var selectedItem = Items[nextIndex];

            ResultsList.SelectedItem = selectedItem;
            ResultsList.ScrollIntoView(selectedItem);
        }

        //TODO remove. we are not going to support paging the result set
        //try to use this for paging support.
        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem) {
                    return (childItem)child;
                }
                else {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null) {
                        return childOfChild;
                    }
                }
            }
            return null;
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

        private static void OnDataProviderChanged(DependencyObject receiver, DependencyPropertyChangedEventArgs args) {
            //TODO changing the search provider really should cancel any searches already in progress
            var ib = receiver as Intellibox;
            if (ib != null && args != null && args.NewValue is IIntelliboxResultsProvider) {
                var provider = args.NewValue as IIntelliboxResultsProvider;
                //Create the wrapper used to make the calls async. This hides the details from the user.
                ib.SearchProvider = new IntelliboxAsyncProvider(provider.DoSearch);
            }
        }

        private void OnDisplayedValueBindingChanged() {
            if (ResultsList != null) {
                this.SetBinding(Intellibox.DisplayTextFromHighlightedItemProperty,
                    BindingBaseFactory.ConstructBindingForHighlighted(this, DisplayedValueBinding));
            }

            this.SetBinding(Intellibox.DisplayTextFromSelectedItemProperty,
                BindingBaseFactory.ConstructBindingForSelected(this, DisplayedValueBinding));
        }

        private void OnListItemMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            ChooseCurrentItem();
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

        private void OnSearchBeginning(string term, int max, object data) {
            var e = SearchBeginning;
            if (e != null) {
                e(term, max, data);
            }
        }

        private void OnSearchCompleted() {
            var e = SearchCompleted;
            if (e != null) {
                e();
            }
        }

        /// <summary>
        /// Called whenever (and ONLY whenever) the user has either
        /// 1. selected an item from the result set
        /// 2. decided not to select an item from the result set
        /// 3. cleared the currently selected item
        /// </summary>
        private void OnUserEndedSearchEvent() {
            if (SearchTimer != null) {
                SearchTimer.Stop();
                //setting to null so that when a new search starts, we grab fresh values for the time interval
                SearchTimer = null;
            }

            if (WaitNotificationTimer != null) {
                WaitNotificationTimer.Stop();
                //setting to null so that when a new search starts, we grab fresh values for the time interval
                WaitNotificationTimer = null;
            }

            if (SearchProvider != null) {
                SearchProvider.CancelAllSearches();
            }

            ShowResults = false;
            noResultsPopup.IsOpen = false;
            waitingForResultsPopup.IsOpen = false;
        }

        private void OnSearchTimerTick(object sender, EventArgs e) {
            if (IsSearchInProgress) {
                var last = ApplyDisableWhitespaceTrim(_lastTextValue);
                var current = ApplyDisableWhitespaceTrim(PART_EDITFIELD.Text);

                // we don't want to search for an empty string, but unlike the first search, we don't want
                // empty search strings to cancel existing searches, because that responsibility
                // belongs to the the code that kicks off the first search
                bool startAnotherSearch = !last.Equals(current) && !string.IsNullOrEmpty(current);
                if (startAnotherSearch) {
                    _lastTextValue = current;
                    OnSearchBeginning(current, MaxResults, Tag);
                    SearchProvider.BeginSearchAsync(_lastTextValue, DateTime.Now.ToUniversalTime(), MaxResults, Tag, ProcessSearchResults);
                }
            }
        }

        private void OnSelectedValueBindingChanged() {
            var bind = BindingBaseFactory.ConstructBindingForSelected(this, SelectedValueBinding);
            this.SetBinding(IntermediateSelectedValueProperty, bind);
        }

        private void OnTextBoxKeyUp(object sender, KeyEventArgs e) {
            if (!HasDataProvider)
                return;

            if (IsCancelKey(e.Key) || IsChooseCurrentItemKey(e.Key) || IsNavigationKey(e.Key)) {
                return;
            }

            var field = sender as TextBox;
            if (field != null) {
                PerformSearchActions(field.Text);
            }
        }

        private void OnTextBoxPreviewKeyDown(object sender, KeyEventArgs e) {
            if (!HasDataProvider || !ShowResults)
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

        private void PerformSearchActions(string enteredText) {
            enteredText = ApplyDisableWhitespaceTrim(enteredText);

            if (enteredText.Equals(_lastTextValue))
                return;

            if (string.IsNullOrEmpty(enteredText)) {
                ClearSelectedItem();
            }
            else {
                bool doSearchNow = !IsSearchInProgress && enteredText.Length >= MinimumPrefixLength;
                if (doSearchNow) {
                    SearchTimer = new DispatcherTimer(
                        TimeSpan.FromMilliseconds(MinimumSearchDelay),
                        DispatcherPriority.Background,
                        new EventHandler(OnSearchTimerTick),
                        this.Dispatcher);

                    WaitNotificationTimer = new DispatcherTimer(
                        TimeSpan.FromMilliseconds(TimeBeforeWaitNotification),
                        DispatcherPriority.Background,
                        new EventHandler(OnWaitNotificationTimerTick),
                        this.Dispatcher);

                    _lastTextValue = enteredText;
                    OnSearchBeginning(_lastTextValue, MaxResults, Tag);
                    SearchProvider.BeginSearchAsync(_lastTextValue, DateTime.Now.ToUniversalTime(), MaxResults, Tag, ProcessSearchResults);

                    SearchTimer.Start();
                    WaitNotificationTimer.Start();
                }
            }
        }

        private void OnWaitNotificationTimerTick(object sender, EventArgs args) {
            if (WaitNotificationTimer != null) {
                WaitNotificationTimer.Stop();
            }

            // this timer only needs to fire once
            WaitNotificationTimer = null;

            waitingForResultsPopup.IsOpen = IsSearchInProgress && !(ShowResults || noResultsPopup.IsOpen);
        }

        

        /// <summary>
        /// Called when a search completes to process the search results.
        /// </summary>
        /// <param name="startTimeUtc"></param>
        /// <param name="results"></param>
        private void ProcessSearchResults(DateTime startTimeUtc, IEnumerable<object> results) {
            if (_lastTimeSearchRecievedUtc > startTimeUtc)
                return; // this result set isn't fresh, so don't bother processing it

            _lastTimeSearchRecievedUtc = startTimeUtc;

            ShowResults = false;
            waitingForResultsPopup.IsOpen = false;

            var list = (results is IList)
                ? (IList)results //optimization to keep from making a copy of the list
                : (IList)results.ToList();

            noResultsPopup.IsOpen = list.Count < 1;
            Items = list;

            if (list.Count > 0) {
                if (_previousResultType != Items[0].GetType()) {
                    _previousResultType = Items[0].GetType();

                    ResultsList.View = ConstructGridView(Items[0]);
                }
                ResultsList.SelectedIndex = 0;
                ShowResults = true;
            }

            OnSearchCompleted();
        }

        private string UpdateSearchBoxText(bool useSelectedItem) {

            var text = useSelectedItem
                ? this.DisplayTextFromSelectedItem
                : this.DisplayTextFromHighlightedItem;

            PART_EDITFIELD.Text = text;
            if (!string.IsNullOrEmpty(text)) {
                PART_EDITFIELD.CaretIndex = text.Length;
            }

            return text;
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
