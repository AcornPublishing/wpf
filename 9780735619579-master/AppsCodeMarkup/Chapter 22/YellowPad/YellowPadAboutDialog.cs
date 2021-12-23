//-----------------------------------------------------
// YellowPadAboutDialog.cs (c) 2006 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Diagnostics;           // for Process class.
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Navigation;    // for RequestNavigateEventArgs.

namespace Petzold.YellowPad
{
    public partial class YellowPadAboutDialog
    {
        public YellowPadAboutDialog()
        {
            InitializeComponent();

            // Load copyright/signature Drawing and set in Image element.
            Uri uri = new Uri("pack://application:,,,/Images/Signature.xaml");
            Stream stream = Application.GetResourceStream(uri).Stream;
            Drawing drawing = (Drawing)XamlReader.Load(stream);
            stream.Close();

            imgSignature.Source = new DrawingImage(drawing);
        }
        // When hyperlink is clicked, go to my Web site.
        void LinkOnRequestNavigate(object sender, RequestNavigateEventArgs args)
        {
            Process.Start(args.Uri.OriginalString);
            args.Handled = true;
        }
    }
}
