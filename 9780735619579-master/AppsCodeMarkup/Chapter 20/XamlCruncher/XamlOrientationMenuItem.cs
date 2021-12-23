//--------------------------------------------------------
// XamlOrientationMenuItem.cs (c) 2006 by Charles Petzold
//--------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.XamlCruncher
{
    class XamlOrientationMenuItem : MenuItem
    {
        MenuItem itemChecked;
        Grid grid;
        TextBox txtbox;
        Frame frame;

        // Orientation public property of type Dock. 
        public Dock Orientation
        {
            set
            {
                foreach (MenuItem item in Items)
                    if (item.IsChecked = (value == (Dock)item.Tag))
                        itemChecked = item;
            }
            get
            {
                return (Dock)itemChecked.Tag;
            }
        }

        // Constructor requires three arguments.
        public XamlOrientationMenuItem(Grid grid, TextBox txtbox, Frame frame)
        {
            this.grid = grid;
            this.txtbox = txtbox;
            this.frame = frame;

            Header = "_Orientation";

            for (int i = 0; i < 4; i++)
                Items.Add(CreateItem((Dock)i));

            (itemChecked = (MenuItem) Items[0]).IsChecked = true;
        }
        // Create each menu item based on Dock setting.
        MenuItem CreateItem(Dock dock)
        {
            MenuItem item = new MenuItem();
            item.Tag = dock;
            item.Click += ItemOnClick;
            item.Checked += ItemOnCheck;

            // Two text strings that appear in menu item.
            FormattedText formtxt1 = CreateFormattedText("Edit");
            FormattedText formtxt2 = CreateFormattedText("Display");
            double widthMax = Math.Max(formtxt1.Width, formtxt2.Width);

            // Create a DrawingVisual and a DrawingContext.
            DrawingVisual vis = new DrawingVisual();
            DrawingContext dc = vis.RenderOpen();

            // Draw boxed text on the visual.
            switch (dock)
            {
                case Dock.Left:         // Edit on left, display on right.
                    BoxText(dc, formtxt1, formtxt1.Width, new Point(0, 0));
                    BoxText(dc, formtxt2, formtxt2.Width, 
                            new Point(formtxt1.Width + 4, 0));
                    break;

                case Dock.Top:          // Edit on top, display on bottom.
                    BoxText(dc, formtxt1, widthMax, new Point(0, 0));
                    BoxText(dc, formtxt2, widthMax, 
                            new Point(0, formtxt1.Height + 4));
                    break;

                case Dock.Right:        // Edit on right, display on left.
                    BoxText(dc, formtxt2, formtxt2.Width, new Point(0, 0));
                    BoxText(dc, formtxt1, formtxt1.Width, 
                            new Point(formtxt2.Width + 4, 0));
                    break;

                case Dock.Bottom:       // Edit on bottom, display on top.
                    BoxText(dc, formtxt2, widthMax, new Point(0, 0));
                    BoxText(dc, formtxt1, widthMax, 
                            new Point(0, formtxt2.Height + 4));
                    break;
            }

            dc.Close();

            // Create Image object based on Drawing from visual.
            DrawingImage drawimg = new DrawingImage(vis.Drawing);
            Image img = new Image();
            img.Source = drawimg;

            // Set the Header of the menu item to the Image object.
            item.Header = img;

            return item;
        }
        // Handles the hairy FormattedText arguments.
        FormattedText CreateFormattedText(string str)
        {
            return new FormattedText(str, CultureInfo.CurrentCulture, 
                FlowDirection.LeftToRight, 
                new Typeface(SystemFonts.MenuFontFamily, SystemFonts.MenuFontStyle, 
                             SystemFonts.MenuFontWeight, FontStretches.Normal),
                SystemFonts.MenuFontSize, SystemColors.MenuTextBrush);
        }
        // Draws text surrounded by a rectangle.
        void BoxText(DrawingContext dc, FormattedText formtxt, 
                                        double width, Point pt)
        {
            Pen pen = new Pen(SystemColors.MenuTextBrush, 1);

            dc.DrawRectangle(null, pen, 
                new Rect(pt.X, pt.Y, width + 4, formtxt.Height + 4));
            double X = pt.X + (width - formtxt.Width) / 2;
            dc.DrawText(formtxt, new Point(X + 2, pt.Y + 2));
        }
        // Check and uncheck items when clicked.
        void ItemOnClick(object sender, RoutedEventArgs args)
        {
            itemChecked.IsChecked = false;
            itemChecked = args.Source as MenuItem;
            itemChecked.IsChecked = true;
        }
        // Change the orientation based on the checked item.
        void ItemOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem itemChecked = args.Source as MenuItem;

            // Initialize the 2nd and 3rd rows and columns to zero.
            for (int i = 1; i < 3; i++)
            {
                grid.RowDefinitions[i].Height = new GridLength(0);
                grid.ColumnDefinitions[i].Width = new GridLength(0);
            }

            // Initialize the cell of the TextBox and Frame to zero.
            Grid.SetRow(txtbox, 0);
            Grid.SetColumn(txtbox, 0);
            Grid.SetRow(frame, 0);
            Grid.SetColumn(frame, 0);

            // Set row and columns based on the orientation setting.
            switch ((Dock)itemChecked.Tag)
            {
                case Dock.Left:             // Edit on left, display on right.
                    grid.ColumnDefinitions[1].Width = GridLength.Auto;
                    grid.ColumnDefinitions[2].Width = 
                                new GridLength(100, GridUnitType.Star);
                    Grid.SetColumn(frame, 2);
                    break;

                case Dock.Top:              // Edit on top, display on bottom.
                    grid.RowDefinitions[1].Height = GridLength.Auto;
                    grid.RowDefinitions[2].Height = 
                                new GridLength(100, GridUnitType.Star);
                    Grid.SetRow(frame, 2);
                    break;

                case Dock.Right:            // Edit on right, display on left.
                    grid.ColumnDefinitions[1].Width = GridLength.Auto;
                    grid.ColumnDefinitions[2].Width = 
                                new GridLength(100, GridUnitType.Star);
                    Grid.SetColumn(txtbox, 2);
                    break;

                case Dock.Bottom:           // Edit on bottom, display on top.
                    grid.RowDefinitions[1].Height = GridLength.Auto;
                    grid.RowDefinitions[2].Height = 
                                new GridLength(100, GridUnitType.Star);
                    Grid.SetRow(txtbox, 2);
                    break;
            }
        }
    }
}
