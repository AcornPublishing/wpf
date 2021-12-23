//---------------------------------------
// People.cs (c) 2006 by Charles Petzold
//---------------------------------------
using Microsoft.Win32;
using Petzold.SingleRecordDataEntry;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Serialization;

namespace Petzold.MultiRecordDataEntry
{
    public class People : ObservableCollection<Person>
    {
        const string strFilter = "People XML files (*.PeopleXml)|" +
                                 "*.PeopleXml|All files (*.*)|*.*";

        public static People Load(Window win)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = strFilter;
            People people = null;

            if ((bool)dlg.ShowDialog(win))
            {
                try
                {
                    StreamReader reader = new StreamReader(dlg.FileName);
                    XmlSerializer xml = new XmlSerializer(typeof(People));
                    people = (People)xml.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not load file: " + exc.Message,
                                    win.Title, MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                    people = null;
                }
            }
            return people;
        }
        public bool Save(Window win)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(win))
            {
                try
                {
                    StreamWriter writer = new StreamWriter(dlg.FileName);
                    XmlSerializer xml = new XmlSerializer(GetType());
                    xml.Serialize(writer, this);
                    writer.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Could not save file: " + exc.Message,
                                    win.Title, MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                    return false;
                }
            }
            return true;
        }
    }
}
