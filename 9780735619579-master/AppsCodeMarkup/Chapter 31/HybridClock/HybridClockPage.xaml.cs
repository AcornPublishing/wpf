//-----------------------------------------------------
// HybridClockPage.xaml.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Petzold.HybridClock
{
    public partial class HybridClockPage : Page
    {
        // Initialize colors for XAML access.
        public static readonly Color clrBackground = Colors.Aqua;
        public static readonly Brush brushBackground = Brushes.Aqua;

        // Save transforms for clock ticks.
        TranslateTransform[] xform = new TranslateTransform[60];

        public HybridClockPage()
        {
            InitializeComponent();

            // Set the Storyboard BeginTime property.
            storyboard.BeginTime = -DateTime.Now.TimeOfDay;

            // Set handler for context menu Opened event.
            ContextMenu menu = new ContextMenu();
            menu.Opened += ContextMenuOnOpened;
            ContextMenu = menu;
            
            // Set handler for Loaded event.
            Loaded += WindowOnLoaded;
        }

        void WindowOnLoaded(object sender, EventArgs args)
        {
            // Create tick marks around clock.
            for (int i = 0; i < 60; i++)
            {
                Ellipse elips = new Ellipse();
                elips.HorizontalAlignment = HorizontalAlignment.Center;
                elips.VerticalAlignment = VerticalAlignment.Center;
                elips.Fill = Brushes.Blue;
                elips.Width = 
                elips.Height = i % 5 == 0 ? 6 : 2;

                TransformGroup group = new TransformGroup();
                group.Children.Add(xform[i] = 
                    new TranslateTransform(datetime.ActualWidth, 0));
                group.Children.Add(
                    new TranslateTransform(grd.Margin.Left / 2, 0));
                group.Children.Add(
                    new TranslateTransform(-elips.Width / 2, -elips.Height /2));
                group.Children.Add(
                    new RotateTransform(i * 6));
                group.Children.Add(
                    new TranslateTransform(elips.Width / 2, elips.Height /2));

                elips.RenderTransform = group;
                grd.Children.Add(elips);
            }
            // Make the opacity mask.
            MakeMask();

            // Set event handler for change in size of the date/time string.
            datetime.SizeChanged += DateTimeOnSizeChanged;
        }
        // Date/time string changes: Recalculate transforms and mask.
        void DateTimeOnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            if (args.WidthChanged)
            {
                for (int i = 0; i < 60; i++)
                    xform[i].X = datetime.ActualWidth;

               MakeMask();
            }
        }
        // Make the opacity mask.
        void MakeMask()
        {
            DrawingGroup group = new DrawingGroup();
            Point ptCenter = new Point(datetime.ActualWidth + grd.Margin.Left, 
                                       datetime.ActualWidth + grd.Margin.Left);

            // Calculate 256 wedges around circle.
            for (int i = 0; i < 256; i++)
            {
                Point ptInner1 = 
                    new Point(ptCenter.X + datetime.ActualWidth * 
                                    Math.Cos(i * 2 * Math.PI / 256),
                              ptCenter.Y + datetime.ActualWidth * 
                                    Math.Sin(i * 2 * Math.PI / 256));

                Point ptInner2 = 
                    new Point(ptCenter.X + datetime.ActualWidth * 
                                    Math.Cos((i + 2) * 2 * Math.PI / 256),
                              ptCenter.Y + datetime.ActualWidth * 
                                    Math.Sin((i + 2) * 2 * Math.PI / 256));

                Point ptOuter1 = 
                    new Point(ptCenter.X + 
                                (datetime.ActualWidth + grd.Margin.Left) * 
                                    Math.Cos(i * 2 * Math.PI / 256),
                              ptCenter.Y + 
                                (datetime.ActualWidth + grd.Margin.Left) * 
                                    Math.Sin(i * 2 * Math.PI / 256));

                Point ptOuter2 = 
                    new Point(ptCenter.X + 
                                (datetime.ActualWidth + grd.Margin.Left) * 
                                    Math.Cos((i + 2) * 2 * Math.PI / 256),
                              ptCenter.Y + 
                                (datetime.ActualWidth + grd.Margin.Left) * 
                                    Math.Sin((i + 2) * 2 * Math.PI / 256));

                PathSegmentCollection segcoll = new PathSegmentCollection();
                segcoll.Add(new LineSegment(ptInner2, false));
                segcoll.Add(new LineSegment(ptOuter2, false));
                segcoll.Add(new LineSegment(ptOuter1, false));
                segcoll.Add(new LineSegment(ptInner1, false));

                PathFigure fig = new PathFigure(ptInner1, segcoll, true);

                PathFigureCollection figcoll = new PathFigureCollection();
                figcoll.Add(fig);

                PathGeometry path = new PathGeometry(figcoll);
                byte byOpacity = (byte)Math.Min(255, 512 - 2 * i);

                SolidColorBrush br = new SolidColorBrush(
                    Color.FromArgb(byOpacity, clrBackground.R, 
                                   clrBackground.G, clrBackground.B));

                GeometryDrawing draw = 
                    new GeometryDrawing(br, new Pen(br, 2), path);
                group.Children.Add(draw);
            }

            DrawingBrush brush = new DrawingBrush(group);
            mask.Fill = brush;
        }

        // Initialize context menu for date/time format.
        void ContextMenuOnOpened(object sender, RoutedEventArgs args)
        {
            ContextMenu menu = sender as ContextMenu;
            menu.Items.Clear();

            string[] strFormats = { "d", "D", "f", "F", "g", "G", "M", 
                                    "R", "s", "t", "T", "u", "U", "Y" };

            foreach (string strFormat in strFormats)
            {
                MenuItem item = new MenuItem();
                item.Header = DateTime.Now.ToString(strFormat);
                item.Tag = strFormat;
                item.IsChecked = strFormat == 
                    (Resources["clock"] as ClockTicker).Format;
                item.Click += MenuItemOnClick;
                menu.Items.Add(item);
            }
        }
        // Process clicked item on context menu.
        void MenuItemOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = (sender as MenuItem);
            (Resources["clock"] as ClockTicker).Format = item.Tag as string;
        }
    }
}