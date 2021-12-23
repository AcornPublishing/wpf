//----------------------------------------------------
// FormatRichText.Para.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.FormatRichText
{
    public partial class FormatRichText : Window
    {
        ToggleButton[] btnAlignment = new ToggleButton[4];

        void AddParaToolBar(ToolBarTray tray, int band, int index)
        {
            // Create ToolBar and add to tray.
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            // Create ToolBar items.
            toolbar.Items.Add(btnAlignment[0] =
                CreateButton(TextAlignment.Left, "Align Left", 0, 4));
            toolbar.Items.Add(btnAlignment[1] =
                CreateButton(TextAlignment.Center, "Center", 2, 2));
            toolbar.Items.Add(btnAlignment[2] =
                CreateButton(TextAlignment.Right, "Align Right", 4, 0));
            toolbar.Items.Add(btnAlignment[3] =
                CreateButton(TextAlignment.Justify, "Justify", 0, 0));

            // Attach another event handler for SelectionChanged.
            txtbox.SelectionChanged += TextBoxOnSelectionChanged2;
        }
        ToggleButton CreateButton(TextAlignment align, string strToolTip,
                                  int offsetLeft, int offsetRight)
        {
            // Create ToggleButton.
            ToggleButton btn = new ToggleButton();
            btn.Tag = align;
            btn.Click += ButtonOnClick;

            // Set Content as Canvas.
            Canvas canv = new Canvas();
            canv.Width = 16;
            canv.Height = 16;
            btn.Content = canv;

            // Draw lines on the Canvas.
            for (int i = 0; i < 5; i++)
            {
                Polyline poly = new Polyline();
                poly.Stroke = SystemColors.WindowTextBrush;
                poly.StrokeThickness = 1;

                if ((i & 1) == 0)
                    poly.Points = new PointCollection(new Point[] 
                        { 
                            new Point(2, 2 + 3 * i), new Point(14, 2 + 3 * i)
                        });
                else
                    poly.Points = new PointCollection(new Point[]
                        {
                            new Point(2 + offsetLeft, 2 + 3 * i),
                            new Point(14 - offsetRight, 2 + 3 * i)
                        });

                canv.Children.Add(poly);
            }

            // Create a ToolTip.
            ToolTip tip = new ToolTip();
            tip.Content = strToolTip;
            btn.ToolTip = tip;

            return btn;
        }
        // Handler for TextBox SelectionChanged event.
        void TextBoxOnSelectionChanged2(object sender, RoutedEventArgs args)
        {
            // Obtain the current TextAlignment.
            object obj = txtbox.Selection.GetPropertyValue(
                                        Paragraph.TextAlignmentProperty);
            // Set the buttons.
            if (obj != null && obj is TextAlignment)
            {
                TextAlignment align = (TextAlignment)obj;

                foreach (ToggleButton btn in btnAlignment)
                    btn.IsChecked = (align == (TextAlignment)btn.Tag);
            }
            else
            {
                foreach (ToggleButton btn in btnAlignment)
                    btn.IsChecked = false;
            }
        }
        // Handler for Button Click event.
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = args.Source as ToggleButton;

            foreach (ToggleButton btnAlign in btnAlignment)
                btnAlign.IsChecked = (btn == btnAlign);
            
            // Set the new TextAlignment.
            TextAlignment align = (TextAlignment) btn.Tag;
            txtbox.Selection.ApplyPropertyValue(Paragraph.TextAlignmentProperty, 
                                                align);
        }
    }
}
