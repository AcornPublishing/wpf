//-------------------------------------------------
// EllipseWithChild.cs (c) 2006 by Charles Petzold
//-------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.EncloseElementInEllipse
{
    public class EllipseWithChild : Petzold.RenderTheBetterEllipse.BetterEllipse
    {
        UIElement child;

        // Public Child property.
        public UIElement Child
        {
            set
            {
                if (child != null)
                {
                    RemoveVisualChild(child);
                    RemoveLogicalChild(child);
                }
                if ((child = value) != null)
                {
                    AddVisualChild(child);
                    AddLogicalChild(child);
                }
            }
            get
            {
                return child;
            }
        }
        // Override of VisualChildrenCount returns 1 if Child is non-null.
        protected override int VisualChildrenCount
        {
            get
            {
                return Child != null ? 1 : 0;
            }
        }
        // Override of GetVisualChildren returns Child.
        protected override Visual GetVisualChild(int index)
        {
            if (index > 0 || Child == null)
                throw new ArgumentOutOfRangeException("index");

            return Child;
        }
        // Override of MeasureOverride calls child's Measure method.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            Size sizeDesired = new Size(0, 0);

            if (Stroke != null)
            {
                sizeDesired.Width += 2 * Stroke.Thickness;
                sizeDesired.Height += 2 * Stroke.Thickness;

                sizeAvailable.Width = 
                    Math.Max(0, sizeAvailable.Width - 2 * Stroke.Thickness);
                sizeAvailable.Height = 
                    Math.Max(0, sizeAvailable.Height - 2 * Stroke.Thickness);
            }
            if (Child != null)
            {
                Child.Measure(sizeAvailable);

                sizeDesired.Width += Child.DesiredSize.Width;
                sizeDesired.Height += Child.DesiredSize.Height;
            }
            return sizeDesired;
        }
        // Override of ArrangeOverride calls child's Arrange method.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            if (Child != null)
            {
                Rect rect = new Rect(
                    new Point((sizeFinal.Width - Child.DesiredSize.Width) / 2,
                              (sizeFinal.Height - Child.DesiredSize.Height) / 2),
                              Child.DesiredSize);
                Child.Arrange(rect);
            }
            return sizeFinal;
        }
    }
}
