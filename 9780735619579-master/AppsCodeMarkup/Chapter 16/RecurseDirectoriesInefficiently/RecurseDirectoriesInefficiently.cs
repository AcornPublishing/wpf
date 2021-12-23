//----------------------------------------------------------------
// RecurseDirectoriesInefficiently.cs (c) 2006 by Charles Petzold
//----------------------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.RecurseDirectoriesInefficiently
{
    public class RecurseDirectoriesInefficiently : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecurseDirectoriesInefficiently());
        }
        public RecurseDirectoriesInefficiently()
        {
            Title = "Recurse Directories Inefficiently";

            TreeView tree = new TreeView();
            Content = tree;

            // Create TreeViewItem based on system drive.
            TreeViewItem item = new TreeViewItem();
            item.Header = Path.GetPathRoot(Environment.SystemDirectory);
            item.Tag = new DirectoryInfo(item.Header as string);
            tree.Items.Add(item);

            // Fill recursively.
            GetSubdirectories(item);
        }
        void GetSubdirectories(TreeViewItem item)
        {
            DirectoryInfo dir = item.Tag as DirectoryInfo;
            DirectoryInfo[] subdirs;

            try
            {
                // Get subdirectories.
                subdirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            // Loop through subdirectories.
            foreach (DirectoryInfo subdir in subdirs)
            {
                // Create a new TreeViewItem for each directory.
                TreeViewItem subitem = new TreeViewItem();
                subitem.Header = subdir.Name;
                subitem.Tag = subdir;
                item.Items.Add(subitem);

                // Recursively obtain subdirectories.
                GetSubdirectories(subitem);
            }
        }
    }
}
