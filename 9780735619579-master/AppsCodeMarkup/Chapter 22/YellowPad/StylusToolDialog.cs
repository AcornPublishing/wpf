//-------------------------------------------------
// StylusToolDialog.cs (c) 2006 by Charles Petzold
//-------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace Petzold.YellowPad
{
    public partial class StylusToolDialog : Window
    {
        // Constructor.
        public StylusToolDialog()
        {
            InitializeComponent();

            // Set event handlers to enable OK button.
            txtboxWidth.TextChanged += TextBoxOnTextChanged;
            txtboxHeight.TextChanged += TextBoxOnTextChanged;
            txtboxAngle.TextChanged += TextBoxOnTextChanged;

            txtboxWidth.Focus();
        }
        // Public property initializes controls and returns their values.
        public DrawingAttributes DrawingAttributes
        {
            set 
            { 
                txtboxHeight.Text = (0.75 * value.Height).ToString("F1");
                txtboxWidth.Text = (0.75 * value.Width).ToString("F1");
                txtboxAngle.Text = 
                    (180 * Math.Acos(value.StylusTipTransform.M11) / 
                                                    Math.PI).ToString("F1");

                chkboxPressure.IsChecked = value.IgnorePressure;
                chkboxHighlighter.IsChecked = value.IsHighlighter;

                if (value.StylusTip == StylusTip.Ellipse)
                    radioEllipse.IsChecked = true;
                else
                    radioRect.IsChecked = true;

                lstboxColor.SelectedColor = value.Color;
                lstboxColor.ScrollIntoView(lstboxColor.SelectedColor);
            }
            get 
            {
                DrawingAttributes drawattr = new DrawingAttributes();

                drawattr.Height = Double.Parse(txtboxHeight.Text) / 0.75;
                drawattr.Width = Double.Parse(txtboxWidth.Text) / 0.75;
                drawattr.StylusTipTransform = 
                    new RotateTransform(Double.Parse(txtboxAngle.Text)).Value;

                drawattr.IgnorePressure = (bool)chkboxPressure.IsChecked;
                drawattr.IsHighlighter = (bool)chkboxHighlighter.IsChecked;
                drawattr.StylusTip =
                    (bool)radioEllipse.IsChecked ? StylusTip.Ellipse : 
                                                   StylusTip.Rectangle;

                drawattr.Color = lstboxColor.SelectedColor;
                return drawattr; 
            }
        }
        // Event handler enables OK button only if all fields are valid.
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            double width, height, angle;

            btnOk.IsEnabled = Double.TryParse(txtboxWidth.Text, out width) &&
                              width / 0.75 >= DrawingAttributes.MinWidth &&
                              width / 0.75 <= DrawingAttributes.MaxWidth &&
                              Double.TryParse(txtboxHeight.Text, out height) &&
                              height / 0.75 >= DrawingAttributes.MinHeight &&
                              height / 0.75 <= DrawingAttributes.MaxHeight &&
                              Double.TryParse(txtboxAngle.Text, out angle);
        }
        // OK button terminates dialog.
        void OkOnClick(object sender, RoutedEventArgs args)
        {
            DialogResult = true;
        }
    }
}
