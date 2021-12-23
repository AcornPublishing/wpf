//----------------------------------------------
// YellowPadHelp.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.YellowPad
{
    public partial class YellowPadHelp
    {
        public YellowPadHelp()
        {
            InitializeComponent();

            // Select first item in TreeView and give it the focus.
            (tree.Items[0] as TreeViewItem).IsSelected = true;
            tree.Focus();
        }
        void HelpOnSelectedItemChanged(object sender, 
                                RoutedPropertyChangedEventArgs<object> args)
        {
            TreeViewItem item = args.NewValue as TreeViewItem;

            if (item.Tag == null)
                return;

            // Navigate to the selected item's Tag property.
            frame.Navigate(new Uri(item.Tag as string, UriKind.Relative));
        }
    }
}
