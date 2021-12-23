//-------------------------------------------------------
// ClickTheGradientCenter.cs (c) 2006 by Charles Petzold
//-------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ClickTheGradientCenter
{
    class ClickTheRadientCenter : Window
    {
        RadialGradientBrush brush;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ClickTheRadientCenter());
        }
        public ClickTheRadientCenter()
        {
            Title = "Click the Gradient Center";
            brush = new RadialGradientBrush(Colors.White, Colors.Red);
            brush.RadiusX = brush.RadiusY = 0.10;
            brush.SpreadMethod = GradientSpreadMethod.Repeat;
            Background = brush;
        }
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            double width = ActualWidth
                - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;
            double height = ActualHeight
                - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight
                - SystemParameters.CaptionHeight;

            Point ptMouse = args.GetPosition(this);
            ptMouse.X /= width;
            ptMouse.Y /= height;

            if (args.ChangedButton == MouseButton.Left)
            {
                brush.Center = ptMouse;
                brush.GradientOrigin = ptMouse;
            }
            else if (args.ChangedButton == MouseButton.Right)
                brush.GradientOrigin = ptMouse;
        }
    }
}
