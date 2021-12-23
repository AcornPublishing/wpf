//--------------------------------------------
// WizardPage4.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ComputerDatingWizard
{
    public partial class WizardPage4: Page
    {
        // Constructor.
        public WizardPage4(Vitals vitals)
        {
            InitializeComponent();

            // Set text in the page.
            runName.Text = vitals.Name;
            runHome.Text = vitals.Home;
            runGender.Text = vitals.Gender;
            runOS.Text = vitals.FavoriteOS;
            runDirectory.Text = vitals.Directory;
            runMomsMaidenName.Text = vitals.MomsMaidenName;
            runPet.Text = vitals.Pet;
            runIncome.Text = vitals.Income;
        }
        // Event handlers for Previous and Submit buttons.
        void PreviousButtonOnClick(object sender, RoutedEventArgs args)
        {
            NavigationService.GoBack();
        }
        void SubmitButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("Thank you!\n\nYou will be contacted by email " +
                            "in four to six months.", 
                            Application.Current.MainWindow.Title,
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
            Application.Current.Shutdown();
        }
    }
}
