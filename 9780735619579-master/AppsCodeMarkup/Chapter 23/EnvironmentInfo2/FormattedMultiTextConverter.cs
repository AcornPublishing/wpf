//------------------------------------------------------------
// FormattedMultiTextConverter.cs (c) 2006 by Charles Petzold
//------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petzold.EnvironmentInfo2
{
    public class FormattedMultiTextConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type typeTarget, 
                              object param, CultureInfo culture)
        {
            return String.Format((string) param, value);
        }
        public object[] ConvertBack(object value, Type[] typeTarget, 
                                    object param, CultureInfo culture)
        {
            return null;
        }
    }
}
