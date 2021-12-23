//-----------------------------------------------------
// SelectColorFromWheel.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.SelectColorFromWheel
{
    public class SelectColorFromWheel : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromWheel());
        }
        public SelectColorFromWheel()
        {
            Title = "Select Color from Wheel";
            SizeToContent = SizeToContent.WidthAndHeight;

            // Create StackPanel as content of window.
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // Create do-nothing button to test tabbing.
            Button btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            // Create ColorWheel control.
            ColorWheel clrwheel = new ColorWheel();
            clrwheel.Margin = new Thickness(24);
            clrwheel.HorizontalAlignment = HorizontalAlignment.Center;
            clrwheel.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrwheel);

            // Bind Background of window to selected value of ColorWheel.
            clrwheel.SetBinding(ColorWheel.SelectedValueProperty, "Background");
            clrwheel.DataContext = this;

            // Create another do-nothing button.
            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
