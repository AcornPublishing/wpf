//--------------------------------------------------
// DesignAButton.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Petzold.DesignAButton
{
    public class DesignAButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DesignAButton());
        }
        public DesignAButton()
        {
            Title = "Design a Button";

            // Create a Button as content of the Window.
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;

            // Create a StackPanel as content of the Button.
            StackPanel stack = new StackPanel();
            btn.Content = stack;

            // Add a Polyline to the StackPanel.
            stack.Children.Add(ZigZag(10));

            // Add an Image to the StackPanel.
            Uri uri = new Uri("pack://application:,,/BOOK06.ICO");  // 32-pixels
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Margin = new Thickness(0, 10, 0, 0);
            img.Source = bitmap;
            img.Stretch = Stretch.None;
            stack.Children.Add(img);

            // Add a Label to the StackPanel.
            Label lbl = new Label();
            lbl.Content = "_Read books!";
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            stack.Children.Add(lbl);

            // Add another Polyline to the StackPanel.
            stack.Children.Add(ZigZag(0));
        }
        Polyline ZigZag(int offset)
        {
            Polyline poly = new Polyline();
            poly.Stroke = SystemColors.ControlTextBrush;
            poly.Points = new PointCollection();

            for (int x = 0; x <= 100; x += 10)
                poly.Points.Add(new Point(x, (x + offset) % 20));

            return poly;
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The button has been clicked", Title);
        }
    }
}
