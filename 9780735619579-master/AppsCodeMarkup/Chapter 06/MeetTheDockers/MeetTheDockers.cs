//-----------------------------------------------
// MeetTheDockers.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.MeetTheDockers
{
    public class MeetTheDockers : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MeetTheDockers());
        }
        public MeetTheDockers()
        {
            Title = "Meet the Dockers";

            DockPanel dock = new DockPanel();
            Content = dock;

            // Create menu.
            Menu menu = new Menu();
            MenuItem item = new MenuItem();
            item.Header = "Menu";
            menu.Items.Add(item);

            // Dock menu at top of panel.
            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(menu);

            // Create tool bar.
            ToolBar tool = new ToolBar();
            tool.Header = "Toolbar";

            // Dock tool bar at top of panel.
            DockPanel.SetDock(tool, Dock.Top);
            dock.Children.Add(tool);

            // Create status bar.
            StatusBar status = new StatusBar();
            StatusBarItem statitem = new StatusBarItem();
            statitem.Content = "Status";
            status.Items.Add(statitem);
            
            // Dock status bar at bottom of panel.
            DockPanel.SetDock(status, Dock.Bottom);
            dock.Children.Add(status);

            // Create list box.
            ListBox lstbox = new ListBox();
            lstbox.Items.Add("List Box Item");

            // Dock list box at left of panel.
            DockPanel.SetDock(lstbox, Dock.Left);
            dock.Children.Add(lstbox);

            // Create text box.
            TextBox txtbox = new TextBox();
            txtbox.AcceptsReturn = true;

            // Add text box to panel & give it input focus.
            dock.Children.Add(txtbox);
            txtbox.Focus();
        }
    }
}
