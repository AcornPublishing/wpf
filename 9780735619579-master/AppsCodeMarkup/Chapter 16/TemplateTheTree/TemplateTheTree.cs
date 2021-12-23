//------------------------------------------------
// TemplateTheTree.cs (c) 2006 by Charles Petzold
//------------------------------------------------
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.TemplateTheTree
{
    public class TemplateTheTree : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TemplateTheTree());
        }
        public TemplateTheTree()
        {
            Title = "Template the Tree";

            // Create TreeView and set as content of window.
            TreeView treevue = new TreeView();
            Content = treevue;

            // Create HierarchicalDataTemplate based on DiskDirectory.
            HierarchicalDataTemplate template = 
                        new HierarchicalDataTemplate(typeof(DiskDirectory));

            // Set Subdirectories property as ItemsSource.
            template.ItemsSource = new Binding("Subdirectories");

            // Create FrameworkElementFactory for TextBlock.
            FrameworkElementFactory factoryTextBlock = 
                            new FrameworkElementFactory(typeof(TextBlock));

            // Bind Text property with Name property from DiskDirectory.
            factoryTextBlock.SetBinding(TextBlock.TextProperty, 
                                        new Binding("Name"));
            
            // Set this Textblock as the VisualTree of the template.
            template.VisualTree = factoryTextBlock;

            // Create a DiskDirectory object for the system drive.
            DiskDirectory dir = new DiskDirectory(
                new DirectoryInfo(
                    Path.GetPathRoot(Environment.SystemDirectory)));

            // Create a root TreeViewItem and set its properties.
            TreeViewItem item = new TreeViewItem();
            item.Header = dir.Name;
            item.ItemsSource = dir.Subdirectories;
            item.ItemTemplate = template;

            // Add TreeViewItem to TreeView.
            treevue.Items.Add(item);
            item.IsExpanded = true;
        }
    }
}
