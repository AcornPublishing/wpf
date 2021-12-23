//------------------------------------------------
// ListNamedColors.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ListNamedColors
{
    class ListNamedColors : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListNamedColors());
        }
        public ListNamedColors()
        {
            Title = "List Named Colors";

            // Create ListBox as content of window.
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // Set the items and the property paths.
            lstbox.ItemsSource = NamedColor.All;
            lstbox.DisplayMemberPath = "Name";
            lstbox.SelectedValuePath = "Color";
        }
        void ListBoxOnSelectionChanged(object sender, 
                                       SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;

            if (lstbox.SelectedValue != null)
            {
                Color clr = (Color)lstbox.SelectedValue;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
