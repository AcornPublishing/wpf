//-----------------------------------------------------------
// EnlargeButtonWithAnimation.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Petzold.EnlargeButtonWithAnimation
{
    public class EnlargeButtonWithAnimation : Window
    {
        const double initFontSize = 12;
        const double maxFontSize = 48;
        Button btn;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EnlargeButtonWithAnimation());
        }
        public EnlargeButtonWithAnimation()
        {
            Title = "Enlarge Button with Animation";

            btn = new Button();
            btn.Content = "Expanding Button";
            btn.FontSize = initFontSize;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            DoubleAnimation anima = new DoubleAnimation();
            anima.Duration = new Duration(TimeSpan.FromSeconds(2));
            anima.From = initFontSize;
            anima.To = maxFontSize;
            anima.FillBehavior = FillBehavior.Stop;

            btn.BeginAnimation(Button.FontSizeProperty, anima);
        }
    }
}
