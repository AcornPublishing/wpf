//----------------------------------------------------------------
// RecurseDirectoriesIncrementally.cs (c) 2006 by Charles Petzold
//----------------------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.RecurseDirectoriesIncrementally
{
    class RecurseDirectoriesIncrementally : Window
    {
        StackPanel stack;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecurseDirectoriesIncrementally());
        }
        public RecurseDirectoriesIncrementally()
        {
            Title = "Recurse Directories Incrementally";

            // Create Grid as content of window.
            Grid grid = new Grid();
            Content = grid;

            // Define ColumnDefinition objects.
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            // Put DirectoryTreeView at left.
            DirectoryTreeView tree = new DirectoryTreeView();
            tree.SelectedItemChanged += TreeViewOnSelectedItemChanged;
            grid.Children.Add(tree);
            Grid.SetColumn(tree, 0);

            // Put GridSplitter in center.
            GridSplitter split = new GridSplitter();
            split.Width = 6;
            split.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            // Put scrolled StackPanel at right.
            ScrollViewer scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, 2);

            stack = new StackPanel();
            scroll.Content = stack;
        }
        void TreeViewOnSelectedItemChanged(object sender, 
                        RoutedPropertyChangedEventArgs<object> args)
        {
            // Get selected item.
            DirectoryTreeViewItem item = args.NewValue as DirectoryTreeViewItem;

            // Clear out the DockPanel.
            stack.Children.Clear();

            // Fill it up again.
            FileInfo[] infos;

            try
            {
                infos = item.DirectoryInfo.GetFiles();
            }
            catch
            {
                return;
            }

            foreach (FileInfo info in infos)
            {
                TextBlock text = new TextBlock();
                text.Text = info.Name;
                stack.Children.Add(text);
            }
        }
    }
}
