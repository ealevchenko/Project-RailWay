using EFRailWay.Entities.KIS;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="field"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static List<Oracle_ArrivalSostav> SortField(this IEnumerable<Oracle_ArrivalSostav> source, string field, bool order)
        {
            switch (field) {
                case "DateTime": return order ? source.OrderByDescending(a => a.DateTime).ToList() : source.OrderBy(a => a.DateTime).ToList();
                case "NaturNum": return order ? source.OrderByDescending(a => a.NaturNum).ToList() : source.OrderBy(a => a.NaturNum).ToList();
                case "IDOrcStation": return order ? source.OrderByDescending(a => a.IDOrcStation).ToList() : source.OrderBy(a => a.IDOrcStation).ToList();
                case "Close": return order ? source.OrderByDescending(a => a.Close).ToList() : source.OrderBy(a => a.Close).ToList();
                case "Napr": return order ? source.OrderByDescending(a => a.Napr).ToList() : source.OrderBy(a => a.Napr).ToList();
                case "Status": return order ? source.OrderByDescending(a => a.Status).ToList() : source.OrderBy(a => a.Status).ToList();
                default: return order ? source.OrderByDescending(a => a.DateTime).ToList() : source.OrderBy(a => a.DateTime).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="field"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static List<Oracle_OutputSostav> SortField(this IEnumerable<Oracle_OutputSostav> source, string field, bool order)
        {
            switch (field) {
                case "DateTime": return order ? source.OrderByDescending(a => a.DateTime).ToList() : source.OrderBy(a => a.DateTime).ToList();
                case "DocNum": return order ? source.OrderByDescending(a => a.DocNum).ToList() : source.OrderBy(a => a.DocNum).ToList();
                case "IDOrcStationFrom": return order ? source.OrderByDescending(a => a.IDOrcStationFrom).ToList() : source.OrderBy(a => a.IDOrcStationFrom).ToList();
                case "IDOrcStationOn": return order ? source.OrderByDescending(a => a.IDOrcStationOn).ToList() : source.OrderBy(a => a.IDOrcStationOn).ToList();
                case "Close": return order ? source.OrderByDescending(a => a.Close).ToList() : source.OrderBy(a => a.Close).ToList();
                case "Status": return order ? source.OrderByDescending(a => a.Status).ToList() : source.OrderBy(a => a.Status).ToList();
                default: return order ? source.OrderByDescending(a => a.DateTime).ToList() : source.OrderBy(a => a.DateTime).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="field"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static List<Oracle_InputSostav> SortField(this IEnumerable<Oracle_InputSostav> source, string field, bool order)
        {
            switch (field) {
                case "DateTime": return order ? source.OrderByDescending(a => a.DateTime).ToList() : source.OrderBy(a => a.DateTime).ToList();
                case "DocNum": return order ? source.OrderByDescending(a => a.DocNum).ToList() : source.OrderBy(a => a.DocNum).ToList();
                case "IDOrcStationFrom": return order ? source.OrderByDescending(a => a.IDOrcStationFrom).ToList() : source.OrderBy(a => a.IDOrcStationFrom).ToList();
                case "IDOrcStationOn": return order ? source.OrderByDescending(a => a.IDOrcStationOn).ToList() : source.OrderBy(a => a.IDOrcStationOn).ToList();
                case "Close": return order ? source.OrderByDescending(a => a.Close).ToList() : source.OrderBy(a => a.Close).ToList();
                case "Status": return order ? source.OrderByDescending(a => a.Status).ToList() : source.OrderBy(a => a.Status).ToList();
                default: return order ? source.OrderByDescending(a => a.DateTime).ToList() : source.OrderBy(a => a.DateTime).ToList();
            }
        }
    }


}
