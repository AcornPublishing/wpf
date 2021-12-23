//-----------------------------------------------------
// DataEntryWithListBox.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using Petzold.MultiRecordDataEntry;
using Petzold.SingleRecordDataEntry;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.DataEntryWithListBox
{
    public partial class DataEntryWithListBox
    {
        ListCollectionView collview;
        People people;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DataEntryWithListBox());
        }
        public DataEntryWithListBox()
        {
            InitializeComponent();

            // Simulate File New command.
            ApplicationCommands.New.Execute(null, this);

            // Set focus to first TextBox in panel. 
            pnlPerson.Children[1].Focus();
        }

        void NewOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            people = new People();
            people.Add(new Person());
            InitializeNewPeopleObject();
        }
        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            people = People.Load(this);

            if (people != null)
                InitializeNewPeopleObject();
        }
        void SaveOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            people.Save(this);
        }
        void InitializeNewPeopleObject()
        {
            // Create a ListCollectionView object based on the People object.
            collview = new ListCollectionView(people);

            // Define a property to sort the view.
            collview.SortDescriptions.Add(
                new SortDescription("LastName", ListSortDirection.Ascending));

            // Link the ListBox and PersonPanel through the ListCollectionView.
            lstbox.ItemsSource = collview;
            pnlPerson.DataContext = collview;

            // Set the selected index of the ListBox.
            if (lstbox.Items.Count > 0)
                lstbox.SelectedIndex = 0;
        }

        void AddOnClick(object sender, RoutedEventArgs args)
        {
            Person person = new Person();
            people.Add(person);
            lstbox.SelectedItem = person;
            pnlPerson.Children[1].Focus();
            collview.Refresh();
        }
        void DeleteOnClick(object sender, RoutedEventArgs args)
        {
            if (lstbox.SelectedItem != null)
            {
                people.Remove(lstbox.SelectedItem as Person);

                if (lstbox.Items.Count > 0)
                    lstbox.SelectedIndex = 0;
                else
                    AddOnClick(sender, args);
            }
        }
    }
}
