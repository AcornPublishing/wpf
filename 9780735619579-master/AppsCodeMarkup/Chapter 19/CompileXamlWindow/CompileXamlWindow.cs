//--------------------------------------------------
// CompileXamlWindow.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.CompileXamlWindow
{
    public partial class CompileXamlWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CompileXamlWindow());
        }
        public CompileXamlWindow()
        {
            // Required method call to hook up event handlers and
            //  initialize fields.
            InitializeComponent();

            // Fill up the ListBox with brush names.
            foreach (PropertyInfo prop in typeof(Brushes).GetProperties())
                lstbox.Items.Add(prop.Name);
        }
        // Button event handler just displays MessageBox.
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = sender as Button;
            MessageBox.Show("The button labled '" + btn.Content + 
                            "' has been clicked.");
        }
        // ListBox event handler changes Fill property of Ellipse.
        void ListBoxOnSelection(object sender, SelectionChangedEventArgs args)
        {
            ListBox lstbox = sender as ListBox;
            string strItem = lstbox.SelectedItem as string;
            PropertyInfo prop = typeof(Brushes).GetProperty(strItem);
            elips.Fill = (Brush)prop.GetValue(null, null);
        }
    }
}
