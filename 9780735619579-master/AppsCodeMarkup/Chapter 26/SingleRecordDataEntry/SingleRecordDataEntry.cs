//------------------------------------------------------
// SingleRecordDataEntry.cs (c) 2006 by Charles Petzold
//------------------------------------------------------
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Petzold.SingleRecordDataEntry
{
    public partial class SingleRecordDataEntry : Window
    {
        const string strFilter = "Person XML files (*.PersonXml)|" +
                                 "*.PersonXml|All files (*.*)|*.*";
        XmlSerializer xml = new XmlSerializer(typeof(Person));

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SingleRecordDataEntry());
        }
        public SingleRecordDataEntry()
        {
            InitializeComponent();

            // Simulate File New command.
            ApplicationCommands.New.Execute(null, this);

            // Set focus to first TextBox in panel.
            pnlPerson.Children[1].Focus();
        }
        // Event handlers for menu items.
        void NewOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            pnlPerson.DataContext = new Person();
        }
        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = strFilter;
            Person pers;

            if ((bool)dlg.ShowDialog(this))
            {
                try
                {
                    StreamReader reader = new StreamReader(dlg.FileName);
                    pers = (Person) xml.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not load file: " + exc.Message, 
                                    Title, MessageBoxButton.OK, 
                                    MessageBoxImage.Exclamation);
                    return;
                }
                pnlPerson.DataContext = pers;
            }
        }
        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(this))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(dlg.FileName);
                    xml.Serialize(writer, pnlPerson.DataContext);
                    writer.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not save file: " + exc.Message,
                                    Title, MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                    return;
                }
            }
        }
    }
}
