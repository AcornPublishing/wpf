//--------------------------------------------
// WizardPage3.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ComputerDatingWizard
{
    public partial class WizardPage3: Page
    {
        Vitals vitals;

        // Constructor.
        public WizardPage3(Vitals vitals)
        {
            InitializeComponent();
            this.vitals = vitals;
        }
        // Event handlers for Previous and Finish buttons.
        void PreviousButtonOnClick(object sender, RoutedEventArgs args)
        {
            NavigationService.GoBack();
        }
        void FinishButtonOnClick(object sender, RoutedEventArgs args)
        {
            // Save information from this page.
            vitals.MomsMaidenName = txtboxMom.Text;
            vitals.Pet = 
                Vitals.GetCheckedRadioButton(grpboxPet).Content as string;
            vitals.Income = 
                Vitals.GetCheckedRadioButton(grpboxIncome).Content as string;

            // Always re-create the final page.
            WizardPage4 page = new WizardPage4(vitals);
            NavigationService.Navigate(page);
        }
    }
}
