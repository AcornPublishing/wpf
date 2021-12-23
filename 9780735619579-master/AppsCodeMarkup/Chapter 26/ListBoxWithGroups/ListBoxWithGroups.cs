//--------------------------------------------------
// ListBoxWithGroups.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using Petzold.MultiRecordDataEntry;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ListBoxWithGroups
{
    public partial class ListBoxWithGroups : Window
    {
        ListCollectionView collview;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListBoxWithGroups());
        }
        public ListBoxWithGroups()
        {
            InitializeComponent();
        }

        void OpenOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            People people = People.Load(this);

            if (people != null)
            {
                collview = new ListCollectionView(people);

                collview.SortDescriptions.Add(
                    new SortDescription("BirthDate",
                                        ListSortDirection.Ascending));

                // Add PeriodGroupsDescription to GroupsDescriptions collection.
                collview.GroupDescriptions.Add(new PeriodGroupDescription());

                lstbox.ItemsSource = collview;

                if (lstbox.Items.Count > 0)
                    lstbox.SelectedIndex = 0;
            }
        }
    }
}
