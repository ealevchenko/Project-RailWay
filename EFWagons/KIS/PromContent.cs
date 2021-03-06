﻿using EFWagons.Abstarct;
using EFWagons.Concrete;
using EFWagons.Entities;
using EFWagons.Statics;
using Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.KIS
{

    
    /// <summary>
    /// Класс информации по станции Промышленная
    /// </summary>
    public class PromContent
    {

        private eventID eventID = eventID.EFWagons_KIS_PromContent;      
  
        IPromSostavRepository rep_ps;
        IPromNatHistRepository rep_pnh; 
        IPromVagonRepository rep_pv;
        IPromGruzSPRepository rep_gsp;
        IPromCexRepository rep_pcx;

        public PromContent() 
        {
            this.rep_ps = new EFPromSostavRepository();
            this.rep_pnh = new EFPromNatHistRepository();
            this.rep_pv = new EFPromVagonRepository();
            this.rep_gsp = new EFPromGruzSPRepository();
            this.rep_pcx = new EFPromCexRepository();
        }

        public PromContent(IPromSostavRepository rep_ps, IPromNatHistRepository rep_pnh, IPromVagonRepository rep_pv, IPromGruzSPRepository rep_gsp, IPromCexRepository rep_pcx) 
        {
            this.rep_ps = rep_ps;
            this.rep_pnh = rep_pnh;
            this.rep_pv = rep_pv;
            this.rep_gsp = rep_gsp;
            this.rep_pcx = rep_pcx;
        }

        #region PROM.SOSTAV
        /// <summary>
        /// Выбрать строки с указанием направления
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromSostav> GetPromSostav(bool direction)
        {
            try
            {
                string sql = "SELECT N_NATUR,D_DD,D_MM,D_YY,T_HH,T_MI,K_ST,N_PUT,NAPR,P_OT,V_P,K_ST_OTPR,K_ST_PR,N_VED_PR,N_SOST_OT,N_SOST_PR,DAT_VVOD FROM PROM.SOSTAV ";
                    if (direction) 
                    {
                        sql+="WHERE (P_OT = 1 and K_ST_PR is not null)";
                    } else {
                    
                    sql+="WHERE ( P_OT = 0 and K_ST_OTPR is not null)";
                    }

                return rep_ps.db.SqlQuery<PromSostav>(sql).AsQueryable();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetPromSostav(1)", eventID);
                return null;
            }
        }
        /// <summary>
        /// Выбрать строки с указанием направления и временного диапазона
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public IQueryable<PromSostav> GetPromSostav(DateTime start, DateTime stop, bool direction)
        {
            return GetPromSostav(direction).ToArray().FilterArrayOfFilterFromTo(Filters.IsGreaterOrequalToLessOrEqual, start, stop).AsQueryable();
        }

        /// <summary>
        /// Вернуть все прибывшие составы
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromSostav> GetArrivalPromSostav()
        {
            return rep_ps.PromSostav.Where(p => p.P_OT == 0 & p.K_ST_OTPR != null);
        }
        /// <summary>
        /// Вернуть все отправленные составы
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromSostav> GetDeparturePromSostav()
        {
            return rep_ps.PromSostav.Where(p => p.P_OT == 1 & p.K_ST_PR != null);
        }
        /// <summary>
        /// Вернуть составы прибывшие на станцию промышленую
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromSostav> GetInputPromSostav()
        {
            return rep_ps.PromSostav.Where(p => p.P_OT == 0 & p.V_P == 1 & p.K_ST != null);
        }
        /// <summary>
        /// Вернуть состав прибывший на станцию промышленую по натурке
        /// </summary>
        /// <param name="natur"></param>
        /// <returns></returns>
        public PromSostav GetInputPromSostavToNatur(int natur)
        {
            return GetInputPromSostav().Where( p => p.N_NATUR == natur).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть состав прибывший на станцию промышленую по натурке и дате
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public PromSostav GetArrivalPromSostavToNatur(int natur, int station, int day, int month, int year)
        {
            return GetInputPromSostav().Where( p => p.N_NATUR == natur & p.K_ST==station & p.D_DD==day & p.D_MM==month & p.D_YY==year).FirstOrDefault();
        }

        public PromSostav GetInputPromSostavToNatur(int natur, int station, int day, int month, int year)
        {
            return rep_ps.PromSostav.Where(p => p.N_NATUR == natur & p.K_ST == station & p.D_DD == day & p.D_MM == month & p.D_YY == year).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть все составы на станции промышленая за указанный период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<PromSostav> GetArrivalPromSostav(DateTime start, DateTime stop)
        {
            return GetArrivalPromSostav().ToArray().FilterArrayOfFilterFromTo(Filters.IsGreaterOrequalToLessOrEqual, start, stop).AsQueryable();
        }
        /// <summary>
        /// Вернуть все составы на станции промышленая за указанный период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<PromSostav> GetDeparturePromSostav(DateTime start, DateTime stop)
        {
            return GetDeparturePromSostav().ToArray().FilterArrayOfFilterFromTo(Filters.IsGreaterOrequalToLessOrEqual, start, stop).AsQueryable();
        }
        /// <summary>
        /// Вернуть составы прибывшие на станцию промышленую за указанный период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<PromSostav> GetInputPromSostav(DateTime start, DateTime stop)
        {
            return GetInputPromSostav().ToArray().FilterArrayOfFilterFromTo(Filters.IsGreaterOrequalToLessOrEqual, start, stop).AsQueryable();
        }
        /// <summary>
        /// Вернуть составы прибывшие на станцию промышленую за указанный период с сортировкой true - по убывания false - по возростанию
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IQueryable<PromSostav> GetInputPromSostav(DateTime start, DateTime stop, bool sort)
        {
            if (sort)
            {
                return GetInputPromSostav(start, stop)
                    .OrderByDescending(p => p.D_YY)
                    .ThenByDescending(p => p.D_MM)
                    .ThenByDescending(p => p.D_DD)
                    .ThenByDescending(p => p.T_HH)
                    .ThenByDescending(p => p.T_MI);
            }
            else { 
                return GetInputPromSostav(start, stop)
                    .OrderBy(p => p.D_YY)
                    .ThenBy(p => p.D_MM)
                    .ThenBy(p => p.D_DD)
                    .ThenBy(p => p.T_HH)
                    .ThenBy(p => p.T_MI);            
            }

        }

        #endregion

        #region PROM.Nat_Hist
        /// <summary>
        /// Получить список всех вагонов станции Промышленная
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromNatHist> GetNatHist() 
        {
            return rep_pnh.PromNatHist;
        }
        /// <summary>
        /// Получить список вагонов по натурному листу станции и дате поступления c сортировкой true- npp по убыванию false- npp по возрастанию
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQueryable<PromNatHist> GetNatHist(int natur, int station, int day, int month, int year, bool? sort) 
        {
            if (sort == null) 
            {
                return GetNatHist().Where(n => n.N_NATUR == natur & n.K_ST == station & n.D_PR_DD == day & n.D_PR_MM == month & n.D_PR_YY == year);
            }
            if ((bool)sort) 
            { 
                return GetNatHist().Where(n => n.N_NATUR == natur & n.K_ST == station & n.D_PR_DD == day & n.D_PR_MM == month & n.D_PR_YY == year).OrderByDescending(n => n.NPP);
            } 
            else 
            { 
                return GetNatHist().Where(n => n.N_NATUR == natur & n.K_ST == station & n.D_PR_DD == day & n.D_PR_MM == month & n.D_PR_YY == year).OrderBy(n => n.NPP);
            }
            

        }
        /// <summary>
        /// Получить список вагонов по натурному листу станции и дате поступления
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQueryable<PromNatHist> GetNatHist(int natur, int station, int day, int month, int year) 
        {
            return GetNatHist(natur, station, day, month, year, null);
        }
        /// <summary>
        /// Получить вагон по натурному листу станции и дате поступления
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="wag"></param>
        /// <returns></returns>
        public PromNatHist GetNatHist(int natur, int station, int day, int month, int year, int wag) 
        {
            return GetNatHist(natur, station, day, month, year, null).Where(h=>h.N_VAG==wag).FirstOrDefault();
        }
        /// <summary>
        /// Получить количество вагонов по натурному листу станции и дате поступления
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int? CountWagonsNatHist(int natur, int station, int day, int month, int year) 
        { 
            IQueryable<PromNatHist> pnh = GetNatHist(natur, station, day, month, year);
            if (pnh == null) return null;
            return pnh.Count();
        }
        /// <summary>
        /// Получить список вагонов по номеру вагона
        /// </summary>
        /// <param name="num_vag"></param>
        /// <returns></returns>
        public IQueryable<PromNatHist> GetNatHistOfVagon(int num_vag) 
        {
            return GetNatHist().Where(n => n.N_VAG == num_vag);
        }
        /// <summary>
        /// Получить список вагонов по номеру вагона и датой прибытия меньше указаной даты
        /// </summary>
        /// <param name="num_vag"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public IQueryable<PromNatHist> GetNatHistOfVagonLess(int num_vag, DateTime start)
        {
            return GetNatHistOfVagon(num_vag).ToArray().FilterArrayOfFilterFrom(Filters.IsLessOrEqual, start).AsQueryable();
        }
        /// <summary>
        /// Получить список вагонов по номеру вагона и датой прибытия меньше указаной даты с сортировкой
        /// </summary>
        /// <param name="num_vag"></param>
        /// <param name="start"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IQueryable<PromNatHist> GetNatHistOfVagonLess(int num_vag, DateTime start, bool sort)
        {
            if (sort)
            {
                return GetNatHistOfVagonLess(num_vag, start)
                    .OrderByDescending(p => p.D_PR_YY)
                    .ThenByDescending(p => p.D_PR_MM)
                    .ThenByDescending(p => p.D_PR_DD)
                    .ThenByDescending(p => p.T_PR_HH)
                    .ThenByDescending(p => p.T_PR_MI);
            }
            else
            {
                return GetNatHistOfVagonLess(num_vag, start)
                    .OrderBy(p => p.D_PR_YY)
                    .ThenBy(p => p.D_PR_MM)
                    .ThenBy(p => p.D_PR_DD)
                    .ThenBy(p => p.T_PR_HH)
                    .ThenBy(p => p.T_PR_MI);
            }

        }

        #endregion

        #region PROM.Vagon
        /// <summary>
        /// Получить список всех вагонов станции Промышленная
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromVagon> GetVagon() 
        {
            return rep_pv.PromVagon;
        }
        /// <summary>
        /// Получить список вагонов по натурному листу станции и дате поступления c сортировкой true- npp по убыванию false- npp по возрастанию
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQueryable<PromVagon> GetVagon(int natur, int station, int day, int month, int year, bool? sort) 
        {
            if (sort == null) 
            {
                return GetVagon().Where(n => n.N_NATUR == natur & n.K_ST == station & n.D_PR_DD == day & n.D_PR_MM == month & n.D_PR_YY == year);
            }
            if ((bool)sort) 
            {
                return GetVagon().Where(n => n.N_NATUR == natur & n.K_ST == station & n.D_PR_DD == day & n.D_PR_MM == month & n.D_PR_YY == year).OrderByDescending(n => n.NPP);
            } 
            else 
            {
                return GetVagon().Where(n => n.N_NATUR == natur & n.K_ST == station & n.D_PR_DD == day & n.D_PR_MM == month & n.D_PR_YY == year).OrderBy(n => n.NPP);
            }
            

        }
        /// <summary>
        /// Получить список вагонов по натурному листу станции и дате поступления
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public IQueryable<PromVagon> GetVagon(int natur, int station, int day, int month, int year) 
        {
            return GetVagon(natur, station, day, month, year, null);
        }
        /// <summary>
        /// Получить количество вагонов по натурному листу станции и дате поступления
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int? CountWagonsVagon(int natur, int station, int day, int month, int year) 
        {
            IQueryable<PromVagon> pv = GetVagon(natur, station, day, month, year);
            if (pv == null) return null;
            return pv.Count();
        }
        /// <summary>
        /// Получить информацию по вагону
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="station"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public PromVagon GetVagon(int natur, int station, int day, int month, int year, int num) 
        {
            return GetVagon(natur, station, day, month, year).Where(p=>p.N_VAG == num).FirstOrDefault();
        }
        #endregion

        #region PROM.GRUZ_SP
        /// <summary>
        /// Получить все грузы
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromGruzSP> GetGruzSP()
        {
            return rep_gsp.PromGruzSP;
        }
        /// <summary>
        /// Получить груз по коду груза
        /// </summary>
        /// <param name="k_gruz"></param>
        /// <returns></returns>
        public PromGruzSP GetGruzSP(int k_gruz) 
        {
            return GetGruzSP().Where(g => g.K_GRUZ == k_gruz).FirstOrDefault();
        }
        /// <summary>
        /// Получить код груза по коду ЕТСНГ (corect - false без коррекции кода ЕТСНГ, corect - true с коррекцией кода ЕТСНГ поиск по диапазону код0 ... код9)
        /// </summary>
        /// <param name="tar_gr"></param>
        /// <param name="corect"></param>
        /// <returns></returns>
        public PromGruzSP GetGruzSPToTarGR(int? tar_gr, bool corect) 
        {
            if (!corect)
            {
                return GetGruzSP().Where(g => g.TAR_GR == tar_gr).FirstOrDefault();
            }
            else { 
                return GetGruzSP().Where(g => g.TAR_GR >= tar_gr*10 & g.TAR_GR <= (tar_gr*10)+9).FirstOrDefault();
            }
        } 

        #endregion

        #region PROM.CEX
        /// <summary>
        /// Получить перечень всех цехов
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromCex> GetCex()
        {
            try
            {
                return rep_pcx.PromCex;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetCex", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить цех по id
        /// </summary>
        /// <param name="k_podr"></param>
        /// <returns></returns>
        public PromCex GetCex(int k_podr)
        {
            return GetCex().Where(c => c.K_PODR == k_podr).FirstOrDefault();
        }
        #endregion
    }
}
