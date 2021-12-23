//-------------------------------------------
// ColorWheel.cs (c) 2006 by Charles Petzold
//-------------------------------------------
using Petzold.CircleTheButtons;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.SelectColorFromWheel
{
    class ColorWheel : ListBox
    {
        public ColorWheel()
        {
            // Define the ItemsPanel template.
            FrameworkElementFactory factoryRadialPanel = 
                            new FrameworkElementFactory(typeof(RadialPanel));
            ItemsPanel = new ItemsPanelTemplate(factoryRadialPanel);

            // Create the DataTemplate for the items.
            DataTemplate template = new DataTemplate(typeof(Brush));
            ItemTemplate = template;

            // Create a FrameworkElementFactory based on Rectangle.
            FrameworkElementFactory elRectangle = 
                                new FrameworkElementFactory(typeof(Rectangle));
            elRectangle.SetValue(Rectangle.WidthProperty, 4.0);
            elRectangle.SetValue(Rectangle.HeightProperty, 12.0);
            elRectangle.SetValue(Rectangle.MarginProperty, 
                                        new Thickness(1, 8, 1, 8));
            elRectangle.SetBinding(Rectangle.FillProperty, new Binding(""));

            // Use that factory for the visual tree.
            template.VisualTree = elRectangle;

            // Set the items in the ListBox.
            PropertyInfo[] props = typeof(Brushes).GetProperties();

            foreach (PropertyInfo prop in props)
                Items.Add((Brush)prop.GetValue(null, null));
        }
    }
}


