//---------------------------------------------------
// BuildButtonFactory.cs (c) 2006 by Charles Petzold
//---------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.BuildButtonFactory
{
    public class BuildButtonFactory : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BuildButtonFactory());
        }
        public BuildButtonFactory()
        {
            Title = "Build Button Factory";

            // Create a ControlTemplate intended for a Button object.
            ControlTemplate template = new ControlTemplate(typeof(Button));

            // Create a FrameworkElementFactory for the Border class.
            FrameworkElementFactory factoryBorder = 
                new FrameworkElementFactory(typeof(Border));

            // Give it a name to refer to it later.
            factoryBorder.Name = "border";

            // Set certain default properties.
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty, 
                                   new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, 
                                   SystemColors.ControlLightBrush);

            // Create a FrameworkElementFactory for the ContentPresenter class.
            FrameworkElementFactory factoryContent = 
                new FrameworkElementFactory(typeof(ContentPresenter));

            // Give it a name to refer to it later.
            factoryContent.Name = "content";

            // Bind some ContentPresenter properties to Button properties.
            factoryContent.SetValue(ContentPresenter.ContentProperty,
                new TemplateBindingExtension(Button.ContentProperty));

            // Notice that the button's Padding is the content's Margin!
            factoryContent.SetValue(ContentPresenter.MarginProperty, 
                new TemplateBindingExtension(Button.PaddingProperty));

            // Make the ContentPresenter a child of the Border.
            factoryBorder.AppendChild(factoryContent);

            // Make the Border the root element of the visual tree.
            template.VisualTree = factoryBorder;

            // Define a new Trigger when IsMouseOver is true.
            Trigger trig = new Trigger();
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;

            // Associate a Setter with that Trigger to change the
            //  CornerRadius property of the "border" element.
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
            set.TargetName = "border";

            // Add the Setter to the Setters collection of the Trigger.
            trig.Setters.Add(set);

            // Similarly, define a Setter to change the FontStyle.
            // (No TargetName is needed because it's the button's property.) 
            set = new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;

            // Add it to the same trigger's Setters collection as before.
            trig.Setters.Add(set);

            // Add the Trigger to the template.
            template.Triggers.Add(trig);

            // Similarly, define a Trigger for IsPressed.
            trig = new Trigger();
            trig.Property = Button.IsPressedProperty;
            trig.Value = true;

            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";

            // Add the Setter to the trigger's Setters collection.
            trig.Setters.Add(set);

            // Add the Trigger to the template.
            template.Triggers.Add(trig);

            // Finally, create a Button.
            Button btn = new Button();

            // Give it the template.
            btn.Template = template;

            // Define other properties normally.
            btn.Content = "Button with Custom Template";
            btn.Padding = new Thickness(20);
            btn.FontSize = 48;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;

            Content = btn;
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("You clicked the button", Title);
        } 
    }
}
