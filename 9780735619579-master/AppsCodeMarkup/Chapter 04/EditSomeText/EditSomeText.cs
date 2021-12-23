//---------------------------------------------
// EditSomeText.cs (c) 2006 by Charles Petzold
//---------------------------------------------
using System;
using System.ComponentModel;        // for CancelEventArgs
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.EditSomeText
{
    class EditSomeText : Window
    {
        static string strFileName = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
                    "Petzold\\EditSomeText\\EditSomeText.txt");

        TextBox txtbox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }
        public EditSomeText()
        {
            Title = "Edit Some Text";

            // Create the text box.
            txtbox = new TextBox();
            txtbox.AcceptsReturn = true;
            txtbox.TextWrapping = TextWrapping.Wrap;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.KeyDown += TextBoxOnKeyDown;
            Content = txtbox;

            // Load the text file.
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch
            {
            }

            // Set the text box caret and input focus.
            txtbox.CaretIndex = txtbox.Text.Length;
            txtbox.Focus();
        }
        protected override void OnClosing(CancelEventArgs args)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strFileName));
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch (Exception exc)
            {
                MessageBoxResult result = 
                    MessageBox.Show("File could not be saved: " + exc.Message + 
                                    "\nClose program anyway?", Title, 
                                    MessageBoxButton.YesNo, 
                                    MessageBoxImage.Exclamation);

                args.Cancel = (result == MessageBoxResult.No);
            }
        }
        void TextBoxOnKeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.F5)
            {
                txtbox.SelectedText = DateTime.Now.ToString();
                txtbox.CaretIndex = txtbox.SelectionStart + 
                                                txtbox.SelectionLength;
            }
        }
    }
}
