//--------------------------------------------------------
// DataEntryWithNavigation.cs (c) 2006 by Charles Petzold
//--------------------------------------------------------
using Petzold.DataEntry;
using Petzold.MultiRecordDataEntry;
using Petzold.SingleRecordDataEntry;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.DataEntryWithNavigation
{
    public partial class DataEntryWithNavigation
    {
        People people;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DataEntryWithNavigation());
        }
        public DataEntryWithNavigation()
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
            people = new People();
            people.Add(new Person());
            InitializeNewPeopleObject();
        }
        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            people = People.Load(this);
            InitializeNewPeopleObject();
        }
        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            people.Save(this);
        }
        void InitializeNewPeopleObject()
        {
            navbar.Collection = people;
            navbar.ItemType = typeof(Person);
            pnlPerson.DataContext = people;
        }
    }
}
