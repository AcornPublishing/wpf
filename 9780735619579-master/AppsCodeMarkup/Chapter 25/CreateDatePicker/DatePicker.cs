//-------------------------------------------
// DatePicker.cs (c) 2006 by Charles Petzold
//-------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CreateDatePicker
{
    public partial class DatePicker
    {
        UniformGrid unigridMonth;
        DateTime datetimeSaved = DateTime.Now.Date;

        // Define DateProperty dependency property.
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime?), 
                typeof(DatePicker), 
                new PropertyMetadata(new DateTime(), DateChangedCallback));

        // Define DateChangedEvent routed event.
        public static readonly RoutedEvent DateChangedEvent =
            EventManager.RegisterRoutedEvent("DateChanged", 
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<DateTime?>), 
                typeof(DatePicker));

        // Constructor.
        public DatePicker()
        {
            InitializeComponent();

            // Initialize Date property.
            Date = datetimeSaved;

            // Attach handler for Loaded event.
            Loaded += DatePickerOnLoaded;
        }
        // Handler for window Loaded event.
        void DatePickerOnLoaded(object sender, RoutedEventArgs args)
        {
            unigridMonth = FindUniGrid(lstboxMonth);

            if (Date != null)
            {
                DateTime dt = (DateTime)Date;
                unigridMonth.FirstColumn =
                    (int)(new DateTime(dt.Year, dt.Month, 1).DayOfWeek);
            }
        }

        // Recursive method to find UniformGrid.
        UniformGrid FindUniGrid(DependencyObject vis)
        {
            if (vis is UniformGrid)
                return vis as UniformGrid;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(vis); i++)
            {
                Visual visReturn = 
                    FindUniGrid(VisualTreeHelper.GetChild(vis, i));
                if (visReturn != null)
                    return visReturn as UniformGrid;
            }
            return null;
        }

        // Date property backed by DateProperty dependency property.
        public DateTime? Date
        {
            set { SetValue(DateProperty, value); }
            get { return (DateTime?)GetValue(DateProperty); }
        }

        // DateChanged event backed by DateChangedEvent routed event.
        public event RoutedPropertyChangedEventHandler<DateTime?> DateChanged
        {
            add { AddHandler(DateChangedEvent, value); }
            remove { RemoveHandler(DateChangedEvent, value); }
        }

        // Back and Forward repeat buttons.
        void ButtonBackOnClick(object sender, RoutedEventArgs args)
        {
            FlipPage(true);
        }
        void ButtonForwardOnClick(object sender, RoutedEventArgs args)
        {
            FlipPage(false);
        }

        // Buttons are duplicated by PageDown and PageUp keys.
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnKeyDown(args);

            if (args.Key == Key.PageDown)
            {
                FlipPage(true);
                args.Handled = true;
            }
            else if (args.Key == Key.PageUp)
            {
                FlipPage(false);
                args.Handled = false;
            }
        }

        // Flip the page to the next month, year, decade, etc.
        void FlipPage(bool isBack)
        {
            if (Date == null)
                return;

            DateTime dt = (DateTime)Date;
            int numPages = isBack ? -1 : 1;

            // If Shift down, go by years.
            if (Keyboard.IsKeyDown(Key.LeftShift) || 
                Keyboard.IsKeyDown(Key.RightShift))
                    numPages *= 12;

            // If Ctrl down, go by decades.
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || 
                Keyboard.IsKeyDown(Key.RightCtrl))
                    numPages = Math.Max(-1200, Math.Min(1200, 120 * numPages));

            // Calculate new DateTime.
            int year = dt.Year + numPages / 12;
            int month = dt.Month + numPages % 12;

            while (month < 1)
            {
                month += 12;
                year -= 1;
            }
            while (month > 12)
            {
                month -= 12;
                year += 1;
            }
           
            // Set Date property (generates DateChangedCallback).
            if (year < DateTime.MinValue.Year)
                Date = DateTime.MinValue.Date;
            else if (year > DateTime.MaxValue.Year)
                Date = DateTime.MaxValue.Date;
            else
                Date = new DateTime(year, month, 
                    Math.Min(dt.Day, DateTime.DaysInMonth(year, month)));
        }

        // If CheckBox being checked, save the Date property and set to null.
        void CheckBoxNullOnChecked(object sender, RoutedEventArgs args)
        {
            if (Date != null)
            {
                datetimeSaved = (DateTime)Date;
                Date = null;
            }
        }

        // If CheckBox being unchecked, restore the Date property.
        void CheckBoxNullOnUnchecked(object sender, RoutedEventArgs args)
        {
            Date = datetimeSaved;
        }

        // For ListBox selection change, change the day of the month.
        void ListBoxOnSelectionChanged(object sender, 
                                       SelectionChangedEventArgs args)
        {
            if (Date == null)
                return;

            DateTime dt = (DateTime)Date;

            // Set new Date property (generates DateChangedCallback).
            if (lstboxMonth.SelectedIndex != -1)
                Date = new DateTime(dt.Year, dt.Month, 
                            Int32.Parse(lstboxMonth.SelectedItem as string));
        }

        // This method is triggered by the DateProperty.
        static void DateChangedCallback(DependencyObject obj, 
                                        DependencyPropertyChangedEventArgs args)
        {
            // Call OnDateChange for the object being changed.
            (obj as DatePicker).OnDateChanged((DateTime?)args.OldValue, 
                                              (DateTime?)args.NewValue);
        }

        // OnDateChanged changes the visuals to match the new value of Date.
        protected virtual void OnDateChanged(DateTime? dtOldValue, 
                                             DateTime? dtNewValue)
        {
            chkboxNull.IsChecked = dtNewValue == null;

            if (dtNewValue != null)
            {
                DateTime dtNew = (DateTime)dtNewValue;

                // Set the month and year text.
                txtblkMonthYear.Text = dtNew.ToString(
                        DateTimeFormatInfo.CurrentInfo.YearMonthPattern);

                // Set the first day of the month.
                if (unigridMonth != null)
                    unigridMonth.FirstColumn = 
                        (int)(new DateTime(dtNew.Year, 
                                           dtNew.Month, 1).DayOfWeek);

                int iDaysInMonth = DateTime.DaysInMonth(dtNew.Year, 
                                                        dtNew.Month);

                // Fill up the ListBox if the number of days isn't right.
                if (iDaysInMonth != lstboxMonth.Items.Count)
                {
                    lstboxMonth.BeginInit();
                    lstboxMonth.Items.Clear();

                    for (int i = 0; i < iDaysInMonth;  i++)
                        lstboxMonth.Items.Add((i + 1).ToString());

                    lstboxMonth.EndInit();
                }
                lstboxMonth.SelectedIndex = dtNew.Day - 1;
            }

            // Now trigger the DateChangedEvent.
            RoutedPropertyChangedEventArgs<DateTime?> args =
                new RoutedPropertyChangedEventArgs<DateTime?>(dtOldValue, 
                                dtNewValue, DatePicker.DateChangedEvent);
            args.Source = this;
            RaiseEvent(args);
        }
    }
}
