//------------------------------------------------
// YellowPadWindow.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.YellowPad
{
    public partial class YellowPadWindow : Window
    {
        // Make the pad 5 inches by 7 inches.
        public static readonly double widthCanvas = 5 * 96;
        public static readonly double heightCanvas = 7 * 96;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new YellowPadWindow());
        }
        public YellowPadWindow()
        {
            InitializeComponent();

            // Draw blue horizontal lines 1/4 inch apart.
            double y = 96;

            while (y < heightCanvas)
            {
                Line line = new Line();
                line.X1 = 0;
                line.Y1 = y;
                line.X2 = widthCanvas;
                line.Y2 = y;
                line.Stroke = Brushes.LightBlue;
                inkcanv.Children.Add(line);

                y += 24;
            }

            // Disable the Eraser-Mode menu item if there's no tablet present.
            if (Tablet.TabletDevices.Count == 0)
                menuEraserMode.Visibility = Visibility.Collapsed;
        }
    }
}
