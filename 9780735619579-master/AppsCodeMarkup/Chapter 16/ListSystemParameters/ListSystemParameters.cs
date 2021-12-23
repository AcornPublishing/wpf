//-----------------------------------------------------
// ListSystemParameters.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Collections.Generic;       // for experimentation
using System.ComponentModel;            // for experimentation
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListSystemParameters
{
    class ListSystemParameters : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListSystemParameters());
        }
        public ListSystemParameters()
        {
            Title = "List System Parameters";

            // Create a ListView as content of the window.
            ListView lstvue = new ListView();
            Content = lstvue;

            // Create a GridView as the View of the ListView.
            GridView grdvue = new GridView();
            lstvue.View = grdvue;

            // Create two GridView columns.
            GridViewColumn col = new GridViewColumn();
            col.Header = "Property Name";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Value";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Value");
            grdvue.Columns.Add(col);

            // Get all the system parameters in one handy array.
            PropertyInfo[] props = typeof(SystemParameters).GetProperties();

            // Add the items to the ListView.
            foreach (PropertyInfo prop in props)
                if (prop.PropertyType != typeof(ResourceKey))
                {
                    SystemParam sysparam = new SystemParam();
                    sysparam.Name = prop.Name;
                    sysparam.Value = prop.GetValue(null, null);
                    lstvue.Items.Add(sysparam);
                }
        }
    }
}