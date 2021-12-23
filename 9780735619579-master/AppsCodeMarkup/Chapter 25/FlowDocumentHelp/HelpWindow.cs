//-------------------------------------------
// HelpWindow.cs (c) 2006 by Charles Petzold
//-------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Petzold.FlowDocumentHelp
{
    public partial class HelpWindow 
    {
        // Public constructors.
        public HelpWindow()
        {
            InitializeComponent();
            treevue.Focus();
        }

        public HelpWindow(string strTopic): this()
        {
            if (strTopic != null)
                frame.Source = new Uri(strTopic, UriKind.Relative);
        }

        // When TreeView selected item changes, set the source of the Frame,
        void TreeViewOnSelectedItemChanged(object sender, 
                                RoutedPropertyChangedEventArgs<object> args)
        {
            if (treevue.SelectedValue != null)
                frame.Source = new Uri(treevue.SelectedValue as string, 
                                       UriKind.Relative);
        }

        // When the Frame has navigated to a new source, synchronize TreeView.
        void FrameOnNavigated(object sender, NavigationEventArgs args)
        {
            if (args.Uri != null && args.Uri.OriginalString != null && 
                                    args.Uri.OriginalString.Length > 0)
            {
                FindItemToSelect(treevue, args.Uri.OriginalString);
            }
        }
        // Search through items in TreeView to select one.
        bool FindItemToSelect(ItemsControl ctrl, string strSource)
        {
            foreach (object obj in ctrl.Items)
            {
                System.Xml.XmlElement xml = obj as System.Xml.XmlElement;
                string strAttribute = xml.GetAttribute("Source");
                TreeViewItem item = (TreeViewItem)
                    ctrl.ItemContainerGenerator.ContainerFromItem(obj);

                // If the TreeViewItem matches the Frame URI, select the item.
                if (strAttribute != null && strAttribute.Length > 0 && 
                                            strSource.EndsWith(strAttribute))
                {
                    if (item != null && !item.IsSelected)
                        item.IsSelected = true;

                    return true;
                }

                // Expand the item to search nested items.
                if (item != null)
                {
                    bool isExpanded = item.IsExpanded;
                    item.IsExpanded = true;

                    if (item.HasItems && FindItemToSelect(item, strSource))
                        return true;

                    item.IsExpanded = isExpanded;
                }
            }
            return false;
        }
    }
}
