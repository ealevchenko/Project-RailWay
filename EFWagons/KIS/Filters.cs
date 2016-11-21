using EFWagons.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.KIS
{
    class Filters
    {
        public delegate bool IntFilter(PromSostav ps, DateTime? start, DateTime? stop);
        public static PromSostav[] FilterArrayOfPromSostav(PromSostav[] promsostavs, IntFilter filter, DateTime? start, DateTime? stop)
        {
            ArrayList aList = new ArrayList();
            foreach (PromSostav ps in promsostavs)
            {
                if (filter(ps, start, stop))
                {
                    aList.Add(ps);
                }
            }
            return ((PromSostav[])aList.ToArray(typeof(PromSostav)));
        }
        /// <summary>
        /// Больше или равно меньше или равно
        /// </summary>
        /// <param name="ps"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static bool IsGreaterOrequalToLessOrEqual(PromSostav ps, DateTime? start, DateTime? stop)
        {
            DateTime? DT = GetDateTime(ps);
            if (DT != null & DT >= start & DT <= stop) { return true; }
            return false;
        }
        /// <summary>
        /// вернуть дату и время
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        private static DateTime? GetDateTime(PromSostav ps)
        {
            if (ps.D_DD != null & ps.D_MM != null & ps.D_YY != null & ps.T_HH != null & ps.T_MI != null)
            {
                return DateTime.Parse(ps.D_DD.ToString() + "-" + ps.D_MM.ToString() + "-" + ps.D_YY.ToString() + " " + ps.T_HH.ToString() + ":" + ps.T_MI.ToString() + ":00", CultureInfo.CreateSpecificCulture("ru-RU"));
            } return null;
        }
    }
}
