//----------------------------------------------------
// DumpControlTemplate.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace Petzold.DumpControlTemplate
{
    public partial class DumpControlTemplate : Window
    {
        Control ctrl;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DumpControlTemplate());
        }
        public DumpControlTemplate()
        {
            InitializeComponent();
        }

        // Event handler for item clicked on Control menu.
        void ControlItemOnClick(object sender, RoutedEventArgs args)
        {
            // Remove any existing child from the first row of the Grid.
            for (int i = 0; i < grid.Children.Count; i++)
                if (Grid.GetRow(grid.Children[i]) == 0)
                {
                    grid.Children.Remove(grid.Children[i]);
                    break;
                }

            // Clear the TextBox.
            txtbox.Text = "";

            // Get the Control class of the clicked menu item.
            MenuItem item = args.Source as MenuItem;
            Type typ = (Type)item.Tag;

            // Prepare to create an object of that type.
            ConstructorInfo info = typ.GetConstructor(System.Type.EmptyTypes);
            
            // Try creating an object of that type.
            try
            {
                ctrl = (Control)info.Invoke(null);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Title);
                return;
            }

            // Try putting it in the grid.
            // If that doesn't work, it's probably a Window.
            try
            {
                grid.Children.Add(ctrl);
            }
            catch
            {
                if (ctrl is Window)
                    (ctrl as Window).Show();
                else
                    return;
            }
            Title = Title.Remove(Title.IndexOf('-')) + "- " + typ.Name;
        }
        // When Dump menu is opened, enable items.
        void DumpOnOpened(object sender, RoutedEventArgs args)
        {
            itemTemplate.IsEnabled = ctrl != null;
            itemItemsPanel.IsEnabled = ctrl != null && ctrl is ItemsControl;
        }
        // Dump Template object attached to ControlTemplate property.
        void DumpTemplateOnClick(object sender, RoutedEventArgs args)
        {
            if (ctrl != null)
                Dump(ctrl.Template);
        }
        // Dump ItemsPanelTemplate object attached to ItemsPanel property.
        void DumpItemsPanelOnClick(object sender, RoutedEventArgs args)
        {
            if (ctrl != null && ctrl is ItemsControl)
                Dump((ctrl as ItemsControl).ItemsPanel);
        }
        // Dump the template.
        void Dump(FrameworkTemplate template)
        {
            if (template != null)
            {
                // Dump the XAML into the TextBox.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = new string(' ', 4);
                settings.NewLineOnAttributes = true;

                StringBuilder strbuild = new StringBuilder();
                XmlWriter xmlwrite = XmlWriter.Create(strbuild, settings);

                try
                {
                    XamlWriter.Save(template, xmlwrite);
                    txtbox.Text = strbuild.ToString();
                }
                catch (Exception exc)
                {
                    txtbox.Text = exc.Message;
                }
            }
            else
                txtbox.Text = "no template";
        }
    }
}
