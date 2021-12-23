//---------------------------------------------------------
// SplineKeyFrameExperiment.cs (c) 2006 by Charles Petzold
//---------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.SplineKeyFrameExperiment
{
    public partial class SplineKeyFrameExperiment : Window
    {
        // Two dependency properties for ControlPoint1 and ControlPoint2.
        public static DependencyProperty ControlPoint1Property =
            DependencyProperty.Register("ControlPoint1", typeof(Point),
                typeof(SplineKeyFrameExperiment),
                new PropertyMetadata(new Point(0, 0), ControlPointOnChanged));

        public static DependencyProperty ControlPoint2Property =
            DependencyProperty.Register("ControlPoint2", typeof(Point),
                typeof(SplineKeyFrameExperiment),
                new PropertyMetadata(new Point(1, 1), ControlPointOnChanged));

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SplineKeyFrameExperiment());
        }

        // Constructor: Mostly draws tick marks too numerous for XAML.
        public SplineKeyFrameExperiment()
        {
            InitializeComponent();

            for (int i = 0; i <= 10; i++)
            {
                // Horizontal text and lines.
                TextBlock txtblk = new TextBlock();
                txtblk.Text = (i / 10m).ToString("N1");
                canvMain.Children.Add(txtblk);
                Canvas.SetLeft(txtblk, 40 + 48 * i);
                Canvas.SetTop(txtblk, 14);

                Line line = new Line();
                line.X1 = 48 * (i + 1);
                line.Y1 = 30;
                line.X2 = line.X1;
                line.Y2 = 528;
                line.Stroke = Brushes.Black;
                canvMain.Children.Add(line);

                // Vertical text and lines.
                txtblk = new TextBlock();
                txtblk.Text = (i / 10m).ToString("N1");
                canvMain.Children.Add(txtblk);
                Canvas.SetLeft(txtblk, 5);
                Canvas.SetTop(txtblk, 40 + 48 * i);

                line = new Line();
                line.X1 = 30;
                line.Y1 = 48 * (i + 1);
                line.X2 = 528;
                line.Y2 = line.Y1;
                line.Stroke = Brushes.Black;
                canvMain.Children.Add(line);
            }
            UpdateLabel();
        }

        // ControlPoint1 and ControlPoint2 properties.
        public Point ControlPoint1
        {
            set { SetValue(ControlPoint1Property, value); }
            get { return (Point)GetValue(ControlPoint1Property); }
        }
        public Point ControlPoint2
        {
            set { SetValue(ControlPoint2Property, value); }
            get { return (Point)GetValue(ControlPoint2Property); }
        }

        // Called whenever one of the ControlPoint properties changes.
        static void ControlPointOnChanged(DependencyObject obj, 
                                    DependencyPropertyChangedEventArgs args)
        {
            SplineKeyFrameExperiment win = obj as SplineKeyFrameExperiment;

            // Set KeySpline element in XAML animation.
            if (args.Property == ControlPoint1Property)
                win.spline.ControlPoint1 = (Point)args.NewValue;

            else if (args.Property == ControlPoint2Property)
                win.spline.ControlPoint2 = (Point)args.NewValue;
        }
        // Handles MouseDown and MouseMove events.
        void CanvasOnMouse(object sender, MouseEventArgs args)
        {
            Canvas canv = sender as Canvas;
            Point ptMouse = args.GetPosition(canv);
            ptMouse.X = Math.Min(1, Math.Max(0, ptMouse.X / canv.ActualWidth));
            ptMouse.Y = Math.Min(1, Math.Max(0, ptMouse.Y / canv.ActualHeight));

            // Set ControlPoint properties.
            if (args.LeftButton == MouseButtonState.Pressed)
                ControlPoint1 = ptMouse;

            if (args.RightButton == MouseButtonState.Pressed)
                ControlPoint2 = ptMouse;

            // Update the label showing ControlPoint properties.
            if (args.LeftButton == MouseButtonState.Pressed || 
                args.RightButton == MouseButtonState.Pressed)
                    UpdateLabel();
        }
        // Set content of XAML Label.
        void UpdateLabel()
        {
            lblInfo.Content = String.Format(
                "Left mouse button changes ControlPoint1 = ({0:F2})\n" +
                "Right mouse button changes ControlPoint2 = ({1:F2})",
                ControlPoint1, ControlPoint2);
        }
    }
}
