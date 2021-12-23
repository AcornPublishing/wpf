//-----------------------------------------
// SineWave.cs (c) 2006 by Charles Petzold
//-----------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SineWave
{
    public class SineWave : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SineWave());
        }
        public SineWave()
        {
            Title = "Sine Wave";

            // Make Polyline content of window.
            Polyline poly = new Polyline();
            poly.VerticalAlignment = VerticalAlignment.Center;
            poly.Stroke = SystemColors.WindowTextBrush;
            poly.StrokeThickness = 2;
            Content = poly;

            // Define the points.
            for (int i = 0; i < 2000; i++)
                poly.Points.Add(
                    new Point(i, 96 * (1 - Math.Sin(i * Math.PI / 192))));
        }
    }
}
