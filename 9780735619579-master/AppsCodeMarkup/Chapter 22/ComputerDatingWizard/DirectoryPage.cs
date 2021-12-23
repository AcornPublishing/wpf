//----------------------------------------------
// DirectoryPage.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using Petzold.RecurseDirectoriesIncrementally;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petzold.ComputerDatingWizard
{
    public partial class DirectoryPage : PageFunction<DirectoryInfo>
    {
        // Constructor.
        public DirectoryPage()
        {
            InitializeComponent();
            treevue.SelectedItemChanged += TreeViewOnSelectedItemChanged;
        }
        // Event handler to enable OK button.
        void TreeViewOnSelectedItemChanged(object sender, 
                            RoutedPropertyChangedEventArgs<object> args)
        {
            btnOk.IsEnabled = args.NewValue != null;
        }
        // Event handlers for Cancel and OK.
        void CancelButtonOnClick(object sender, RoutedEventArgs args)
        {
            OnReturn(new ReturnEventArgs<DirectoryInfo>());
        }
        void OkButtonOnClick(object sender, RoutedEventArgs args)
        {
            DirectoryInfo dirinfo = 
                (treevue.SelectedItem as DirectoryTreeViewItem).DirectoryInfo;

            OnReturn(new ReturnEventArgs<DirectoryInfo>(dirinfo));
        }
    }
}
