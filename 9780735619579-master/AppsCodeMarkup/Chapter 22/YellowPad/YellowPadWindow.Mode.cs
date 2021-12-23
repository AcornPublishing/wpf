//-----------------------------------------------------
// YellowPadWindow.Mode.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.YellowPad
{
    public partial class YellowPadWindow : Window
    {
        // Stylus-Mode submenu opened: check one of the items.
        void StylusModeOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;

            foreach (MenuItem child in item.Items)
                child.IsChecked = inkcanv.EditingMode ==
                                    (InkCanvasEditingMode)child.Tag;
        }
        // Set the EditingMode property from the selected item.
        void StylusModeOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            inkcanv.EditingMode = (InkCanvasEditingMode)item.Tag;
        }
        // Eraser-Mode submenu opened: check one of the items.
        void EraserModeOnOpened(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;

            foreach (MenuItem child in item.Items)
                child.IsChecked = inkcanv.EditingModeInverted ==
                                    (InkCanvasEditingMode)child.Tag;
        }
        // Set the EditingModeInverted property from the selected item.
        void EraserModeOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            inkcanv.EditingModeInverted = (InkCanvasEditingMode)item.Tag;
        }
    }
}
