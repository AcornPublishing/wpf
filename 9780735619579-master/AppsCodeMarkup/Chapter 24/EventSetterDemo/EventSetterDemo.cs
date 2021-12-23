//------------------------------------------------
// EventSetterDemo.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.EventSetterDemo
{
    public partial class EventSetterDemo : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EventSetterDemo());
        }
        public EventSetterDemo()
        {
            InitializeComponent();
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            MessageBox.Show("The button labeled " + btn.Content +
                            " has been clicked", Title);
        }
    }
}
