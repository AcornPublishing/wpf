//----------------------------------------------------
// RgbToColorConverter.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Petzold.ColorScroll
{
    public class RgbToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type typeTarget, 
                              object param, CultureInfo culture)
        {
            Color clr = Color.FromRgb((byte)(double)value[0], 
                                      (byte)(double)value[1],
                                      (byte)(double)value[2]);

            if (typeTarget == typeof(Color))
                return clr;

            if (typeTarget == typeof(Brush))
                return new SolidColorBrush(clr);

            return null;
        }
        public object[] ConvertBack(object value, Type[] typeTarget, 
                                    object param, CultureInfo culture)
        {
            Color clr;
            object[] primaries = new object[3];

            if (value is Color)
                clr = (Color) value;

            else if (value is SolidColorBrush)
                clr = (value as SolidColorBrush).Color;

            else
                return null;

            primaries[0] = clr.R;
            primaries[1] = clr.G;
            primaries[2] = clr.B;
            return primaries;
        }
    }
}
