//----------------------------------------------------
// DrawButtonsOnBitmap.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.DrawButtonsOnBitmap
{
    public class DrawButtonsOnBitmap : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DrawButtonsOnBitmap());
        }
        public DrawButtonsOnBitmap()
        {
            Title = "Draw Buttons on Bitmap";

            // Create a UniformGrid for hosting buttons.
            UniformGrid unigrid = new UniformGrid();
            unigrid.Columns = 4;

            // Create 32 ToggleButton objects on UniformGrid.
            for (int i = 0; i < 32; i++)
            {
                ToggleButton btn = new ToggleButton();
                btn.Width = 96;
                btn.Height = 24;
                btn.IsChecked = (i < 4 | i > 27) ^ (i % 4 == 0 | i % 4 == 3);
                unigrid.Children.Add(btn);
            }

            // Size the UniformGrid.
            unigrid.Measure(new Size(Double.PositiveInfinity,
                                     Double.PositiveInfinity));

            Size szGrid = unigrid.DesiredSize;

            // Arrange the UniformGrid.
            unigrid.Arrange(new Rect(new Point(0, 0), szGrid));

            // Create the RenderTargetBitmap object.
            RenderTargetBitmap renderbitmap =
                new RenderTargetBitmap((int)Math.Ceiling(szGrid.Width),
                                       (int)Math.Ceiling(szGrid.Height),
                                       96, 96, PixelFormats.Default);

            // Render the UniformGrid on the RenderTargetBitmap.
            renderbitmap.Render(unigrid);

            // Create an Image object and set its Source to the bitmap.
            Image img = new Image();
            img.Source = renderbitmap;

            // Make the Image object the content of the window.
            Content = img;
        }
    }
}
