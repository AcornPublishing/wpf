//--------------------------------------
// Page2.cs (c) 2006 by Charles Petzold
//--------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Petzold.NavigationDemo
{
    public partial class Page2
    {
        public Page2()
        {
            InitializeComponent();
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            NavigationService.Navigate(
                        new Uri("Page1.xaml", UriKind.Relative));
        }
        void HyperlinkOnRequestNavigate(object sender, 
                                        RequestNavigateEventArgs args)
        {
            NavigationService.Navigate(args.Uri);
            args.Handled = true;
        }
    }
}
