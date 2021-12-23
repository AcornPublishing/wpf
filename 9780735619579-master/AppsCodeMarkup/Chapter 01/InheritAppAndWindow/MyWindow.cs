//-----------------------------------------
// MyWindow.cs (c) 2006 by Charles Petzold
//-----------------------------------------
using System;
using System.Windows;
using System.Windows.Input;

namespace Petzold.InheritAppAndWindow
{
    public class MyWindow : Window
    {
        public MyWindow()
        {
            Title = "Inherit App & Window";
        }
        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            base.OnMouseDown(args);

            string strMessage =
                string.Format("Window clicked with {0} button at point ({1})",
                              args.ChangedButton, args.GetPosition(this));
            MessageBox.Show(strMessage, Title);
        }
    }
}