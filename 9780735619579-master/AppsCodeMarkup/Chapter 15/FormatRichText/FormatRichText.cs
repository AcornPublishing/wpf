//-----------------------------------------------
// FormatRichText.cs (c) 2006 by Charles Petzold
//-----------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.FormatRichText
{
    public partial class FormatRichText : Window
    {
        RichTextBox txtbox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FormatRichText());
        }
        public FormatRichText()
        {
            Title = "Format Rich Text";

            // Create DockPanel as content of window.
            DockPanel dock = new DockPanel();
            Content = dock;

            // Create ToolBarTray docked at top of client area.
            ToolBarTray tray = new ToolBarTray();
            dock.Children.Add(tray);
            DockPanel.SetDock(tray, Dock.Top);

            // Create RichTextBox.
            txtbox = new RichTextBox();
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            // Call methods in other files.
            AddFileToolBar(tray, 0, 0);
            AddEditToolBar(tray, 1, 0);
            AddCharToolBar(tray, 2, 0);
            AddParaToolBar(tray, 2, 1);
            AddStatusBar(dock);

            // Fill rest of client area with RichTextBox and give it focus.
            dock.Children.Add(txtbox);
            txtbox.Focus();
        }
    }
}
