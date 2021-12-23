//-------------------------------------------------------
// PeriodGroupDescription.cs (c) 2006 by Charles Petzold
//-------------------------------------------------------
using Petzold.SingleRecordDataEntry;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Petzold.ListBoxWithGroups
{
    public class PeriodGroupDescription : GroupDescription
    {
        public override object GroupNameFromItem(object item, int level, 
                                                 CultureInfo culture)
        {
            Person person = item as Person;

            if (person.BirthDate == null)
                return "Unknown";

            int year = ((DateTime)person.BirthDate).Year;
   
            if (year < 1575)
                return "Pre-Baroque";

            if (year < 1725)
                return "Baroque";

            if (year < 1795)
                return "Classical";

            if (year < 1870)
                return "Romantic";

            if (year < 1910)
                return "20th Century";

            return "Post-War";
        }
    }
}
