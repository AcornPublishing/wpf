//-----------------------------------------------
// SplitTheClient.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

class SplitTheClient : Window
{
    [STAThread]
    public static void Main()
    {
        Application app = new Application();
        app.Run(new SplitTheClient());
    }
    public SplitTheClient()
    {
        Title = "Split the Client";

        // Grid with vertical splitter.
        Grid grid1 = new Grid();
        grid1.ColumnDefinitions.Add(new ColumnDefinition());
        grid1.ColumnDefinitions.Add(new ColumnDefinition());
        grid1.ColumnDefinitions.Add(new ColumnDefinition());
        grid1.ColumnDefinitions[1].Width = GridLength.Auto;
        Content = grid1;

        // Button at left of vertical splitter.
        Button btn = new Button();
        btn.Content = "Button No. 1";
        grid1.Children.Add(btn);
        Grid.SetRow(btn, 0);
        Grid.SetColumn(btn, 0);

        // Vertical splitter.
        GridSplitter split = new GridSplitter();
        split.ShowsPreview = true;
        split.HorizontalAlignment = HorizontalAlignment.Center;
        split.VerticalAlignment = VerticalAlignment.Stretch;
        split.Width = 6;
        grid1.Children.Add(split);
        Grid.SetRow(split, 0);
        Grid.SetColumn(split, 1);

        // Grid with horizontal splitter.
        Grid grid2 = new Grid();
        grid2.RowDefinitions.Add(new RowDefinition());
        grid2.RowDefinitions.Add(new RowDefinition());
        grid2.RowDefinitions.Add(new RowDefinition());
        grid2.RowDefinitions[1].Height = GridLength.Auto;
        grid1.Children.Add(grid2);
        Grid.SetRow(grid2, 0);
        Grid.SetColumn(grid2, 2);

        // Button at top of horizontal splitter.
        btn = new Button();
        btn.Content = "Button No. 2";
        grid2.Children.Add(btn);
        Grid.SetRow(btn, 0);
        Grid.SetColumn(btn, 0);

        // Horizontal splitter.
        split = new GridSplitter();
        split.ShowsPreview = true;
        split.HorizontalAlignment = HorizontalAlignment.Stretch;
        split.VerticalAlignment = VerticalAlignment.Center;
        split.Height = 6;
        grid2.Children.Add(split);
        Grid.SetRow(split, 1);
        Grid.SetColumn(split, 0);

        // Bottom at bottom of horizontal splitter.
        btn = new Button();
        btn.Content = "Button No. 3";
        grid2.Children.Add(btn);
        Grid.SetRow(btn, 2);
        Grid.SetColumn(btn, 0);
    }
}
