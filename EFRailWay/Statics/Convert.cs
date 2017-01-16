using EFRailWay.KIS;
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

        public static string ConvertWhere (this String source, List<OracleRules> list, string name_field_on, string name_field_from, string log_oper)
        {
            if (String.IsNullOrWhiteSpace(name_field_on) | String.IsNullOrWhiteSpace(name_field_from) | String.IsNullOrWhiteSpace(log_oper) | list==null) return null; 
            string Where = "";
            foreach(OracleRules or in list)
            {
              Where+=" ("+name_field_on+"="+or.IDOn.ToString()+ " AND "+name_field_from+" IN("+or.IDFrom.IntsToString(",")+")) "+log_oper;
            }
            return Where.Remove(Where.Length - log_oper.Length);
        }

    }


}
