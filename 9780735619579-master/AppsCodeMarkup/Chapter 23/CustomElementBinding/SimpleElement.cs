//----------------------------------------------
// SimpleElement.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Petzold.CustomElementBinding
{
    class SimpleElement : FrameworkElement
    {
        // Define DependencyProperty.
        public static DependencyProperty NumberProperty;

        // Create DependencyProperty in static constructor.
        static SimpleElement()
        {
            NumberProperty = 
                DependencyProperty.Register("Number", typeof(double), 
                                            typeof(SimpleElement),
                    new FrameworkPropertyMetadata(0.0, 
                            FrameworkPropertyMetadataOptions.AffectsRender));
        }

        // Expose DependencyProperty as CLR property.
        public double Number
        {
            set { SetValue(NumberProperty, value); }
            get { return (double)GetValue(NumberProperty); }
        }

        // Hard-coded size for MeasureOverride.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            return new Size(200, 50);
        }

        // OnRender just displays Number property.
        protected override void OnRender(DrawingContext dc)    
        {
            dc.DrawText(
                new FormattedText(Number.ToString(), 
                        CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                        new Typeface("Times New Roman"), 12, 
                        SystemColors.WindowTextBrush), 
                new Point(0, 0)); 
        }
    }
}
