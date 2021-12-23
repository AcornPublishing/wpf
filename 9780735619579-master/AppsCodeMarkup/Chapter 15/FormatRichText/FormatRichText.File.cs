//----------------------------------------------------
// FormatRichText.File.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    public partial class FormatRichText : Window
    {
        string[] formats = 
            {
                DataFormats.Xaml, DataFormats.XamlPackage, DataFormats.Rtf,
                DataFormats.Text, DataFormats.Text
            };

        string strFilter =
            "XAML Document Files (*.xaml)|*.xaml|" +
            "XAML Package Files (*.zip)|*.zip|" +
            "Rich Text Format Files (*.rtf)|*.rtf|" +
            "Text Files (*.txt)|*.txt|" +
            "All files (*.*)|*.*";
        
        void AddFileToolBar(ToolBarTray tray, int band, int index)
        {
            // Create the ToolBar.
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
                {
                    ApplicationCommands.New, ApplicationCommands.Open,
                    ApplicationCommands.Save
                };

            string[] strImages = 
                { 
                    "NewDocumentHS.png", "openHS.png", "saveHS.png"
                };

            // Create buttons for the ToolBar.
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button();
                btn.Command = comm[i];
                toolbar.Items.Add(btn);

                Image img = new Image();
                img.Source = new BitmapImage(
                        new Uri("pack://application:,,/Images/" + strImages[i]));
                img.Stretch = Stretch.None;
                btn.Content = img;

                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;
            }

            // Add the command bindings.
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.New, OnNew));
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Open, OnOpen));
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Save, OnSave));
        }

        // New: Set content to an empty string.
        void OnNew(object sender, ExecutedRoutedEventArgs args)
        {
            FlowDocument flow = txtbox.Document;
            TextRange range = new TextRange(flow.ContentStart,
                                            flow.ContentEnd);
            range.Text = "";
        }

        // Open: Display dialog box and load file.
        void OnOpen(object sender, ExecutedRoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, 
                                                flow.ContentEnd);
                FileStream strm = null; 

                try
                {
                    strm = new FileStream(dlg.FileName, FileMode.Open);
                    range.Load(strm, formats[dlg.FilterIndex - 1]);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
                finally
                {
                    if (strm != null)
                        strm.Close();
                }
            }
        }

        // Save: Display dialog box and save file.
        void OnSave(object sender, ExecutedRoutedEventArgs args)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = strFilter;

            if ((bool)dlg.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, 
                                                flow.ContentEnd);
                FileStream strm = null;

                try
                {
                    strm = new FileStream(dlg.FileName, FileMode.Create);
                    range.Save(strm, formats[dlg.FilterIndex - 1]);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
                finally
                {
                    if (strm != null)
                        strm.Close();
                }
            }
        }
    }
}
