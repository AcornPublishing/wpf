//-------------------------------------------------
// PopupContextMenu.cs (c) 2006 by Charles Petzold
//-------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.PopupContextMenu
{
    public class PopupContextMenu : Window
    {
        ContextMenu menu;
        MenuItem itemBold, itemItalic;
        MenuItem[] itemDecor;
        Inline inlClicked;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PopupContextMenu());
        }
        public PopupContextMenu()
        {
            Title = "Popup Context Menu";

            // Create ContextMenu.
            menu = new ContextMenu();

            // Add an item for "Bold".
            itemBold = new MenuItem();
            itemBold.Header = "Bold";
            menu.Items.Add(itemBold);

            // Add an item for "Italic".
            itemItalic = new MenuItem();
            itemItalic.Header = "Italic";
            menu.Items.Add(itemItalic);

            // Get all the TextDecorationLocation members.
            TextDecorationLocation[] locs = 
                (TextDecorationLocation[]) 
                    Enum.GetValues(typeof(TextDecorationLocation));

            // Create an array of MenuItem objects and fill them up.
            itemDecor = new MenuItem[locs.Length];

            for (int i = 0; i < locs.Length; i++)
            {
                TextDecoration decor = new TextDecoration();
                decor.Location = locs[i];

                itemDecor[i] = new MenuItem();
                itemDecor[i].Header = locs[i].ToString();
                itemDecor[i].Tag = decor;
                menu.Items.Add(itemDecor[i]);
            }

            // Use one handler for the entire context menu.
            menu.AddHandler(MenuItem.ClickEvent, 
                            new RoutedEventHandler(MenuOnClick));

            // Create a TextBlock as content of the window.
            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            // Break a famous quotation up into words.
            string strQuote = "To be, or not to be, that is the question";
            string[] strWords = strQuote.Split();

            // Make each word a Run, and add to the TextBlock.
            foreach (string str in strWords)
            {
                Run run = new Run(str);

                // Make sure that TextDecorations is an actual collection!
                run.TextDecorations = new TextDecorationCollection();
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }
        }
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs args)
        {
            base.OnMouseRightButtonUp(args);

            if ((inlClicked = args.Source as Inline) != null)
            {
                // Check the menu items according to properties of the InLine.
                itemBold.IsChecked = (inlClicked.FontWeight == FontWeights.Bold);
                itemItalic.IsChecked = (inlClicked.FontStyle == FontStyles.Italic);

                foreach (MenuItem item in itemDecor)
                    item.IsChecked = (inlClicked.TextDecorations.Contains
                            (item.Tag as TextDecoration));

                // Display context menu.
                menu.IsOpen = true;
                args.Handled = true;
            }
        }
        void MenuOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = args.Source as MenuItem;

            item.IsChecked ^= true;

            // Change the Inline based on the checked or unchecked item.
            if (item == itemBold)
                inlClicked.FontWeight = 
                    (item.IsChecked ? FontWeights.Bold : FontWeights.Normal);

            else if (item == itemItalic)
                inlClicked.FontStyle = 
                    (item.IsChecked ? FontStyles.Italic : FontStyles.Normal);

            else
            {
                if (item.IsChecked)
                    inlClicked.TextDecorations.Add(item.Tag as TextDecoration);
                else
                    inlClicked.TextDecorations.Remove(item.Tag as TextDecoration);
            }
        }
    }
}
