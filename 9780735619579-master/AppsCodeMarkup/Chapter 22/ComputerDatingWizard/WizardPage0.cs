//--------------------------------------------
// WizardPage0.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ComputerDatingWizard
{
    public partial class WizardPage0
    {
        public WizardPage0()
        {
            InitializeComponent();
        }
        void BeginButtonOnClick(object sender, RoutedEventArgs args)
        {
            if (NavigationService.CanGoForward)
                NavigationService.GoForward();
            else
            {
                Vitals vitals = new Vitals();
                WizardPage1 page = new WizardPage1(vitals);
                NavigationService.Navigate(page);
            }
        }
    }
}
