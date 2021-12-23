//--------------------------------------------
// PrintBanner.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PrintBanner
{
    public class PrintBanner : Window
    {
        TextBox txtbox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintBanner());
        }
        public PrintBanner()
        {
            Title = "Print Banner";
            SizeToContent = SizeToContent.WidthAndHeight;

            // Make StackPanel content of window.
            StackPanel stack = new StackPanel();
            Content = stack;

            // Create TextBox.
            txtbox = new TextBox();
            txtbox.Width = 250;
            txtbox.Margin = new Thickness(12);
            stack.Children.Add(txtbox);

            // Create Button.
            Button btn = new Button();
            btn.Content = "_Print...";
            btn.Margin = new Thickness(12);
            btn.Click += PrintOnClick;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            stack.Children.Add(btn);

            txtbox.Focus();
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

                // Create new BannerDocumentPaginator object.
                BannerDocumentPaginator paginator = new BannerDocumentPaginator();

                // Set Text property from TextBox.
                paginator.Text = txtbox.Text;

                // Give it a PageSize property based on the paper dimensions.
                paginator.PageSize = new Size(dlg.PrintableAreaWidth, 
                                              dlg.PrintableAreaHeight);

                // Call PrintDocument to print the document.
                dlg.PrintDocument(paginator, "Banner: " + txtbox.Text);
            }
        }
    }
}
