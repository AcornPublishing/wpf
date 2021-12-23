//----------------------------------------------
// GenericWindow.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using Petzold.FlowDocumentHelp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.Generic
{
    public partial class GenericWindow : Window
    {
        public GenericWindow()
        {
            InitializeComponent();
        }
        void HelpOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            HelpWindow win = new HelpWindow();
            win.Owner = this;
            win.Title = "Help for Generic";
            win.Show();
        }
    }
}
