//-----------------------------------------
// Employee.cs (c) 2006 by Charles Petzold
//-----------------------------------------
using System;

namespace Petzold.ContentTemplateDemo
{
    public class Employee
    {
        // Private fields.
        string name;
        string face;
        DateTime birthdate;
        bool lefthanded;

        // Parameterless constructor used in XAML.
        public Employee()
        {
        }
        // Constructor with parameter used in C# code.
        public Employee(string name, string face, 
                        DateTime birthdate, bool lefthanded)
        {
            Name = name;
            Face = face;
            BirthDate = birthdate;
            LeftHanded = lefthanded;
        }

        // Public properties.
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        public string Face
        {
            set { face = value; }
            get { return face; }
        }
        public DateTime BirthDate
        {
            set { birthdate = value; }
            get { return birthdate; }
        }
        public bool LeftHanded
        {
            set { lefthanded = value; }
            get { return lefthanded; }
        }
    }
}
