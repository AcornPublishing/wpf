//-------------------------------------------------------
// FormattedTextConverter.cs (c) 2006 by Charles Petzold
//-------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petzold.FormattedDigitalClock
{
    public class FormattedTextConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, 
                              object param, CultureInfo culture)
        {
            if (param is string)
                return String.Format(param as string, value);

            return value.ToString();
        }
        public object ConvertBack(object value, Type typeTarget, 
                                  object param, CultureInfo culture)
        {
            return null;
        }
    }
}
