//---------------------------------------------
// XamlCruncher.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using System;
using System.IO;                            // for StringReader
using System.Text;                          // for StringBuilder
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;   // for StatusBarItem
using System.Windows.Input;
using System.Windows.Markup;                // for XamlReader.Load
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;     // for DispatcherUnhandledExceptionEventArgs
using System.Xml;                           // for XmlTextReader

namespace Petzold.XamlCruncher
{
    class XamlCruncher : Petzold.NotepadClone.NotepadClone
    {
        Frame frameParent;          // To display object created by XAML.
        Window win;                 // Window created from XAML.
        StatusBarItem statusParse;  // Displays parsing error or OK.
        int tabspaces = 4;          // When Tab key pressed.

        // Loaded settings.
        XamlCruncherSettings settingsXaml;

        // Menu maintenance.
        XamlOrientationMenuItem itemOrientation;
        bool isSuspendParsing = false;

        [STAThread]
        public new static void Main()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose;
            app.Run(new XamlCruncher());
        }
        // Public property for menu item to suspend parsing.
        public bool IsSuspendParsing
        {
            set { isSuspendParsing = value; }
            get { return isSuspendParsing; }
        }
        // Constructor.
        public XamlCruncher()
        {
            // New filter for File Open and Save dialog boxes.
            strFilter = "XAML Files(*.xaml)|*.xaml|All Files(*.*)|*.*";

            // Find the DockPanel and remove the TextBox from it.
            DockPanel dock = txtbox.Parent as DockPanel;
            dock.Children.Remove(txtbox);

            // Create a Grid with three rows and columns, all 0 pixels.
            Grid grid = new Grid();
            dock.Children.Add(grid);

            for (int i = 0; i < 3; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = new GridLength(0);
                grid.RowDefinitions.Add(rowdef);

                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = new GridLength(0);
                grid.ColumnDefinitions.Add(coldef);
            }

            // Initialize the first row and column to 100*.
            grid.RowDefinitions[0].Height = 
                        new GridLength(100, GridUnitType.Star);
            grid.ColumnDefinitions[0].Width = 
                        new GridLength(100, GridUnitType.Star);

            // Add two GridSplitter controls to the Grid.
            GridSplitter split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Stretch;
            split.VerticalAlignment = VerticalAlignment.Center;
            split.Height = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 1);
            Grid.SetColumn(split, 0);
            Grid.SetColumnSpan(split, 3);

            split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetRow(split, 0);
            Grid.SetColumn(split, 1);
            Grid.SetRowSpan(split, 3);

            // Create a Frame for displaying XAML object.
            frameParent = new Frame();
            frameParent.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            grid.Children.Add(frameParent);

