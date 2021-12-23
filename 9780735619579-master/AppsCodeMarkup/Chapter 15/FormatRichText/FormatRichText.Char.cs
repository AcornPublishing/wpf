//----------------------------------------------------
// FormatRichText.Char.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using Petzold.SelectColorFromGrid;      // for ColorGridBox
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    public partial class FormatRichText : Window
    {
        ComboBox comboFamily, comboSize;
        ToggleButton btnBold, btnItalic;
        ColorGridBox clrboxBackground, clrboxForeground;

        void AddCharToolBar(ToolBarTray tray, int band, int index)
        {
            // Create ToolBar and add to ToolBarTray.
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            // Create ComboBox for font families.
            comboFamily = new ComboBox();
            comboFamily.Width = 144;
            comboFamily.ItemsSource = Fonts.SystemFontFamilies;
            comboFamily.SelectedItem = txtbox.FontFamily;
            comboFamily.SelectionChanged += FamilyComboOnSelection;
            toolbar.Items.Add(comboFamily);

            ToolTip tip = new ToolTip();
            tip.Content = "Font Family";
            comboFamily.ToolTip = tip;

            // Create ComboBox for font size.
            comboSize = new ComboBox();
            comboSize.Width = 48;
            comboSize.IsEditable = true;
            comboSize.Text = (0.75 * txtbox.FontSize).ToString();
            comboSize.ItemsSource = new double[] 
                { 
                    8, 9, 10, 11, 12, 14, 16, 18,
                    20, 22, 24, 26, 28, 36, 48, 72 
                };
            comboSize.SelectionChanged += SizeComboOnSelection;
            comboSize.GotKeyboardFocus += SizeComboOnGotFocus;
            comboSize.LostKeyboardFocus += SizeComboOnLostFocus;
            comboSize.PreviewKeyDown += SizeComboOnKeyDown;
            toolbar.Items.Add(comboSize);

            tip = new ToolTip();
            tip.Content = "Font Size";
            comboSize.ToolTip = tip;

            // Create Bold button.
            btnBold = new ToggleButton();
            btnBold.Checked += BoldButtonOnChecked;
            btnBold.Unchecked += BoldButtonOnChecked;
            toolbar.Items.Add(btnBold);

            Image img = new Image();
            img.Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/boldhs.png"));
            img.Stretch = Stretch.None;
            btnBold.Content = img;

            tip = new ToolTip();
            tip.Content = "Bold";
            btnBold.ToolTip = tip;

            // Create Italic button.
            btnItalic = new ToggleButton();
            btnItalic.Checked += ItalicButtonOnChecked;
            btnItalic.Unchecked += ItalicButtonOnChecked;
            toolbar.Items.Add(btnItalic);

            img = new Image();
            img.Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/ItalicHS.png"));
            img.Stretch = Stretch.None;
            btnItalic.Content = img;

            tip = new ToolTip();
            tip.Content = "Italic";
            btnItalic.ToolTip = tip;

            toolbar.Items.Add(new Separator());

            // Create Background and Foreground color menu.
            Menu menu = new Menu();
            toolbar.Items.Add(menu);

            // Create Background menu item.
            MenuItem item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/ColorHS.png"));
            img.Stretch = Stretch.None;
            item.Header = img;

            clrboxBackground = new ColorGridBox();
            clrboxBackground.SelectionChanged += BackgroundOnSelectionChanged;
            item.Items.Add(clrboxBackground);

            tip = new ToolTip();
            tip.Content = "Background Color";
            item.ToolTip = tip;

            // Create Foreground menu item.
            item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(
                    new Uri("pack://application:,,/Images/Color_fontHS.png"));
            img.Stretch = Stretch.None;
            item.Header = img;

            clrboxForeground = new ColorGridBox();
            clrboxForeground.SelectionChanged += ForegroundOnSelectionChanged;
            item.Items.Add(clrboxForeground);

            tip = new ToolTip();
            tip.Content = "Foreground Color";
            item.ToolTip = tip;

            // Install handler for RichTextBox SelectionChanged event.
            txtbox.SelectionChanged += TextBoxOnSelectionChanged;
        }

        // Handler for RichTextBox SelectionChanged event.
        void TextBoxOnSelectionChanged(object sender, RoutedEventArgs args)
        {
            // Obtain FontFamily of currently selected text...
            object obj = txtbox.Selection.GetPropertyValue(
                                            FlowDocument.FontFamilyProperty);
            // ... and set it in the ComboBox.
            if (obj is FontFamily)
                comboFamily.SelectedItem = (FontFamily)obj;
            else
                comboFamily.SelectedIndex = -1;

            // Obtain FontSize of currently selected text...
            obj = txtbox.Selection.GetPropertyValue(
                                            FlowDocument.FontSizeProperty);

            // ... and set it in the ComboBox.
            if (obj is double)
                comboSize.Text = (0.75 * (double)obj).ToString();
            else
                comboSize.SelectedIndex = -1;

            // Obtain FontWeight of currently selected text...
            obj = txtbox.Selection.GetPropertyValue(
                                            FlowDocument.FontWeightProperty);

            // .. and set the ToggleButton.
            if (obj is FontWeight)
                btnBold.IsChecked = (FontWeight)obj == FontWeights.Bold;

            // Obtain FontStyle of currently selected text...
            obj = txtbox.Selection.GetPropertyValue(
                                            FlowDocument.FontStyleProperty);
            // .. and set the ToggleButton.
            if (obj is FontStyle)
                btnItalic.IsChecked = (FontStyle)obj == FontStyles.Italic;

            // Obtain colors and set the ColorGridBox controls.
            obj = txtbox.Selection.GetPropertyValue(
                                            FlowDocument.BackgroundProperty);
            if (obj != null && obj is Brush)
                clrboxBackground.SelectedValue = (Brush)obj;

            obj = txtbox.Selection.GetPropertyValue(
                                            FlowDocument.ForegroundProperty);
            if (obj != null && obj is Brush)
                clrboxForeground.SelectedValue = (Brush)obj;
        }

        // Handler for FontFamily ComboBox SelectionChanged. 
        void FamilyComboOnSelection(object sender, SelectionChangedEventArgs args)
        {
            // Obtain selected FontFamily.
            ComboBox combo = args.Source as ComboBox;
            FontFamily family = combo.SelectedItem as FontFamily;

            // Set it on selected text.
            if (family != null)
                txtbox.Selection.ApplyPropertyValue(
                                FlowDocument.FontFamilyProperty, family);

            // Set focus back to TextBox.
            txtbox.Focus();
        }

        // Handlers for FontSize ComboBox.
        string strOriginal;

        void SizeComboOnGotFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            strOriginal = (sender as ComboBox).Text;
        }
        void SizeComboOnLostFocus(object sender, KeyboardFocusChangedEventArgs args)
        {
            double size;

            if (Double.TryParse((sender as ComboBox).Text, out size))
                txtbox.Selection.ApplyPropertyValue(
                        FlowDocument.FontSizeProperty, size / 0.75);
            else
                (sender as ComboBox).Text = strOriginal;
        }
        void SizeComboOnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                (sender as ComboBox).Text = strOriginal;
                args.Handled = true;
                txtbox.Focus();
            }
            else if (args.Key == Key.Enter)
            {
                args.Handled = true;
                txtbox.Focus();
            }
        }
        void SizeComboOnSelection(object sender, SelectionChangedEventArgs args)
        {
            ComboBox combo = args.Source as ComboBox;

            if (combo.SelectedIndex != -1)
            {
                double size = (double) combo.SelectedValue;

                txtbox.Selection.ApplyPropertyValue(
                            FlowDocument.FontSizeProperty, size / 0.75);
                txtbox.Focus();
            }
        }

        // Handler for Bold button.
        void BoldButtonOnChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = args.Source as ToggleButton;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.FontWeightProperty,
                (bool)btn.IsChecked ? FontWeights.Bold : FontWeights.Normal);
        }
        // Handler for Italic button.
        void ItalicButtonOnChecked(object sender, RoutedEventArgs args)
        {
            ToggleButton btn = args.Source as ToggleButton;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.FontStyleProperty,
                (bool)btn.IsChecked ? FontStyles.Italic : FontStyles.Normal);
        }
        // Handler for Background color changed.
        void BackgroundOnSelectionChanged(object sender, 
                                          SelectionChangedEventArgs args)
        {
            ColorGridBox clrbox = args.Source as ColorGridBox;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.BackgroundProperty,
                                                clrbox.SelectedValue);
        }
        // Handler for Foreground color changed.
        void ForegroundOnSelectionChanged(object sender, 
                                          SelectionChangedEventArgs args)
        {
            ColorGridBox clrbox = args.Source as ColorGridBox;

            txtbox.Selection.ApplyPropertyValue(FlowDocument.ForegroundProperty,
                                                clrbox.SelectedValue);
        }
    }
}
