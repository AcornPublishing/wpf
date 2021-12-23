//---------------------------------------------
// PrintEllipse.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PrintEllipse
{
    public class PrintEllipse : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintEllipse());
        }
        public PrintEllipse()
        {
            Title = "Print Ellipse";
            FontSize = 24;

            // Create StackPanel as content of Window.
            StackPanel stack = new StackPanel();
            Content = stack;

            // Create Button for printing.
            Button btn = new Button();
            btn.Content = "_Print...";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.Margin = new Thickness(24);
            btn.Click += PrintOnClick;
            stack.Children.Add(btn);
        }
        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                // Create DrawingVisual and open DrawingContext.
                DrawingVisual vis = new DrawingVisual();
                DrawingContext dc = vis.RenderOpen();

                // Draw ellipse.
                dc.DrawEllipse(Brushes.LightGray, new Pen(Brushes.Black, 3),
                               new Point(dlg.PrintableAreaWidth / 2,
                                         dlg.PrintableAreaHeight / 2),
                               dlg.PrintableAreaWidth / 2, 
                               dlg.PrintableAreaHeight / 2);

                // Close DrawingContext.
                dc.Close();

                // Finally, print the page.
                dlg.PrintVisual(vis, "My first print job");
            }
        }            
    }
}