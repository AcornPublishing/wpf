//--------------------------------------------------
// DirectoryTreeView.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.RecurseDirectoriesIncrementally
{
    public class DirectoryTreeView : TreeView
    {
        // Constructor builds partial directory tree.
        public DirectoryTreeView()
        {
            RefreshTree();
        }
        public void RefreshTree()
        {
            BeginInit();
            Items.Clear();

            // Obtain the disk drives.
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                char chDrive = drive.Name.ToUpper()[0];
                DirectoryTreeViewItem item = 
                            new DirectoryTreeViewItem(drive.RootDirectory);

                // Display VolumeLabel if drive ready; otherwise just DriveType.
                if (chDrive != 'A' && chDrive != 'B' && 
                            drive.IsReady && drive.VolumeLabel.Length > 0)
                    item.Text = String.Format("{0} ({1})", drive.VolumeLabel, 
                                                           drive.Name);
                else
                    item.Text = String.Format("{0} ({1})", drive.DriveType, 
                                                           drive.Name);

                // Determine proper bitmap for drive.
                if (chDrive == 'A' || chDrive == 'B')
                    item.SelectedImage = item.UnselectedImage = new BitmapImage(
                        new Uri("pack://application:,,/Images/35FLOPPY.BMP"));

                else if (drive.DriveType == DriveType.CDRom)
                    item.SelectedImage = item.UnselectedImage = new BitmapImage(
                        new Uri("pack://application:,,/Images/CDDRIVE.BMP"));
                else
                    item.SelectedImage = item.UnselectedImage = new BitmapImage(
                        new Uri("pack://application:,,/Images/DRIVE.BMP"));
                        
                Items.Add(item);

                // Populate the drive with directories.
                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady)
                    item.Populate();
            }
            EndInit();
        }
    }
}
