//-------------------------------------------
// NamedColor.cs (c) 2006 by Charles Petzold
//-------------------------------------------
using System;
using System.Reflection;
using System.Windows.Media;

namespace Petzold.ListNamedColors
{
    class NamedColor
    {
        static NamedColor[] nclrs;
        Color clr;
        string str;

        // Static constructor.
        static NamedColor()
        {
            PropertyInfo[] props = typeof(Colors).GetProperties();
            nclrs = new NamedColor[props.Length];

            for (int i = 0; i < props.Length; i++)
                nclrs[i] = new NamedColor(props[i].Name, 
                                          (Color)props[i].GetValue(null, null));
        }

        // Private constructor.
        private NamedColor(string str, Color clr)
        {
            this.str = str;
            this.clr = clr;
        }

        // Static read-only property.
        public static NamedColor[] All
        {
            get { return nclrs; }
        }

        // Read-only properties.
        public Color Color
        {
            get { return clr; }
        }
        public string Name
        {
            get
            {
                string strSpaced = str[0].ToString();

                for (int i = 1; i < str.Length; i++)
                    strSpaced += (char.IsUpper(str[i]) ? " " : "") + 
                                            str[i].ToString();
                return strSpaced;
            }
        }

        // Override of ToString method.
        public override string ToString()
        {
            return str;
        }
    }
}
