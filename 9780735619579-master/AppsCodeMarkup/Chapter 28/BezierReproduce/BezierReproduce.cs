//------------------------------------------------
// BezierReproduce.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.BezierReproduce
{
    public class BezierReproduce : Petzold.BezierExperimenter.BezierExperimenter
    {
        Polyline bezier;

        [STAThread]
        public new static void Main()
        {
            Application app = new Application();
            app.Run(new BezierReproduce());
        }
        public BezierReproduce()
        {
            Title = "Bezier Reproduce";

            bezier = new Polyline();
            bezier.Stroke = Brushes.Blue;
            canvas.Children.Add(bezier);
        }
        protected override void CanvasOnSizeChanged(object sender, 
                                        SizeChangedEventArgs args)
        {
            base.CanvasOnSizeChanged(sender, args);
            DrawBezierManually();
        }
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);
            DrawBezierManually();
        }
        protected override void OnMouseMove(MouseEventArgs args)
        {
            base.OnMouseMove(args);
            DrawBezierManually();
        }
        void DrawBezierManually()
        {
            Point[] pts = new Point[10];

            for (int i = 0; i < pts.Length; i++)
            {
                double t = (double)i / (pts.Length - 1);

                double x = (1 - t) * (1 - t) * (1 - t) * ptStart.Center.X +
                           3 * t * (1 - t) * (1 - t) * ptCtrl1.Center.X +
                           3 * t * t * (1 - t) * ptCtrl2.Center.X +
                           t * t * t * ptEnd.Center.X;

                double y = (1 - t) * (1 - t) * (1 - t) * ptStart.Center.Y +
                           3 * t * (1 - t) * (1 - t) * ptCtrl1.Center.Y +
                           3 * t * t * (1 - t) * ptCtrl2.Center.Y +
                           t * t * t * ptEnd.Center.Y;

                pts[i] = new Point(x, y);
            }
            bezier.Points = new PointCollection(pts);
        }
    }
}