            // Put the TextBox in the Grid.
            txtbox.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtbox);

            // Case the loaded settings to XamlCruncherSettings.
            settingsXaml = (XamlCruncherSettings)settings;

            // Insert "Xaml" item on top-level menu.
            MenuItem itemXaml = new MenuItem();
            itemXaml.Header = "_Xaml";
            menu.Items.Insert(menu.Items.Count - 1, itemXaml);

            // Create XamlOrientationMenuItem & add to menu.
            itemOrientation =
                new XamlOrientationMenuItem(grid, txtbox, frameParent);
            itemOrientation.Orientation = settingsXaml.Orientation;
            itemXaml.Items.Add(itemOrientation);

            // Menu item to set tab spaces.
            MenuItem itemTabs = new MenuItem();
            itemTabs.Header = "_Tab Spaces...";
            itemTabs.Click += TabSpacesOnClick;
            itemXaml.Items.Add(itemTabs);

            // Menu item to suppress parsing.
            MenuItem itemNoParse = new MenuItem();
            itemNoParse.Header = "_Suspend Parsing";
            itemNoParse.IsCheckable = true;
            itemNoParse.SetBinding(MenuItem.IsCheckedProperty,
                                        "IsSuspendParsing");
            itemNoParse.DataContext = this;
            itemXaml.Items.Add(itemNoParse);

            // Command to reparse.
            InputGestureCollection collGest = new InputGestureCollection();
            collGest.Add(new KeyGesture(Key.F6));
            RoutedUICommand commReparse =
                new RoutedUICommand("_Reparse", "Reparse",
                                    GetType(), collGest);

            // Menu item to reparse.
            MenuItem itemReparse = new MenuItem();
            itemReparse.Command = commReparse;
            itemXaml.Items.Add(itemReparse);

            // Command binding to reparse.
            CommandBindings.Add(new CommandBinding(commReparse,
                                ReparseOnExecuted));

            // Command to show window.
            collGest = new InputGestureCollection();
            collGest.Add(new KeyGesture(Key.F7));
            RoutedUICommand commShowWin = 
                new RoutedUICommand("Show _Window", "ShowWindow",
                                    GetType(), collGest);

            // Menu item to show window.
            MenuItem itemShowWin = new MenuItem();
            itemShowWin.Command = commShowWin;
            itemXaml.Items.Add(itemShowWin);

            // Command binding to show window.
            CommandBindings.Add(new CommandBinding(commShowWin,
                            ShowWindowOnExecuted, ShowWindowCanExecute));

            // Menu item to save as new startup document.
            MenuItem itemTemplate = new MenuItem();
            itemTemplate.Header = "Save as Startup _Document";
            itemTemplate.Click += NewStartupOnClick;
            itemXaml.Items.Add(itemTemplate);

            // Insert Help on Help menu.
            MenuItem itemXamlHelp = new MenuItem();
            itemXamlHelp.Header = "_Help...";
            itemXamlHelp.Click += HelpOnClick;
            MenuItem itemHelp = (MenuItem)menu.Items[menu.Items.Count - 1];
            itemHelp.Items.Insert(0, itemXamlHelp);

            // New StatusBar item.
            statusParse = new StatusBarItem();
            status.Items.Insert(0, statusParse);
            status.Visibility = Visibility.Visible;

            // Install handler for unhandled exception.
            // Comment out this code when experimenting with new features
            //   or changes to the program!
            Dispatcher.UnhandledException += DispatcherOnUnhandledException;
        }
        // Override of NewOnExecute handler puts StartupDocument in TextBox.
        protected override void NewOnExecute(object sender, 
                                             ExecutedRoutedEventArgs args)
        {
            base.NewOnExecute(sender, args);

            string str = ((XamlCruncherSettings)settings).StartupDocument;

            // Make sure the next Replace doesn't add too much.
            str = str.Replace("\r\n", "\n");

            // Replace line feeds with carriage return/line feeds.
            str = str.Replace("\n", "\r\n"); 
            txtbox.Text = str;
            isFileDirty = false;
        }
        // Override of LoadSettings loads XamlCruncherSettings.
        protected override object LoadSettings()
        {
            return XamlCruncherSettings.Load(typeof(XamlCruncherSettings), 
                                           strAppData);
        }
        // Override of OnClosed saves Orientation from menu item.
        protected override void OnClosed(EventArgs args)
        {
            settingsXaml.Orientation = itemOrientation.Orientation; 
            base.OnClosed(args);
        }
        // Override of SaveSettings saves XamlCruncherSettings object.
        protected override void SaveSettings()
        {
            ((XamlCruncherSettings)settings).Save(strAppData);
        }
        // Handler for Tab Spaces menu item. 
        void TabSpacesOnClick(object sender, RoutedEventArgs args)
        {
            XamlTabSpacesDialog dlg = new XamlTabSpacesDialog();
            dlg.Owner = this;
            dlg.TabSpaces = settingsXaml.TabSpaces;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                settingsXaml.TabSpaces = dlg.TabSpaces;
            }
        }
        // Handler for Reparse menu item.
        void ReparseOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            Parse();
        }

        // Handlers for Show Window menu item.
        void ShowWindowCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = (win != null);
        }
        void ShowWindowOnExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            if (win != null)
                win.Show();
        }
        // Handler for Save as New Startup Document menu item.
        void NewStartupOnClick(object sender, RoutedEventArgs args)
        {
            ((XamlCruncherSettings)settings).StartupDocument = txtbox.Text;
        }
        // Help menu item.
        void HelpOnClick(object sender, RoutedEventArgs args)
        {
            Uri uri = new Uri("pack://application:,,,/XamlCruncherHelp.xaml");
            Stream stream = Application.GetResourceStream(uri).Stream;

            Window win = new Window();
            win.Title = "XAML Cruncher Help";
            win.Content = XamlReader.Load(stream);
            win.Show();
        }
        // OnPreviewKeyDown substitutes spaces for Tab key.
        protected override void OnPreviewKeyDown(KeyEventArgs args)
        {
            base.OnPreviewKeyDown(args);

            if (args.Source == txtbox && args.Key == Key.Tab)
            {
                string strInsert = new string(' ', tabspaces);
                int iChar = txtbox.SelectionStart;
                int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar);

                if (iLine != -1)
                {
                    int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine);
                    strInsert = new string(' ', 
                        settingsXaml.TabSpaces - iCol % settingsXaml.TabSpaces);
                }

                txtbox.SelectedText = strInsert;
                txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
                args.Handled = true;
            }
        }
        // TextBoxOnTextChanged attempts to parse XAML.
        void TextBoxOnTextChanged(object sender, TextChangedEventArgs args)
        {
            if (IsSuspendParsing)
                txtbox.Foreground = SystemColors.WindowTextBrush;
            else
                Parse();
        }

        // General Parse method also called for Reparse menu item.
        void Parse()
        {
            StringReader strreader = new StringReader(txtbox.Text);
            XmlTextReader xmlreader = new XmlTextReader(strreader);

            try
            {
                object obj = XamlReader.Load(xmlreader);
                txtbox.Foreground = SystemColors.WindowTextBrush;

                if (obj is Window)
                {
                    win = obj as Window;
                    statusParse.Content = "Press F7 to display Window";
                }
                else
                {
                    win = null;
                    frameParent.Content = obj; 
                    statusParse.Content = "OK";
                }
            }
            catch (Exception exc)
            {
                txtbox.Foreground = Brushes.Red;
                statusParse.Content = exc.Message;
            }
        }
        // UnhandledException handler required if XAML object throws exception.
        void DispatcherOnUnhandledException(object sender, 
                                    DispatcherUnhandledExceptionEventArgs args)
        {
            statusParse.Content = "Unhandled Exception: " + args.Exception.Message;
            args.Handled = true;
        }
    }
}
