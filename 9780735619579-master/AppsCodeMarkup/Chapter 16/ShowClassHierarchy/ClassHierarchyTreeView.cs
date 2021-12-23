//----------------------------------------------------
// ClassHierarchyTreeView (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ShowClassHierarchy
{
    public class ClassHierarchyTreeView : TreeView
    {
        public ClassHierarchyTreeView(Type typeRoot)
        {
            // Make sure PresentationCore is loaded.
            UIElement dummy = new UIElement();

            // Put all the referenced assemblies in a List.
            List<Assembly> assemblies = new List<Assembly>();

            // Get all referenced assemblies.
            AssemblyName[] anames = 
                    Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            // Add to assemblies list.
            foreach (AssemblyName aname in anames)
                assemblies.Add(Assembly.Load(aname));

            // Store descendants of typeRoot in a sorted list.
            SortedList<string, Type> classes = new SortedList<string, Type>();
            classes.Add(typeRoot.Name, typeRoot);

            // Get all the types in the assembly.
            foreach (Assembly assembly in assemblies)
                foreach (Type typ in assembly.GetTypes())
                    if (typ.IsPublic && typ.IsSubclassOf(typeRoot))
                        classes.Add(typ.Name, typ);
            
            // Create root item.
            TypeTreeViewItem item = new TypeTreeViewItem(typeRoot);
            Items.Add(item);

            // Call recursive method.
            CreateLinkedItems(item, classes);
        }
        void CreateLinkedItems(TypeTreeViewItem itemBase, 
                               SortedList<string, Type> list)
        {
            foreach (KeyValuePair<string, Type> kvp in list)
                if (kvp.Value.BaseType == itemBase.Type)
                {
                    TypeTreeViewItem item = new TypeTreeViewItem(kvp.Value);
                    itemBase.Items.Add(item);
                    CreateLinkedItems(item, list);
                }
        }
    }
}
