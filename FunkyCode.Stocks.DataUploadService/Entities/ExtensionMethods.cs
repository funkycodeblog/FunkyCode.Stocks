using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace DataObj
{
    public static class ExtensionMethods
    {

        public static List<T> GetRangeLast<T>(this List<T> elements, int count)
        {
            int numberOfElements = elements.Count;
            return elements.GetRange(numberOfElements - count, count);
        }

        public static List<T> GetRangeFirst<T>(this List<T> elements, int count)
        {
            int numberOfElements = elements.Count;
            return elements.GetRange(0, count);
        }

        public static double ToDouble(this string str)
        {

            string str0 = str.Replace(" ", "");
            char sep = str.FirstOrDefault(c => c == ',' || c == '.');
            if (sep != 0)
            {
                string sysSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                str0 = str0.Replace(sep.ToString(), sysSep);
            }

            return double.Parse(str0);


        }

        public static int ToInt(this string str)
        {
            return int.Parse(str);

        }

        



    }
}
