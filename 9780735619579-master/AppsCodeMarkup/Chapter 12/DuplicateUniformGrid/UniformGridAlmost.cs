//--------------------------------------------------
// UniformGridAlmost.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.DuplicateUniformGrid
{
    class UniformGridAlmost : Panel
    {
        // Public static readonly dependency properties.
        public static readonly DependencyProperty ColumnsProperty;

        // Static constructor to create dependency property.
        static UniformGridAlmost()
        {
            ColumnsProperty =
                DependencyProperty.Register(
                    "Columns", typeof(int), typeof(UniformGridAlmost),
                    new FrameworkPropertyMetadata(1,
                            FrameworkPropertyMetadataOptions.AffectsMeasure));
        }
        // Columns property.
        public int Columns
        {
            set { SetValue(ColumnsProperty, value); }
            get { return (int)GetValue(ColumnsProperty); }
        }
        // Read-Only Rows property.
        public int Rows
        {
            get { return (InternalChildren.Count + Columns - 1) / Columns; }
        }
        // Override of MeasureOverride apportions space.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            // Calculate a child size based on uniform rows and columns.
            Size sizeChild = new Size(sizeAvailable.Width / Columns,
                                      sizeAvailable.Height / Rows);

            // Variables to accumulate maximum widths and heights.
            double maxwidth = 0;
            double maxheight = 0;

            foreach (UIElement child in InternalChildren)
            {
                // Call Measure for each child ...
                child.Measure(sizeChild);

                // ... and then examine DesiredSize property of child.
                maxwidth = Math.Max(maxwidth, child.DesiredSize.Width);
                maxheight = Math.Max(maxheight, child.DesiredSize.Height);
            }
            // Now calculate a desired size for the grid itself.
            return new Size(Columns * maxwidth, Rows * maxheight);
        }
        // Override of ArrangeOverride positions children.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            // Calculate a child size based on uniform rows and columns.
            Size sizeChild = new Size(sizeFinal.Width / Columns,
                                      sizeFinal.Height / Rows);

            for (int index = 0; index < InternalChildren.Count; index++)
            {
                int row = index / Columns;
                int col = index % Columns;

                // Calculate a rectangle for each child within sizeFinal ...
                Rect rectChild = 
                    new Rect(new Point(col * sizeChild.Width, 
                                       row * sizeChild.Height),
                             sizeChild);

                // ... and call Arrange for that child.
                InternalChildren[index].Arrange(rectChild);
            }
            return sizeFinal;
        }
    }
}
