using EFWagons.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Statics
{
    public static class Filters
    {
        public delegate bool FilterFromTo<T>(T ps, DateTime? start, DateTime? stop);
        public delegate bool FilterFrom<T>(T ps, DateTime? start);
        //public delegate bool FilterPromNatHist(PromNatHist pnh, DateTime? start, DateTime? stop);
        //public delegate bool FilterPromVagon(PromVagon pv, DateTime? start, DateTime? stop);

        public static T[] FilterArrayOfFilterFromTo<T>(this IEnumerable<T> source, FilterFromTo<T> filter, DateTime? start, DateTime? stop)
        {
            ArrayList aList = new ArrayList();
            foreach (T s in source)
            {
                if (filter(s, start, stop))
                {
                    aList.Add(s);
                }
            }
            return ((T[])aList.ToArray(typeof(T)));
        }

        public static T[] FilterArrayOfFilterFrom<T>(this IEnumerable<T> source, FilterFrom<T> filter, DateTime? start)
        {
            ArrayList aList = new ArrayList();
            foreach (T s in source)
            {
                if (filter(s, start))
                {
                    aList.Add(s);
                }
            }
            return ((T[])aList.ToArray(typeof(T)));
        }

        /// <summary>
        /// Больше или равно меньше или равно для PromSostav
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
        /// Больше или равно меньше или равно для PromNatHist
        /// </summary>
        /// <param name="pnh"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static bool IsGreaterOrequalToLessOrEqual(PromNatHist pnh, DateTime? start, DateTime? stop)
        {
            DateTime? DT = GetDateTime(pnh);
            if (DT != null & DT >= start & DT <= stop) { return true; }
            return false;
        }
        /// <summary>
        /// Больше или равно меньше или равно для PromVagon
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static bool IsGreaterOrequalToLessOrEqual(PromVagon pv, DateTime? start, DateTime? stop)
        {
            DateTime? DT = GetDateTime(pv);
            if (DT != null & DT >= start & DT <= stop) { return true; }
            return false;
        }

        public static bool IsLessOrEqual(PromNatHist pnh, DateTime? start)
        {
            DateTime? DT = GetDateTime(pnh);
            if (DT != null & DT <= start) { return true; }
            return false;
        }

        /// <summary>
        /// вернуть дату и время
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static DateTime? GetDateTime(PromSostav ps)
        {
            if (ps.D_DD != null & ps.D_MM != null & ps.D_YY != null & ps.T_HH != null & ps.T_MI != null)
            {
                return DateTime.Parse(ps.D_DD.ToString() + "-" + ps.D_MM.ToString() + "-" + ps.D_YY.ToString() + " " + ps.T_HH.ToString() + ":" + ps.T_MI.ToString() + ":00", CultureInfo.CreateSpecificCulture("ru-RU"));
            } return null;
        }

        public static DateTime? GetDateTime(PromNatHist pnh)
        {
            if (pnh.D_PR_DD != null & pnh.D_PR_MM != null & pnh.D_PR_YY != null & pnh.T_PR_HH != null & pnh.T_PR_MI != null)
            {
                return DateTime.Parse(pnh.D_PR_DD.ToString() + "-" + pnh.D_PR_MM.ToString() + "-" + pnh.D_PR_YY.ToString() + " " + pnh.T_PR_HH.ToString() + ":" + pnh.T_PR_MI.ToString() + ":00", CultureInfo.CreateSpecificCulture("ru-RU"));
            } return null;
        }

        public static DateTime? GetDateTime(PromVagon pv)
        {
            if (pv.D_PR_DD != null & pv.D_PR_MM != null & pv.D_PR_YY != null & pv.T_PR_HH != null & pv.T_PR_MI != null)
            {
                return DateTime.Parse(pv.D_PR_DD.ToString() + "-" + pv.D_PR_MM.ToString() + "-" + pv.D_PR_YY.ToString() + " " + pv.T_PR_HH.ToString() + ":" + pv.T_PR_MI.ToString() + ":00", CultureInfo.CreateSpecificCulture("ru-RU"));
            } return null;
        }

        //public static PromNatHist[] FilterArrayOfPromNatHist(this IEnumerable<PromNatHist> source, FilterPromNatHist filter, DateTime? start, DateTime? stop)
        //{
        //    ArrayList aList = new ArrayList();
        //    foreach (PromNatHist s in source)
        //    {
        //        if (filter(s, start, stop))
        //        {
        //            aList.Add(s);
        //        }
        //    }
        //    return ((PromNatHist[])aList.ToArray(typeof(PromNatHist)));
        //}

        //public static PromVagon[] FilterArrayOfPromVagon(this IEnumerable<PromVagon> source, FilterPromVagon filter, DateTime? start, DateTime? stop)
        //{
        //    ArrayList aList = new ArrayList();
        //    foreach (PromVagon s in source)
        //    {
        //        if (filter(s, start, stop))
        //        {
        //            aList.Add(s);
        //        }
        //    }
        //    return ((PromVagon[])aList.ToArray(typeof(PromVagon)));
        //}

    }
}
