//-----------------------------------------------------
// FileSystemInfoButton.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Diagnostics;       // For the Process class
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ExploreDirectories
{
    public class FileSystemInfoButton : Button
    {
        FileSystemInfo info;

        // Parameterless constructor make "My Documents" button.
        public FileSystemInfoButton()
            :
            this(new DirectoryInfo(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
        {
        }

        // Single-argument constructor makes directory or file button.
        public FileSystemInfoButton(FileSystemInfo info)
        {
            this.info = info;
            Content = info.Name;
            if (info is DirectoryInfo)
                FontWeight = FontWeights.Bold;
            Margin = new Thickness(10);
        }

        // Two-argument constructor makes "Parent Directory" button.
        public FileSystemInfoButton(FileSystemInfo info, string str)
            :
            this(info)
        {
            Content = str;
        }

        // OnClick override does everything else.
        protected override void OnClick()
        {
            if (info is FileInfo)
            {
                Process.Start(info.FullName);
            }
            else if (info is DirectoryInfo)
            {
                DirectoryInfo dir = info as DirectoryInfo;
                Application.Current.MainWindow.Title = dir.FullName;

                Panel pnl = Parent as Panel;
                pnl.Children.Clear();

                if (dir.Parent != null)
                    pnl.Children.Add(new FileSystemInfoButton(dir.Parent, ".."));

                foreach (FileSystemInfo inf in dir.GetFileSystemInfos())
                    pnl.Children.Add(new FileSystemInfoButton(inf));
            }
            base.OnClick();
        }
    }
}