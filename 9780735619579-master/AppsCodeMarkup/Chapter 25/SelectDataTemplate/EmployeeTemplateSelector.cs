//---------------------------------------------------------
// EmployeeTemplateSelector.cs (c) 2006 by Charles Petzold
//---------------------------------------------------------
using Petzold.ContentTemplateDemo;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SelectDataTemplate
{
    public class EmployeeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, 
                                                    DependencyObject container)
        {
            Employee emp = item as Employee;
            FrameworkElement el = container as FrameworkElement;

            return (DataTemplate) el.FindResource(
                emp.LeftHanded ? "templateLeft" : "templateRight");
        }
    }
}
