//-----------------------------------------------------------
// DependencyPropertyListView.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.ExploreDependencyProperties
{
    public class DependencyPropertyListView : ListView
    {
        // Define dependency property for Type.
        public static DependencyProperty TypeProperty;

        // Register dependency property in static constructor.
        static DependencyPropertyListView()
        {
            TypeProperty = DependencyProperty.Register("Type", typeof(Type), 
                    typeof(DependencyPropertyListView),
                    new PropertyMetadata(null, 
                        new PropertyChangedCallback(OnTypePropertyChanged)));
        }
        // Static method called when TypeProperty changes.
        static void OnTypePropertyChanged(DependencyObject obj,
                                    DependencyPropertyChangedEventArgs args)
        {
            // Get the ListView object involved.
            DependencyPropertyListView lstvue = obj as DependencyPropertyListView;

            // Get the new value of the Type property.
            Type type = args.NewValue as Type;

            // Get rid of all the items currently stored by the ListView.
            lstvue.ItemsSource = null;

            // Get all the DependencyProperty fields in the Type object.
            if (type != null)
            {
                SortedList<string, DependencyProperty> list = 
                                    new SortedList<string, DependencyProperty>();
                FieldInfo[] infos = type.GetFields();

                foreach (FieldInfo info in infos)
                    if (info.FieldType == typeof(DependencyProperty))
                        list.Add(info.Name, 
                                 (DependencyProperty)info.GetValue(null));

                // Set the ItemsSource to the list.
                lstvue.ItemsSource = list.Values;
            }
        }
        // Public Type property 
        public Type Type
        {
            set { SetValue(TypeProperty, value); }
            get { return (Type)GetValue(TypeProperty); }
        }
        // Constructor.
        public DependencyPropertyListView()
        {
            // Create a GridView and set to View property.
            GridView grdvue = new GridView();
            this.View = grdvue;

            // First column displays the 'Name' property of DependencyProperty.
            GridViewColumn col = new GridViewColumn();
            col.Header = "Name";
            col.Width = 150;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            // Second column is labeled 'Owner'.
            col = new GridViewColumn();
            col.Header = "Owner";
            col.Width = 100;
            grdvue.Columns.Add(col);

            // Second column displays 'OwnerType' of DependencyProperty.
            // This one requires a data template.
            DataTemplate template = new DataTemplate();
            col.CellTemplate = template;

            // A TextBlock will display the data.
            FrameworkElementFactory elTextBlock = 
                        new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBlock;

            // Bind the 'OwnerType' property of DependencyProperty
            //  with the Text property of the TextBlock
            //  using a converter of TypeToString.
            Binding bind = new Binding("OwnerType");
            bind.Converter = new TypeToString();
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            // Third column is labeled 'Type'
            col = new GridViewColumn();
            col.Header = "Type";
            col.Width = 100;
            grdvue.Columns.Add(col);

            // This one requires a similar template to bind with 'PropertyType'.
            template = new DataTemplate();
            col.CellTemplate = template;
            elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBlock;
            bind = new Binding("PropertyType");
            bind.Converter = new TypeToString();
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);

            // Fourth column labeled 'Default' displays 
            //  DefaultMetadata.DefaultValue.
            col = new GridViewColumn();
            col.Header = "Default";
            col.Width = 75;
            col.DisplayMemberBinding = 
                            new Binding("DefaultMetadata.DefaultValue");
            grdvue.Columns.Add(col);

            // Fifth column is similar.
            col = new GridViewColumn();
            col.Header = "Read-Only";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.ReadOnly");
            grdvue.Columns.Add(col);

            // Sixth column, ditto.
            col = new GridViewColumn();
            col.Header = "Usage";
            col.Width = 75;
            col.DisplayMemberBinding = 
                            new Binding("DefaultMetadata.AttachedPropertyUsage");
            grdvue.Columns.Add(col);

            // Seventh column displays metadata flags.
            col = new GridViewColumn();
            col.Header = "Flags";
            col.Width = 250;
            grdvue.Columns.Add(col);

            // A template is required to convert using MetadataToFlags.
            template = new DataTemplate();
            col.CellTemplate = template;
            elTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBlock;
            bind = new Binding("DefaultMetadata");
            bind.Converter = new MetadataToFlags();
            elTextBlock.SetBinding(TextBlock.TextProperty, bind);
        }
    }
}
