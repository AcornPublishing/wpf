//------------------------------------------
// Constants.cs (c) 2006 by Charles Petzold
//------------------------------------------
using System;
using System.Windows;
using System.Windows.Media;

namespace Petzold.AccessStaticFields
{
    public static class Constants
    {
        // Public static members.
        public static readonly FontFamily fntfam =
            new FontFamily("Times New Roman Italic");

        public static double FontSize
        {
            get { return 72 / 0.75; }
        }

        public static readonly LinearGradientBrush brush =
            new LinearGradientBrush(Colors.LightGray, Colors.DarkGray,
                                    new Point(0, 0), new Point(1, 1));
    }
}