//---------------------------------------------------
// AccessStaticFields.cs (c) 2006 by Charles Petzold
//---------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.AccessStaticFields
{
    public partial class AccessStaticFields : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new AccessStaticFields());
        }
        public AccessStaticFields()
        {
            InitializeComponent();
        }
    }
}