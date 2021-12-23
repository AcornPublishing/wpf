//------------------------------------------------
// ControlMenuItem.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.DumpControlTemplate
{
    public class ControlMenuItem : MenuItem
    {
        public ControlMenuItem()
        {
            // Obtain the assembly in which the Control class is defined.
            Assembly asbly = Assembly.GetAssembly(typeof(Control));

            // This is an array of all the types in that class.
            Type[] atype = asbly.GetTypes();

            // We're going to store descendants of Control in a sorted list.
            SortedList<string, MenuItem> sortlst = 
                                    new SortedList<string, MenuItem>();

            Header = "Control";
            Tag = typeof(Control);
            sortlst.Add("Control", this);

            // Enumerate all the types in the array.
            // For Control and its descendants, create menu items and
            //  add to the SortedList object.
            // Notice the menu item Tag property is the associated Type object.
            foreach (Type typ in atype)
            {
                if (typ.IsPublic && (typ.IsSubclassOf(typeof(Control))))
                {
                    MenuItem item = new MenuItem();
                    item.Header = typ.Name;
                    item.Tag = typ;
                    sortlst.Add(typ.Name, item);
                }
            }

            // Go through the sorted list and set menu item parents.
            foreach (KeyValuePair<string, MenuItem> kvp in sortlst)
            {
                if (kvp.Key != "Control")
                {
                    string strParent = ((Type)kvp.Value.Tag).BaseType.Name;
                    MenuItem itemParent = sortlst[strParent];
                    itemParent.Items.Add(kvp.Value);
                }
            }

            // Scan through again:
            //  If abstract and selectable, disable.
            //  If not abstract and not selectable, add a new item.
            foreach (KeyValuePair<string, MenuItem> kvp in sortlst)
            {
                Type typ = (Type)kvp.Value.Tag;

                if (typ.IsAbstract && kvp.Value.Items.Count == 0)
                    kvp.Value.IsEnabled = false;

                if (!typ.IsAbstract && kvp.Value.Items.Count > 0)
                {
                    MenuItem item = new MenuItem();
                    item.Header = kvp.Value.Header as string;
                    item.Tag = typ;
                    kvp.Value.Items.Insert(0, item);
                }
            }
        }
    }
}
