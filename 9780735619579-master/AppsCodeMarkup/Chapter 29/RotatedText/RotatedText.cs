//--------------------------------------------
// RotatedText.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.RotatedText
{
    public class RotatedText : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RotatedText());
        }
        public RotatedText()
        {
            Title = "Rotated Text";

            // Make Canvas content of window.
            Canvas canv = new Canvas();
            Content = canv;

            // Display 18 rotated TextBlock elements.
            for (int angle = 0; angle < 360; angle += 20)
            {
                TextBlock txtblk = new TextBlock();
                txtblk.FontFamily = new FontFamily("Arial");
                txtblk.FontSize = 24;
                txtblk.Text = "     Rotated Text";
                txtblk.RenderTransformOrigin = new Point(0, 0.5);
                txtblk.RenderTransform = new RotateTransform(angle);

                canv.Children.Add(txtblk);
                Canvas.SetLeft(txtblk, 200);
                Canvas.SetTop(txtblk, 200);
            }
        }
    }
}