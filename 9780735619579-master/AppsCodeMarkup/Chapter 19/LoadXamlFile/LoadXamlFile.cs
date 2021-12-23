//---------------------------------------------
// LoadXamlFile.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace Petzold.LoadXamlFile
{
    public class LoadXamlFile : Window
    {
        Frame frame;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new LoadXamlFile());
        }
        public LoadXamlFile()
        {
            Title = "Load XAML File";

            DockPanel dock = new DockPanel();
            Content = dock;

            // Create button for Open File dialog.
            Button btn = new Button();
            btn.Content = "Open File...";
            btn.Margin = new Thickness(12);
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.Click += ButtonOnClick;
            dock.Children.Add(btn);
            DockPanel.SetDock(btn, Dock.Top);

            // Create Frame for hosting loaded XAML.
            frame = new Frame();
            dock.Children.Add(frame);
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XAML Files (*.xaml)|*.xaml|All files (*.*)|*.*";

            if ((bool)dlg.ShowDialog())
            {
                try
                {
                    // Read file with XmlTextReader.
                    XmlTextReader xmlreader = new XmlTextReader(dlg.FileName);

                    // Convert XAML to object.
                    object obj = XamlReader.Load(xmlreader);

                    // If it's a Window, call Show.
                    if (obj is Window)
                    {
                        Window win = obj as Window;
                        win.Owner = this;
                        win.Show();
                    }

                    // Otherwise, set as Content of Frame.
                    else
                        frame.Content = obj;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
            }
        }
    }
}