//--------------------------------------------------
// PrintBetterBanner.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using Petzold.ChooseFont;
using Petzold.PrintBanner;
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PrintBetterBanner
{
    public class PrintBetterBanner : Window
    {
        TextBox txtbox;
        Typeface face;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintBetterBanner());
        }
        public PrintBetterBanner()
        {
            Title = "Print Better Banner";
            SizeToContent = SizeToContent.WidthAndHeight;

            // Make StackPanel content of window.
            StackPanel stack = new StackPanel();
            Content = stack;

            // Create TextBox.
            txtbox = new TextBox();
            txtbox.Width = 250;
            txtbox.Margin = new Thickness(12);
            stack.Children.Add(txtbox);

            // Create Font Button.
            Button btn = new Button();
            btn.Content = "_Font...";
            btn.Margin = new Thickness(12);
            btn.Click += FontOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            // Create Print Button.
            btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            // Initialize Facename field.
            face = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            txtbox.Focus();
        }
        void FontOnClick(object sender, RoutedEventArgs args)
        {
            FontDialog dlg = new FontDialog();
            dlg.Owner = this;
            dlg.Typeface = face;

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                face = dlg.Typeface;
            }
        }
        void PrintOnClick(object sender, RoutedEventArgs args)
        {
            PrintDialog dlg = new PrintDialog();

            if (dlg.ShowDialog().GetValueOrDefault())
            {
                // Make sure orientation is Portrait.
                PrintTicket prntkt = dlg.PrintTicket;
                prntkt.PageOrientation = PageOrientation.Portrait;
                dlg.PrintTicket = prntkt;

                // Create new DocumentPaginator object.
                BannerDocumentPaginator paginator = new BannerDocumentPaginator();

                // Set Text property from TextBox.
                paginator.Text = txtbox.Text;

                // Set Typeface property from field.
                paginator.Typeface = face;

                // Give it a PageSize property based on the paper dimensions.
                paginator.PageSize = new Size(dlg.PrintableAreaWidth, 
                                              dlg.PrintableAreaHeight);

                // Call PrintDocument to print the document.
                dlg.PrintDocument(paginator, "Banner: " + txtbox.Text);
            }
        }
    }
}
