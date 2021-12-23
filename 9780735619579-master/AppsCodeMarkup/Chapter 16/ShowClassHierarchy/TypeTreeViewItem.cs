//-------------------------------------------------
// TypeTreeViewItem.cs (c) 2006 by Charles Petzold
//-------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ShowClassHierarchy
{
    class TypeTreeViewItem : TreeViewItem
    {
        Type typ;

        // Two constructors.
        public TypeTreeViewItem()
        {
        }
        public TypeTreeViewItem(Type typ)
        {
            Type = typ;
        }

        // Public Type property of type Type.
        public Type Type
        {
            set
            {
                typ = value;

                if (typ.IsAbstract)
                    Header = typ.Name + " (abstract)";
                else
                    Header = typ.Name;
            }
            get
            {
                return typ;
            }
        }
    }
}
