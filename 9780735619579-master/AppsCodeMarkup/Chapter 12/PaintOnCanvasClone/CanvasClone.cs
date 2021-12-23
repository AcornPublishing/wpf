//--------------------------------------------
// CanvasClone.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PaintOnCanvasClone
{
    public class CanvasClone : Panel
    {
        // Define two dependency properties.
        public static readonly DependencyProperty LeftProperty;
        public static readonly DependencyProperty TopProperty;

        static CanvasClone()
        {
            // Register the dependency properties as attached properties.
            //  Default value is 0 and any change invalidates parent's arrange.
            LeftProperty = DependencyProperty.RegisterAttached("Left",
                    typeof(double), typeof(CanvasClone),
                    new FrameworkPropertyMetadata(0.0, 
                        FrameworkPropertyMetadataOptions.AffectsParentArrange));

            TopProperty = DependencyProperty.RegisterAttached("Top",
                    typeof(double), typeof(CanvasClone), 
                    new FrameworkPropertyMetadata(0.0, 
                        FrameworkPropertyMetadataOptions.AffectsParentArrange));
        }
        // Static methods to set and get attached properties.
        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }
        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }
        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }
        // Override of MeasureOverride just calls Measure on children.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            foreach (UIElement child in InternalChildren)
                child.Measure(new Size(Double.PositiveInfinity, 
                                       Double.PositiveInfinity));

            // Return default value (0, 0).
            return base.MeasureOverride(sizeAvailable);
        }
        // Override of ArrangeOverride positions children.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            foreach (UIElement child in InternalChildren)
                child.Arrange(new Rect(
                    new Point(GetLeft(child), GetTop(child)), child.DesiredSize));

            return sizeFinal;
        }
    }
}
