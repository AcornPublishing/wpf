//-------------------------------------
// Tile.cs (c) 2006 by Charles Petzold
//-------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.PlayJeuDeTacquin
{
    public class Tile : Canvas
    {
        const int SIZE = 64;    // 2/3 inch
        const int BORD = 6;     // 1/16 inch
        TextBlock txtblk;

        public Tile()
        {
            Width = SIZE;
            Height = SIZE;

            // Upper-left shadowed border.
            Polygon poly = new Polygon();
            poly.Points = new PointCollection(new Point[] 
                { 
                    new Point(0, 0), new Point(SIZE, 0), 
                    new Point(SIZE-BORD, BORD),
                    new Point(BORD, BORD), 
                    new Point(BORD, SIZE-BORD), new Point(0, SIZE)
                });
            poly.Fill = SystemColors.ControlLightLightBrush;
            Children.Add(poly);

            // Lower-right shadowed border.
            poly = new Polygon();
            poly.Points = new PointCollection(new Point[] 
                { 
                    new Point(SIZE, SIZE), new Point(SIZE, 0), 
                    new Point(SIZE-BORD, BORD),
                    new Point(SIZE-BORD, SIZE-BORD), 
                    new Point(BORD, SIZE-BORD), new Point(0, SIZE)
                });
            poly.Fill = SystemColors.ControlDarkBrush;
            Children.Add(poly);

            // Host for centered text.
            Border bord = new Border();
            bord.Width = SIZE - 2 * BORD;
            bord.Height = SIZE - 2 * BORD;
            bord.Background = SystemColors.ControlBrush;
            Children.Add(bord);
            SetLeft(bord, BORD);
            SetTop(bord, BORD);

            // Display of text.
            txtblk = new TextBlock();
            txtblk.FontSize = 32;
            txtblk.Foreground = SystemColors.ControlTextBrush;
            txtblk.HorizontalAlignment = HorizontalAlignment.Center;
            txtblk.VerticalAlignment = VerticalAlignment.Center;
            bord.Child = txtblk;
        }
        // Public property to set text.
        public string Text
        {
            set { txtblk.Text = value; }
            get { return txtblk.Text; }
        }
    }
}
