//---------------------------------------------------------
// CollectionViewWithFilter.cs (c) 2006 by Charles Petzold
//---------------------------------------------------------
using Petzold.SingleRecordDataEntry;
using Petzold.MultiRecordDataEntry;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CollectionViewWithFilter
{
    public partial class CollectionViewWithFilter : Window
    {
        ListCollectionView collview;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CollectionViewWithFilter());
        }
        public CollectionViewWithFilter()
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
                    new SortDescription("LastName",
                                        ListSortDirection.Ascending));

                lstbox.ItemsSource = collview;

                if (lstbox.Items.Count > 0)
                    lstbox.SelectedIndex = 0;

                radioAll.IsChecked = true;
            }
        }

        // Event handlers for RadioButtons.
        void RadioOnChecked(object sender, RoutedEventArgs args)
        {
            if (collview == null)
                return;

            RadioButton radio = args.Source as RadioButton;

            switch (radio.Name)
            {
                case "radioLiving":
                    collview.Filter = PersonIsLiving;
                    break;

                case "radioDead":
                    collview.Filter = PersonIsDead;
                    break;

                case "radioAll":
                    collview.Filter = null;
                    break;
            }
        }
        bool PersonIsLiving(object obj)
        {
            return (obj as Person).DeathDate == null;
        }
        bool PersonIsDead(object obj)
        {
            return (obj as Person).DeathDate != null;
        }
    }
}