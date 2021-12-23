//------------------------------------------
// XamlClock.cs (c) 2006 by Charles Petzold
//------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.XamlClock
{
    public partial class XamlClock : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new XamlClock());
        }
        public XamlClock()
        {
            InitializeComponent();

            // Initialize Storyboard to display current time.
            storyboard.BeginTime = -DateTime.Now.TimeOfDay;
        }
    }
}