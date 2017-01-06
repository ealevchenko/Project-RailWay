using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Statics
{
    public static class ConvertRW
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return list.Contains(source);
        }

        public static string IntsToString (this IEnumerable<int> source, string sep)
        {
            string list = "";
            foreach (int i in source) 
            {
                list += i.ToString() + sep;
            }
            return list.Remove(list.Length - 1);
        }

    }


}
