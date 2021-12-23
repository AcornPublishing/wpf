//--------------------------------------------
// SpaceButton.cs (c) 2006 by Charles Petzold
//--------------------------------------------
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.SetSpaceProperty
{
    public class SpaceButton : Button
    {
        // A traditional .NET private field and public property.
        string txt;

        public string Text
        {
            set
            {
                txt = value;
                Content = SpaceOutText(txt);
            }
            get
            {
                return txt;
            }
        }

        // A DependencyProperty and public property.
        public static readonly DependencyProperty SpaceProperty;

        public int Space
        {
            set
            {
                SetValue(SpaceProperty, value);
            }
            get
            {
                return (int)GetValue(SpaceProperty);
            }
        }

        // Static constructor.
        static SpaceButton()
        {
            // Define the metadata.
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.DefaultValue = 1;
            metadata.AffectsMeasure = true;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropertyChanged;

            // Register the DependencyProperty.
            SpaceProperty = 
                DependencyProperty.Register("Space", typeof(int),
                                            typeof(SpaceButton), metadata, 
                                            ValidateSpaceValue);
        }

        // Callback method for value validation.
        static bool ValidateSpaceValue(object obj)
        {
            int i = (int)obj;
            return i >= 0;
        }

        // Callback method for property changed.
        static void OnSpacePropertyChanged(DependencyObject obj, 
                                    DependencyPropertyChangedEventArgs args)
        {
            SpaceButton btn = obj as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);
        }

        // Method to insert spaces in the text.
        string SpaceOutText(string str)
        {
            if (str == null)
                return null;

            StringBuilder build = new StringBuilder();

            foreach (char ch in str)
                build.Append(ch + new string(' ', Space));

            return build.ToString();
        }
    }
}
