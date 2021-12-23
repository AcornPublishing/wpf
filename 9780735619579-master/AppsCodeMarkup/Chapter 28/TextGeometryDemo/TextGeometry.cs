//---------------------------------------------
// TextGeometry.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Petzold.TextGeometryDemo
{
    public class TextGeometry
    {
        // Private fields backing public properties.
        string txt = "";
        FontFamily fntfam = new FontFamily();
        FontStyle fntstyle = FontStyles.Normal;
        FontWeight fntwt = FontWeights.Normal;
        FontStretch fntstr = FontStretches.Normal;
        double emsize = 24;
        Point ptOrigin = new Point(0, 0);

        // Public Properties.
        public string Text
        {
            set { txt = value; }
            get { return txt; }
        }
        public FontFamily FontFamily
        {
            set { fntfam = value; }
            get { return fntfam; }
        }
        public FontStyle FontStyle
        {
            set { fntstyle = value; }
            get { return fntstyle; }
        }
        public FontWeight FontWeight
        {
            set { fntwt = value; }
            get { return fntwt; }
        }
        public FontStretch FontStretch
        {
            set { fntstr = value; }
            get { return fntstr; }
        }
        public double FontSize
        {
            set { emsize = value; }
            get { return emsize; }
        }
        public Point Origin
        {
            set { ptOrigin = value; }
            get { return ptOrigin; }
        }

        // Public read-only property to return Geometry object.
        public Geometry Geometry
        {
            get
            {
                FormattedText formtxt = 
                    new FormattedText(Text, CultureInfo.CurrentCulture, 
                                      FlowDirection.LeftToRight,
                                      new Typeface(FontFamily, FontStyle, 
                                                   FontWeight, FontStretch), 
                                      FontSize, Brushes.Black);

                return formtxt.BuildGeometry(Origin);
            }
        }

        // Required for animations using paths.
        public PathGeometry PathGeometry
        {
            get
            {
                return PathGeometry.CreateFromGeometry(Geometry);
            }
        }

    }
}
