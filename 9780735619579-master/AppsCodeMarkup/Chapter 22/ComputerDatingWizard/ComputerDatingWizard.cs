//-----------------------------------------------------
// ComputerDatingWizard.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ComputerDatingWizard
{
    public partial class ComputerDatingWizard 
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ComputerDatingWizard());
        }
        public ComputerDatingWizard()
        {
            InitializeComponent();

            // Navigate to the greeting page.
            frame.Navigate(new WizardPage0());
        }
    }
}
