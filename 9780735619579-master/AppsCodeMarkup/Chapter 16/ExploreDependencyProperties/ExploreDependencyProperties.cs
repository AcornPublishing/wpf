//------------------------------------------------------------
// ExploreDependencyProperties.cs (c) 2006 by Charles Petzold
//------------------------------------------------------------
using Petzold.ShowClassHierarchy;       // for ClassHierarchyTreeView
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ExploreDependencyProperties
{
    public class ExploreDependencyProperties : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ExploreDependencyProperties());
        }
        public ExploreDependencyProperties()
        {
            Title = "Explore Dependency Properties";

            // Create Grid as content of window.
            Grid grid = new Grid();
            Content = grid;

            // Three column definitions for Grid.
            ColumnDefinition col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            col = new ColumnDefinition();
            col.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(col);

            col = new ColumnDefinition();
            col.Width = new GridLength(3, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            // ClassHierarchyTreeView goes on left side.
            ClassHierarchyTreeView treevue = 
                            new ClassHierarchyTreeView(typeof(DependencyObject));
            grid.Children.Add(treevue);
            Grid.SetColumn(treevue, 0);

            // GridSplitter goes in the center cell.
            GridSplitter split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            // DependencyPropertyListView goes on right side.
            DependencyPropertyListView lstvue = new DependencyPropertyListView();
            grid.Children.Add(lstvue);
            Grid.SetColumn(lstvue, 2);

            // Set a binding between TreeView and ListView.
            lstvue.SetBinding(DependencyPropertyListView.TypeProperty, 
                              "SelectedItem.Type");
            lstvue.DataContext = treevue;
        }
    }
}