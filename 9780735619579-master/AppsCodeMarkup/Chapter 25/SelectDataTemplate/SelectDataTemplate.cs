//---------------------------------------------------
// SelectDataTemplate.cs (c) 2006 by Charles Petzold
//---------------------------------------------------
using System;
using System.Windows;

namespace Petzold.SelectDataTemplate
{
    public partial class SelectDataTemplate : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectDataTemplate());
        }
        public SelectDataTemplate()
        {
            InitializeComponent();
        }
    }
}