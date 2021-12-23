//------------------------------------------------
// ShadowTheStylus.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ShadowTheStylus
{
    public class ShadowTheStylus : Window
    {
        // Define some constants for the stylus polylines.
        static readonly SolidColorBrush brushStylus = Brushes.Blue;
        static readonly SolidColorBrush brushShadow = Brushes.LightBlue;
        static readonly double widthStroke = 96 / 2.54;       // 1 cm
        static readonly Vector vectShadow = 
                        new Vector(widthStroke / 4, widthStroke / 4);

        // More fields for stylus-move operations.
        Canvas canv;
        Polyline polyStylus, polyShadow;
        bool isDrawing;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShadowTheStylus());
        }
        public ShadowTheStylus()
        {
            Title = "Shadow the Stylus";

            // Create a Canvas for window content.
            canv = new Canvas();
            Content = canv;
        }
        protected override void OnStylusDown(StylusDownEventArgs args)
        {
            base.OnStylusDown(args);
            Point ptStylus = args.GetPosition(canv);

            // Create a Polyline with rounded ends and joins for the foreground.
            polyStylus = new Polyline();
            polyStylus.Stroke = brushStylus;
            polyStylus.StrokeThickness = widthStroke;
            polyStylus.StrokeStartLineCap = PenLineCap.Round;
            polyStylus.StrokeEndLineCap = PenLineCap.Round;
            polyStylus.StrokeLineJoin = PenLineJoin.Round;
            polyStylus.Points = new PointCollection();
            polyStylus.Points.Add(ptStylus);

            // Another Polyline for the shadow.
            polyShadow = new Polyline();
            polyShadow.Stroke = brushShadow;
            polyShadow.StrokeThickness = widthStroke;
            polyShadow.StrokeStartLineCap = PenLineCap.Round;
            polyShadow.StrokeEndLineCap = PenLineCap.Round;
            polyShadow.StrokeLineJoin = PenLineJoin.Round;
            polyShadow.Points = new PointCollection();
            polyShadow.Points.Add(ptStylus + vectShadow);

            // Insert shadow before all foreground polylines.
            canv.Children.Insert(canv.Children.Count / 2, polyShadow);

            // Foreground can go at end.
            canv.Children.Add(polyStylus);

            CaptureStylus();
            isDrawing = true;
            args.Handled = true;
        }
        protected override void OnStylusMove(StylusEventArgs args)
        {
            base.OnStylusMove(args);

            if (isDrawing)
            {
                Point ptStylus = args.GetPosition(canv);
                polyStylus.Points.Add(ptStylus);
                polyShadow.Points.Add(ptStylus + vectShadow);
                args.Handled = true;
            }
        }
        protected override void OnStylusUp(StylusEventArgs args)
        {
            base.OnStylusUp(args);

            if (isDrawing)
            {
                isDrawing = false;
                ReleaseStylusCapture();
                args.Handled = true;
            }
        }
        protected override void OnTextInput(TextCompositionEventArgs args)
        {
            base.OnTextInput(args);

            // End drawing with press of Escape key.
            if (isDrawing && args.Text.IndexOf('\x1B') != -1)
            {
                ReleaseStylusCapture();
                args.Handled = true;
            }
        }
        protected override void OnLostStylusCapture(StylusEventArgs args)
        {
            base.OnLostStylusCapture(args);

            // Abnormal end of drawing: Remove child shapes.
            if (isDrawing)
            {
                canv.Children.Remove(polyStylus);
                canv.Children.Remove(polyShadow);
                isDrawing = false;
            }
        }
    }
}
