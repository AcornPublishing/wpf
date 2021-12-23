//----------------------------------------------------
// ToggleBoldAndItalic.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ToggleBoldAndItalic
{
    public class ToggleBoldAndItalic : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ToggleBoldAndItalic());
        }
        public ToggleBoldAndItalic()
        {
            Title = "Toggle Bold & Italic";

            TextBlock text = new TextBlock();
            text.FontSize = 32;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            Content = text;

            string strQuote = "To be, or not to be, that is the question";
            string[] strWords = strQuote.Split();

            foreach (string str in strWords)
            {
                Run run = new Run(str);
                run.MouseDown += RunOnMouseDown;
                text.Inlines.Add(run);
                text.Inlines.Add(" ");
            }
        }
        void RunOnMouseDown(object sender, MouseButtonEventArgs args)
        {
            Run run = sender as Run;

            if (args.ChangedButton == MouseButton.Left)
                run.FontStyle = run.FontStyle == FontStyles.Italic ? 
                    FontStyles.Normal : FontStyles.Italic;

            if (args.ChangedButton == MouseButton.Right)
                run.FontWeight = run.FontWeight == FontWeights.Bold ?
                    FontWeights.Normal : FontWeights.Bold;
        }
    }
}
