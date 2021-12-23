//-----------------------------------------------------
// XamlCruncherSettings.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.XamlCruncher
{
    public class XamlCruncherSettings : Petzold.NotepadClone.NotepadCloneSettings
    {
        // Default settings of user preferences.
        public Dock Orientation = Dock.Left;
        public int TabSpaces = 4;
        public string StartupDocument = 
            "<Button xmlns=\"http://schemas.microsoft.com/winfx" + 
                        "/2006/xaml/presentation\"\r\n" +
            "        xmlns:x=\"http://schemas.microsoft.com/winfx" + 
                        "/2006/xaml\">\r\n" +
            "    Hello, XAML!\r\n" +
            "</Button>\n";

        // Constructor to initialize default settings in NotepadCloneSettings.
        public XamlCruncherSettings()
        {
            FontFamily = "Lucida Console";
            FontSize = 10 / 0.75;
        }
    }
}