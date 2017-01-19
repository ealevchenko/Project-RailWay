using EFWagons.Abstarct;
using EFWagons.Concrete;
using EFWagons.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Statics
{
    public class KometaContent
    {
        private eventID eventID = eventID.EFWagons_KIS_KometaContent;  
        
        IKometaVagonSobRepository rep_vsob;
        IKometaSobstvForNaklRepository rep_sfn;
        IKometaStranaRepository rep_str;

        public KometaContent() 
        {
            this.rep_vsob = new EFKometaVagonSobRepository();
            this.rep_sfn = new EFKometaSobstvForNaklRepository();
            this.rep_str = new EFKometaStranaRepository();
        }

        public KometaContent(IKometaVagonSobRepository rep_vsob, IKometaSobstvForNaklRepository rep_sfn, IKometaStranaRepository rep_str) 
        {
            this.rep_vsob = rep_vsob;
            this.rep_sfn = rep_sfn;
            this.rep_str = rep_str;
        }

        #region KometaVagonSob
        /// <summary>
        /// Получить все вагоны
        /// </summary>
        /// <returns></returns>
        public IQueryable<KometaVagonSob> GetVagonsSob()
        {
            try
            {
                return rep_vsob.KometaVagonSob;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetVagonsSob", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список вагонов по указаному номеру
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IQueryable<KometaVagonSob> GetVagonsSob(int num)
        {
            return GetVagonsSob().Where(c => c.N_VAGON == num).OrderByDescending(c => c.DATE_AR);
        }
        /// <summary>
        /// Получить список вагонов по указаному номеру с незаконченой арендой
        /// </summary>
        /// <param name="num"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public KometaVagonSob GetVagonsSob(int num, DateTime dt)
        {
            return GetVagonsSob(num).Where(v => v.DATE_AR <= dt & v.DATE_END == null).FirstOrDefault();
        }
        /// <summary>
        /// Получить список вагонов по которым поменяли владельца за указаную дату период до
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="day_period"></param>
        /// <returns></returns>
        public IQueryable<KometaVagonSob> GetChangeVagonsSob(DateTime dt, int day_period)
        {
            DateTime start_dt = dt.AddDays(-1 * day_period);
            return GetVagonsSob().Where(c => c.DATE_AR>=start_dt & c.DATE_AR<=dt).OrderBy(c => c.DATE_AR);
        }
        /// <summary>
        /// Получить список вагонов по которым поменяли владельца за указанный период
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="day_period"></param>
        /// <returns></returns>
        public IQueryable<KometaVagonSob> GetChangeVagonsSob(int day_period)
        {
            return GetChangeVagonsSob(DateTime.Now, day_period);
        }
        #endregion

        #region KometaSobstvForNakl
        /// <summary>
        /// Получить собственников по накладной
        /// </summary>
        /// <returns></returns>
        public IQueryable<KometaSobstvForNakl> GetSobstvForNakl()
        {
            return rep_sfn.KometaSobstvForNakl;
        }
        /// <summary>
        /// Получить собственников по накладной по коду
        /// </summary>
        /// <param name="kod_sob"></param>
        /// <returns></returns>
        public KometaSobstvForNakl GetSobstvForNakl(int kod_sob)
        {
            return GetSobstvForNakl().Where(s=>s.SOBSTV==kod_sob).FirstOrDefault();
        }
        #endregion

        #region KometaStrana
        /// <summary>
        /// Вернуть список стран
        /// </summary>
        /// <returns></returns>
        public IQueryable<KometaStrana> GetKometaStrana() 
        {
            return rep_str.KometaStrana;
        }
        public KometaStrana GetKometaStrana(int kod_stran) 
        {
            return GetKometaStrana().Where(s=>s.KOD_STRAN == kod_stran).FirstOrDefault();
        }
        #endregion

    }
}
