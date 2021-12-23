//-----------------------------------------------
// FilterWithText.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using Petzold.SingleRecordDataEntry;
using Petzold.MultiRecordDataEntry;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.FilterWithText
{
    public partial class FilterWithText : Window
    {
        ListCollectionView collview;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FilterWithText());
        }
        public FilterWithText()
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

                txtboxFilter.Text = "";
                collview.Filter = LastNameFilter;

                lstbox.ItemsSource = collview;

                if (lstbox.Items.Count > 0)
                    lstbox.SelectedIndex = 0;
            }
        }
        bool LastNameFilter(object obj)
        {
            return (obj as Person).LastName.StartsWith(txtboxFilter.Text, 
                                    StringComparison.CurrentCultureIgnoreCase);
        }
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (collview == null)
                return;

            collview.Refresh();
        }
    }
}