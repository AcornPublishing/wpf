//------------------------------------------------------
// YellowPadWindow.Tools.cs (c) 2006 by Charles Petzold
//------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.YellowPad
{
    public partial class YellowPadWindow : Window
    {
        // Display StylusToolDialog and use DrawingAttributes property.
        void StylusToolOnClick(object sender, RoutedEventArgs args)
        {
            StylusToolDialog dlg = new StylusToolDialog();
            dlg.Owner = this;
            dlg.DrawingAttributes = inkcanv.DefaultDrawingAttributes;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                inkcanv.DefaultDrawingAttributes = dlg.DrawingAttributes;
            }
        }
        // Display EraserToolDialog and use EraserShape property.
        void EraserToolOnClick(object sender, RoutedEventArgs args)
        {
            EraserToolDialog dlg = new EraserToolDialog();
            dlg.Owner = this;
            dlg.EraserShape = inkcanv.EraserShape;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                inkcanv.EraserShape = dlg.EraserShape;
            }
        }
    }
}
