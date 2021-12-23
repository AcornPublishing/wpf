//----------------------------------------------------------
// ListColorsEvenElegantlier.cs (c) 2006 by Charles Petzold
//----------------------------------------------------------
using Petzold.ListNamedBrushes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.ListColorsEvenElegantlier
{
    public class ListColorsEvenElegantlier : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsEvenElegantlier());
        }
        public ListColorsEvenElegantlier()
        {
            Title = "List Colors Even Elegantlier";

            // Create a DataTemplate for the items.
            DataTemplate template = new DataTemplate(typeof(NamedBrush));

            // Create a FrameworkElementFactory based on StackPanel.
            FrameworkElementFactory factoryStack =
                                new FrameworkElementFactory(typeof(StackPanel));
            factoryStack.SetValue(StackPanel.OrientationProperty, 
                                                Orientation.Horizontal);

            // Make that the root of the DataTemplate visual tree.
            template.VisualTree = factoryStack;

            // Create a FrameworkElementFactory based on Rectangle.
            FrameworkElementFactory factoryRectangle =
                                new FrameworkElementFactory(typeof(Rectangle));
            factoryRectangle.SetValue(Rectangle.WidthProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.HeightProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.MarginProperty, new Thickness(2));
            factoryRectangle.SetValue(Rectangle.StrokeProperty, 
                                            SystemColors.WindowTextBrush);
            factoryRectangle.SetBinding(Rectangle.FillProperty, 
                                            new Binding("Brush"));
            // Add it to the StackPanel.
            factoryStack.AppendChild(factoryRectangle);

            // Create a FrameworkElementFactory based on TextBlock.
            FrameworkElementFactory factoryTextBlock =
                                new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.VerticalAlignmentProperty,
                                            VerticalAlignment.Center);
            factoryTextBlock.SetValue(TextBlock.TextProperty, 
                                            new Binding("Name"));
            // Add it to the StackPanel.
            factoryStack.AppendChild(factoryTextBlock);

            // Create ListBox as content of window.
            ListBox lstbox = new ListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            Content = lstbox;

            // Set the ItemTemplate property to the template created above.
            lstbox.ItemTemplate = template;

            // Set the ItemsSource to the array of NamedBrush objects.
            lstbox.ItemsSource = NamedBrush.All;

            // Bind the SelectedValue to window Background.
            lstbox.SelectedValuePath = "Brush";
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;
        }
    }
}
