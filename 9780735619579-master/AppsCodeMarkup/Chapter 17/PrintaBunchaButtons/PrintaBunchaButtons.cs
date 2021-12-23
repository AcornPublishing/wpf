//----------------------------------------------------
// PrintaBunchaButtons.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PrintaBunchaButtons
{
    public class PrintaBunchaButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintaBunchaButtons());
        }
        public PrintaBunchaButtons()
        {
            Title = "Print a Bunch of Buttons";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;

            // Create 'Print' button.
            Button btn = new Button();
            btn.FontSize = 24;
            btn.Content = "Print ...";
            btn.Padding = new Thickness(12);
            btn.Margin = new Thickness(96);
            btn.Click += PrintOnClick;
            Content = btn;
        }
        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                // Create Grid panel.
                Grid grid = new Grid();

                // Define five auto-sized rows and columns.
                for (int i = 0; i < 5; i++)
                {
                    ColumnDefinition coldef = new ColumnDefinition();
                    coldef.Width = GridLength.Auto;
                    grid.ColumnDefinitions.Add(coldef);

                    RowDefinition rowdef = new RowDefinition();
                    rowdef.Height = GridLength.Auto;
                    grid.RowDefinitions.Add(rowdef);
                }

                // Give the Grid a gradient brush.
                grid.Background = 
                    new LinearGradientBrush(Colors.Gray, Colors.White,
                                            new Point(0, 0), new Point(1, 1));

                // Every program needs a bit of randomness.
                Random rand = new Random();

                // Fill the Grid with 25 buttons.
                for (int i = 0; i < 25; i++)
                {
                    Button btn = new Button();
                    btn.FontSize = 12 + rand.Next(8);
                    btn.Content = "Button No. " + (i + 1);
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    btn.VerticalAlignment = VerticalAlignment.Center;
                    btn.Margin = new Thickness(6);
                    grid.Children.Add(btn);
                    Grid.SetRow(btn, i % 5);
                    Grid.SetColumn(btn, i / 5);
                }

                // Size the Grid.
                grid.Measure(new Size(Double.PositiveInfinity,
                                      Double.PositiveInfinity));

                Size sizeGrid = grid.DesiredSize;

                // Determine point for centering Grid on page.
                Point ptGrid =
                    new Point((dlg.PrintableAreaWidth - sizeGrid.Width) / 2,
                              (dlg.PrintableAreaHeight - sizeGrid.Height) / 2);

                // Layout pass.
                grid.Arrange(new Rect(ptGrid, sizeGrid));

                // Now print it.
                dlg.PrintVisual(grid, Title);
            }
        }
    }
}