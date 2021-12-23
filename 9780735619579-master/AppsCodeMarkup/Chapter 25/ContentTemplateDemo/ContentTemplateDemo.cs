//--------------------------------------------------
// ContentTemplateDemo.cs (c) 2006 by Charles Petzold
//--------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.ContentTemplateDemo
{
    public partial class ContentTemplateDemo : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ContentTemplateDemo());
        }
        public ContentTemplateDemo()
        {
            InitializeComponent();
            
            // Add another EmployeeButton just to demonstrate the code.
            EmployeeButton btn = new EmployeeButton();
            btn.Content = new Employee("Jim", "Jim.png", 
                                       new DateTime(1975, 6, 15), false);
            stack.Children.Add(btn);
        }

        // Click event handler for button.
        void EmployeeButtonOnClick(object sender, RoutedEventArgs args)
        {
            Button btn = args.Source as Button;
            Employee emp = btn.Content as Employee;
            MessageBox.Show(emp.Name + " button clicked!", Title);
        }
    }
}
