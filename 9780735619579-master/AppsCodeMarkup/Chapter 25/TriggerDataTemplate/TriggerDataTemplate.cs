//----------------------------------------------------
// TriggerDataTemplate.cs (c) 2006 by Charles Petzold
//----------------------------------------------------
using System;
using System.Windows;

namespace Petzold.TriggerDataTemplate
{
    public partial class TriggerDataTemplate : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TriggerDataTemplate());
        }
        public TriggerDataTemplate()
        {
            InitializeComponent();
        }
    }
}