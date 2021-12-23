//-----------------------------------------
// MyWindow.cs (c) 2006 by Charles Petzold
//-----------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.IncludeApplicationDefinition
{
    public partial class MyWindow : Window
    {
        public MyWindow()
        {
            InitializeComponent();
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = sender as Button;
            MessageBox.Show("The button labled '" + btn.Content +
                            "' has been clicked.");
        }
    }
}