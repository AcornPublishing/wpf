//---------------------------------------------------------
// DoubleToDecimalConverter.cs (c) 2006 by Charles Petzold
//---------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petzold.DecimalScrollBar
{
    [ValueConversion(typeof(double), typeof(decimal))]
    public class DoubleToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type typeTarget, 
                              object param, CultureInfo culture)
        {
            decimal num = new Decimal((double)value);

            if (param != null)
                num = Decimal.Round(num, Int32.Parse(param as string));

            return num;
        }
        public object ConvertBack(object value, Type typeTarget, 
                                  object param, CultureInfo culture)
        {
            return Decimal.ToDouble((decimal)value);
        }
    }
}
