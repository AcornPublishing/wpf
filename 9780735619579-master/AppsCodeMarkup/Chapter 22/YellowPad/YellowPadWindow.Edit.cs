//-----------------------------------------------------
// YellowPadWindow.Edit.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;

namespace Petzold.YellowPad
{
    public partial class YellowPadWindow : Window
    {
        // Enable Format item if strokes have been selected.
        void EditOnOpened(object sender, RoutedEventArgs args)
        {
            itemFormat.IsEnabled = inkcanv.GetSelectedStrokes().Count > 0;
        }
        // Enable Cut, Copy, Delete items if strokes have been selected.
        void CutCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = inkcanv.GetSelectedStrokes().Count > 0;
        }
        // Implement Cut and Copy with methods in InkCanvas.
        void CutOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            inkcanv.CutSelection();
        }
        void CopyOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            inkcanv.CopySelection();
        }
        // Enable Paste item if the InkCanvas is cool with the clipboard.

        void PasteCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = inkcanv.CanPaste();
        }
        // Implement Paste with method in InkCanvas.
        void PasteOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            inkcanv.Paste();
        }
        // Implement Delete "manually."
        void DeleteOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (Stroke strk in inkcanv.GetSelectedStrokes())
                inkcanv.Strokes.Remove(strk);
        }
        // Select All item: select all the strokes.
        void SelectAllOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            inkcanv.Select(inkcanv.Strokes);
        }
        // Format Selection item: invoke StylusToolDialog.
        void FormatOnClick(object sender, RoutedEventArgs args)
        {
            StylusToolDialog dlg = new StylusToolDialog();
            dlg.Owner = this;
            dlg.Title = "Format Selection";

            // Try getting the DrawingAttributes of the first selected stroke.
            StrokeCollection strokes = inkcanv.GetSelectedStrokes();

            if (strokes.Count > 0)
                dlg.DrawingAttributes = strokes[0].DrawingAttributes;
            else
                dlg.DrawingAttributes = inkcanv.DefaultDrawingAttributes;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                // Set the DrawingAttributes of all the selected strokes.
                foreach (Stroke strk in strokes)
                    strk.DrawingAttributes = dlg.DrawingAttributes;
            }
        }
    }
}
