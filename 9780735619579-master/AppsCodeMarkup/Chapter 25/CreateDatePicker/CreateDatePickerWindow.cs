//-------------------------------------------------------
// CreateDatePickerWindow.cs (c) 2006 by Charles Petzold
//-------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CreateDatePicker
{
    public partial class CreateDatePickerWindow : Window
    {
        public CreateDatePickerWindow()
        {
            InitializeComponent();
        }

        // Handler for DatePicker DateChanged event.
        void DatePickerOnDateChanged(object sender,
                        RoutedPropertyChangedEventArgs<DateTime?> args)
        {
            if (args.NewValue != null)
            {
                DateTime dt = (DateTime)args.NewValue;
                txtblkDate.Text = dt.ToString("d");
            }
            else
                txtblkDate.Text = "";
        }
    }
}
