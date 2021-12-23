//-----------------------------------------------------
// DrawGraphicsOnBitmap.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.DrawGraphicsOnBitmap
{
    public class DrawGraphicsOnBitmap : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DrawGraphicsOnBitmap());
        }
        public DrawGraphicsOnBitmap()
        {
            Title = "Draw Graphics on Bitmap";

            // Set background to demonstrate transparency of bitmap.
            Background = Brushes.Khaki;

            // Create the RenderTargetBitmap object.
            RenderTargetBitmap renderbitmap =
                new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Default);

            // Create a DrawingVisual objects.
            DrawingVisual drawvis = new DrawingVisual();
            DrawingContext dc = drawvis.RenderOpen();
            dc.DrawRoundedRectangle(Brushes.Blue, new Pen(Brushes.Red, 10),
                                    new Rect(25, 25, 50, 50), 10, 10);
            dc.Close();

            // Render the DrawingVisual on the RenderTargetBitmap.
            renderbitmap.Render(drawvis);

            // Create an Image object and set its Source to the bitmap.
            Image img = new Image();
            img.Source = renderbitmap;

            // Make the Image object the content of the window.
            Content = img;
        }
    }
}