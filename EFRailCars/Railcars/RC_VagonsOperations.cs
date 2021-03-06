﻿using EFRailCars.Abstract;
using EFRailCars.Concrete;
using EFRailCars.Entities;
using Logs;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRailCars.Statics;
using System.Collections.Generic;

namespace EFRailCars.Railcars
{

    
    public class RC_VagonsOperations
    {
        IVagonsOperationsRepository rep_vo;
        private eventID eventID = eventID.EFRailCars_RailCars_RC_VagonsOperations;

        public RC_VagonsOperations() 
        {
            this.rep_vo = new EFVagonsOperationsRepository();
        }

        public RC_VagonsOperations(IVagonsOperationsRepository rep_vo) 
        {
            this.rep_vo = rep_vo;
        }
        /// <summary>
        /// Получить все вагоны
        /// </summary>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOperations()
        {
            try
            {
                return rep_vo.VAGON_OPERATIONS;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetVagonsOperations", eventID);
                return null;
            }
        }
        /// <summary>
        /// Вернуть операцию по id
        /// </summary>
        /// <param name="id_oper"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS GetVagonsOperations(int id_oper)
        {
            return rep_vo.VAGON_OPERATIONS.Where(o=>o.id_oper==id_oper).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному натурному листу и дате захода на АМКР
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOperationsToNatur(int natur, DateTime dt_amkr) 
        {
            return GetVagonsOperations().Where(o => o.n_natur == natur & o.dt_amkr == dt_amkr).OrderBy(o => o.num_vag_on_way);
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному id составу МТ и дате захода на АМКР
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <param name="dt_amkr"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOperationsToMTSostav(int id_sostav, DateTime dt_amkr) 
        {
            return GetVagonsOperations().Where(o => o.IDSostav == id_sostav & o.dt_amkr == dt_amkr).OrderBy(o => o.num_vag_on_way);
        }
        /// </summary>        
        /// Вернуть операцию по вагону по указаному натурному листу и дате захода на АМКР
        /// </summary>     
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="num_vagon"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS GetVagonsOperationsToNatur(int natur, DateTime dt_amkr, int num_vagon) 
        {
            return GetVagonsOperationsToNatur(natur, dt_amkr).Where(o => o.num_vagon == num_vagon).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному id составу МТ и дате захода на АМКР
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS GetVagonsOperationsToMTSostav(int id_sostav, DateTime dt_amkr, int id_vagon) 
        {
            return GetVagonsOperationsToMTSostav(id_sostav, dt_amkr).Where(o => o.id_vagon == id_vagon).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному номеру документа (копирование по прибытию)
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOperationsToDocInputSostav(int doc) 
        {
            return GetVagonsOperations().Where(o => o.id_ora_23_temp == doc);
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному номеру документа и вагона (копирование по прибытию)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="num_vag"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS GetVagonsOperationsToDocInputSostav(int doc, int num_vag) 
        {
            return GetVagonsOperationsToDocInputSostav(doc).Where(o => o.num_vagon == num_vag).OrderByDescending(o => o.id_oper).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному номеру документа (копирование по отправке)
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOperationsToDocOutputSostav(int doc) 
        {
            return GetVagonsOperations().Where(o => o.id_oracle == doc);
        }
        /// <summary>
        /// Вернуть операции над вагонами по указаному номеру документа и вагона (копирование по прибытию)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="num_vag"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS GetVagonsOperationsToDocOutputSostav(int doc, int num_vag) 
        {
            return GetVagonsOperationsToDocOutputSostav(doc).Where(o => o.num_vagon == num_vag).OrderByDescending(o => o.id_oper).FirstOrDefault();
        }
        /// <summary>
        /// Операция над вагоном с указаным натуральным листом и датой захода существует?
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="num_vagon"></param>
        /// <returns></returns>
        public bool IsVagonOperationKIS(int natur, DateTime dt_amkr, int num_vagon) 
        {
            VAGON_OPERATIONS vo = GetVagonsOperationsToNatur(natur, dt_amkr, num_vagon);
            if (vo != null) { return true; }
            return false;
        }
        /// <summary>
        /// Операция над вагоном с указаным id состава МТ и датой захода существует? 
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <returns></returns>
        public bool IsVagonOperationMT(int id_sostav, DateTime dt_amkr, int id_vagon) 
        {
            VAGON_OPERATIONS vo = GetVagonsOperationsToMTSostav(id_sostav, dt_amkr, id_vagon);
            if (vo != null) { return true; }
            return false;
        }
        /// <summary>
        /// Вернуть последний по порядку вагон на пути
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public int? MaxPositionWay(int id_way) 
        {
            return GetVagonsOperations().Where(o => o.id_way == id_way & o.is_present == 1).Max(o => o.num_vag_on_way);
        }
        /// <summary>
        /// Получить операции с вагонами имеющие id_sostav (вставка по данным металлургтранс)
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOperationsToInsertMT(int id_sostav) 
        {
            return GetVagonsOperations().Where(o => o.IDSostav == id_sostav);
        }
        /// <summary>
        /// Добавить удалить
        /// </summary>
        /// <param name="vagons"></param>
        /// <returns></returns>
        public int SaveVagonsOperations(VAGON_OPERATIONS vagonsoperations)
        {
            return rep_vo.SaveVAGONOPERATIONS(vagonsoperations);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_oper"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS DeleteVagonsOperations(int id_oper)
        {
            return rep_vo.DeleteVAGONOPERATIONS(id_oper);
        }
        /// <summary>
        /// Поставить вагон на станцию по данным КИС
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <param name="id_station"></param>
        /// <param name="id_way"></param>
        /// <param name="id_stat_kis"></param>
        /// <returns></returns>
        public int InsertVagon(int natur, DateTime dt_amkr, int id_vagon, int num_vagon, int? id_sostav, DateTime? dt_uz, int id_station, int id_way, int id_stat_kis, int? id_cond, int? id_cond2)
        {
            int? position = MaxPositionWay(id_way);
            if (position!=null) 
            {position++;} 
            else {position=1;}
            VAGON_OPERATIONS vo = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_uz = dt_uz,
                dt_amkr = dt_amkr,
                dt_out_amkr = null,
                n_natur = natur,
                id_vagon = id_vagon,
                id_stat = id_station,
                dt_from_stat = null,
                dt_on_stat = dt_amkr,
                id_way = id_way,
                dt_from_way = null,
                dt_on_way = dt_amkr,
                num_vag_on_way = position,
                is_present = 1,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = id_cond2,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = id_cond,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = id_sostav,
                num_vagon = num_vagon,
            };
            return SaveVagonsOperations(vo);
        }
        /// <summary>
        /// Поставить вагон в прибитие на станции АМКР из станций Кривого Рога
        /// </summary>
        /// <param name="IDSostav"></param>
        /// <param name="id_vagon"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_station_from"></param>
        /// <param name="position"></param>
        /// <param name="id_gruz"></param>
        /// <param name="weight_gruz"></param>
        /// <param name="id_station_in"></param>
        /// <param name="num_train"></param>
        /// <returns></returns>
        public int InsertVagon(int IDSostav, int id_vagon, int num_vagon, DateTime dt_uz_on, DateTime dt_uz_from, int id_station_from, int position ,int? id_gruz, decimal weight_gruz,int id_station_in, int num_train, int id_cond2, int way_from)
        {
            //TODO: !!ДОРАБОТАТЬ (ДОБАВИТЬ В ПРИБЫТИЕ С УЗ) - убрать id_vagon,id_gruz,weight_gruz (эти данные берутся из справочника САП входящие поставки по (dt_uz)dt_amkr и num_vagon)
            VAGON_OPERATIONS vo = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_uz = dt_uz_from,
                dt_amkr = null, // 
                dt_out_amkr = null,
                n_natur = null,
                id_vagon = id_vagon,
                id_stat = id_station_from,
                dt_from_stat = dt_uz_from,
                dt_on_stat = dt_uz_on,
                id_way = way_from,
                dt_from_way = dt_uz_from,
                dt_on_way = dt_uz_on,
                num_vag_on_way = position,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = id_cond2, // 15
                id_gruz = id_gruz,
                id_gruz_amkr = id_gruz,
                id_shop_gruz_for = null,
                weight_gruz = weight_gruz,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = null,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = id_station_in,
                st_lock_order = position,
                st_lock_train = num_train,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = IDSostav,
                num_vagon = num_vagon,
            };
            return SaveVagonsOperations(vo);
        }
        /// <summary>
        /// Поставить вагон на путь станции по данным КИС (копирование по внутреним станциям по прибытию)
        /// </summary>
        /// <param name="IDSostav"></param>
        /// <param name="doc"></param>
        /// <param name="natur"></param>
        /// <param name="id_vagon"></param>
        /// <param name="num_vagon"></param>
        /// <param name="dt_uz"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="dt_imp"></param>
        /// <param name="id_station_from"></param>
        /// <param name="position"></param>
        /// <param name="id_gruz"></param>
        /// <param name="note"></param>
        /// <param name="id_station_in"></param>
        /// <param name="num_train"></param>
        /// <param name="id_cond"></param>
        /// <returns></returns>
        public int InsertInputVagon(int IDSostav, int doc, int natur, int id_vagon, int num_vagon, DateTime dt_uz, DateTime dt_amkr, DateTime dt_imp, int id_station_from, int position, int? id_gruz, string note, int id_station_in, int num_train, int? id_cond, int? id_oper_parent, int? id_ways)
        {
            //TODO: !!ДОРАБОТАТЬ (ДОБАВИТЬ В ПРИБЫТИЕ С УЗ) - убрать id_vagon,id_gruz,weight_gruz (эти данные берутся из справочника САП входящие поставки по (dt_uz)dt_amkr и num_vagon)
            VAGON_OPERATIONS vo = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_uz = dt_uz,
                dt_amkr = dt_amkr, // 
                dt_out_amkr = null,
                n_natur = natur,
                id_vagon = id_vagon,
                id_stat = id_station_from,
                dt_from_stat = dt_imp,
                dt_on_stat = null,
                id_way = id_ways, //TODO: добавить путь
                dt_from_way = dt_imp,
                dt_on_way = null,
                num_vag_on_way = position,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = null, //id_cond2, // 15
                id_gruz = id_gruz,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = id_cond,
                note = note,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = id_station_in,
                st_lock_order = position,
                st_lock_train = num_train,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = id_oper_parent,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = doc,
                IDSostav = IDSostav,
                num_vagon = num_vagon,
            };
            return SaveVagonsOperations(vo);
        }
        /// <summary>
        /// Поставить вагон на путь станции по данным КИС (копирование по внутреним станциям по отправке)
        /// </summary>
        /// <param name="IDSostav"></param>
        /// <param name="doc"></param>
        /// <param name="natur"></param>
        /// <param name="id_vagon"></param>
        /// <param name="num_vagon"></param>
        /// <param name="dt_uz"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="dt_out"></param>
        /// <param name="id_station_from"></param>
        /// <param name="position"></param>
        /// <param name="id_gruz"></param>
        /// <param name="id_tupik"></param>
        /// <param name="id_nazn_country"></param>
        /// <param name="id_station_nazn"></param>
        /// <param name="id_station_in"></param>
        /// <param name="num_train"></param>
        /// <param name="id_cond"></param>
        /// <param name="note"></param>
        /// <param name="id_ways"></param>
        /// <returns></returns>
        public int InsertOutputVagon(int IDSostav, int doc, int natur, int id_vagon, int num_vagon, DateTime dt_uz, DateTime dt_amkr, DateTime dt_out, int id_station_from, 
            int position, int? id_gruz, int? id_tupik, int? id_nazn_country, int id_station_nazn, int id_station_in, int num_train, int? id_cond, string note,
            int? id_ways)
        {
            VAGON_OPERATIONS vo = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_uz = dt_uz,
                dt_amkr = dt_amkr, // 
                dt_out_amkr = null,
                n_natur = natur,
                id_vagon = id_vagon,
                id_stat = id_station_from,
                dt_from_stat = dt_out,
                dt_on_stat = null,
                id_way = id_ways,
                dt_from_way = dt_out,
                dt_on_way = null,
                num_vag_on_way = position,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = 14,
                id_gruz = id_gruz,
                id_gruz_amkr = id_gruz,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = id_tupik,
                id_nazn_country = id_nazn_country,
                id_gdstait = id_station_nazn,
                id_cond = id_cond,
                note = note,
                is_hist = 0,
                id_oracle = doc,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = id_station_in,
                st_lock_order = position,
                st_lock_train = num_train,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                IDSostav = IDSostav,
                num_vagon = num_vagon,
            };
            return SaveVagonsOperations(vo);
        }
        /// <summary>
        /// Обновить информацию по вагону поставленному на путь или принятому вручную.
        /// </summary>
        /// <param name="dt_amkr"></param>
        /// <param name="num_vagon"></param>
        /// <param name="natur"></param>
        /// <param name="idstation_amkr"></param>
        /// <param name="id_gruz"></param>
        /// <param name="id_shop"></param>
        /// <param name="id_cond"></param>
        /// <returns></returns>
        public int UpdateVagon(DateTime dt_amkr, int num_vagon, int natur, int[] idstation_amkr, int id_gruz, int id_shop, int? id_cond) 
        {
            try
            {
                string idstation_amkr_s = idstation_amkr.IntsToString(",");
                string sql = "update  dbo.VAGON_OPERATIONS " +
                                "set id_gruz = " + id_gruz.ToString() + ", id_gruz_amkr = " + id_gruz.ToString() + ", id_shop_gruz_for = " + id_shop.ToString() +
                                ", id_cond = " + (id_cond != null ? id_cond.ToString() : "null ") +
                                " where n_natur= " + natur.ToString() +
                                " and num_vagon= " + num_vagon.ToString() +
                                " and convert(smalldatetime,dt_amkr,120) ='" + dt_amkr.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " and id_stat in(" + idstation_amkr_s + ")";
                return rep_vo.db.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "UpdateVagon(1)", eventID);
                return -1;
            }   
        }
        /// <summary>
        /// Обновить информацию по вагону принятому в ручну на станции
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <param name="num_vagon"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_cond"></param>
        /// <param name="natur"></param>
        /// <returns></returns>
        public int UpdateVagon(int id_sostav , int num_vagon , int[] idstation_amkr, DateTime dt_amkr, int? id_cond, int natur)
        {
            try
            {
                string idstation_amkr_s = idstation_amkr.IntsToString(",");
                string sql = "update  dbo.VAGON_OPERATIONS " +
                                "set dt_amkr = Convert(datetime,'" + dt_amkr.ToString("yyyy-MM-dd HH:mm:ss") + "',120)" +
                                ", id_cond = " + (id_cond != null ? id_cond.ToString() : "null ") +
                                ", n_natur = " + natur.ToString() +
                                " where IDSostav = " + id_sostav.ToString() +
                                " and num_vagon= " + num_vagon.ToString() +
                                " and id_stat in(" + idstation_amkr_s + ")";
                return rep_vo.db.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "UpdateVagon(2)", eventID);
                return -1;
            }                    
        }

        /// <summary>
        /// Удалить вагоны пренадлежащие составу перенесеному по данным металлург транс 
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public int DeleteVagonsToInsertMT(int id_sostav)
        {
            try
            {
                return rep_vo.db.ExecuteSqlCommand("DELETE FROM dbo.VAGON_OPERATIONS WHERE IDSostav=" + id_sostav.ToString());
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteVagonsToInsertMT(1)", eventID);
                return -1;
            }
            
            
        }
        /// <summary>
        /// Удалить вагон пренадлежащий составу перенесеному по данным металлург транс 
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <param name="num_vag"></param>
        /// <returns></returns>
        public int DeleteVagonsToInsertMT(int id_sostav, int num_vag)
        {
            try
            {
                return rep_vo.db.ExecuteSqlCommand("DELETE FROM dbo.VAGON_OPERATIONS WHERE id_stat in (33,35) AND IDSostav = " + id_sostav.ToString() + " AND num_vagon = " + num_vag.ToString());
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteVagonsToInsertMT(2)", eventID);
                return -1;
            }
            
            
        }
        /// <summary>
        /// Удалить вагоны пренадлежащие натурному листу с датой
        /// </summary>
        /// <param name="natur_list"></param>
        /// <param name="dt_amkr"></param>
        /// <returns></returns>
        public int DeleteVagonsToNaturList(int natur_list, DateTime dt_amkr)
        {
            try
            {
                return rep_vo.db.ExecuteSqlCommand("DELETE FROM dbo.VAGON_OPERATIONS WHERE n_natur=" + natur_list.ToString() + " AND Convert(char(19),dt_amkr) ='" + dt_amkr.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteVagonsToNaturList", eventID);
                return -1;
            }
            
            
        }

        /// <summary>
        /// Удалить цепочку ранее поставленного вагона
        /// </summary>
        /// <param name="id_oper"></param>
        /// <param name="num"></param>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public int DeleteChainVagons(int id_oper, int num, int? id_sostav)
        {
            try
            {
                string sql = "DELETE FROM dbo.VAGON_OPERATIONS WHERE num_vagon=" + num.ToString() + " and id_oper>=" + id_oper.ToString() + " and IDSostav " + (id_sostav == null ? "is null" : "= " + id_sostav.ToString());
                return rep_vo.db.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("DeleteVagonsToDocOutput(id_oper:{0},num:{1},id_sostav:{2})",id_oper,num,id_sostav), eventID);
                return -1;
            }
        }
        /// <summary>
        /// Удалить вагоны пренадлежащие документу прибытия на станцию
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public int DeleteVagonsToDocInput(int doc)
        {
            //TODO: Прибить все вагоны которые перенесены по прибытию и отпраквлены далее
            int delete = 0;
            try
            {
                IQueryable<VAGON_OPERATIONS> list = GetVagonsOperationsToDocInputSostav(doc).OrderBy(o => o.num_vag_on_way);
                if (list == null) return 0;
                foreach (VAGON_OPERATIONS vag in list.ToList())
                {
                    int res = DeleteChainVagons(vag.id_oper, (int)vag.num_vagon, vag.IDSostav);
                    if (res > 0) delete++;
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("DeleteVagonsToDocInput(doc:{0})", doc), eventID);
                return -1;
            }
            return delete;
            //try
            //{
            //    return rep_vo.db.ExecuteSqlCommand("DELETE FROM dbo.VAGON_OPERATIONS WHERE id_ora_23_temp=" + doc.ToString());
            //}
            //catch (Exception e)
            //{
            //    LogRW.LogError(e, "DeleteVagonsToDocInput", eventID);
            //    return -1;
            //}
        }

        /// <summary>
        /// Удалить вагоны пренадлежащие документу по отправке на станцию
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public int DeleteVagonsToDocOutput(int doc)
        {
            int delete = 0;
            try
            {
                IQueryable<VAGON_OPERATIONS> list = GetVagonsOperationsToDocOutputSostav(doc).OrderBy(o => o.num_vag_on_way);
                if (list == null) return 0;
                foreach (VAGON_OPERATIONS vag in list.ToList())
                {
                    int res = DeleteChainVagons(vag.id_oper, (int)vag.num_vagon, vag.IDSostav);
                    if (res > 0) delete++;
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("DeleteVagonsToDocOutput(doc:{0})",doc), eventID);
                return -1;
            }
            return delete;
        }

        /// <summary>
        /// Очистить данные из прибытия по указанной станции до указанного времени
        /// </summary>
        /// <param name="id_station"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public int ClearArrivingWagons(int id_station, DateTime date)
        {
            try
            {
                SqlParameter id_st = new SqlParameter("@id_station", id_station);
                SqlParameter dt = new SqlParameter("@dt", date);
                return rep_vo.db.ExecuteSqlCommand("EXEC RailWay.ClearArrivingWagons @id_station, @dt", id_st, dt);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "ClearArrivingWagons", eventID);
                return -1;
            }
            
            
        }
        /// <summary>
        /// Вернуть вагон прибывающий из станций УЗ на станцию АМКР по Id состава и номера вагона
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <param name="num"></param>
        /// <param name="idstation_uz"></param>
        /// <param name="idstation"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS GetVagonsOfArrivalUZ(int id_mtsostav, int num, int[] idstation_uz, int idstation)
        {
            try
            {
                string station_uz_s = idstation_uz.IntsToString(",");
                string sql = "SELECT * FROM dbo.VAGON_OPERATIONS where [IDSostav]=" + id_mtsostav.ToString() + " and [num_vagon] = " + num.ToString() + " and [id_stat] in(" + station_uz_s + ") and [st_lock_id_stat] = " + idstation.ToString();
                return rep_vo.db.SqlQuery<VAGON_OPERATIONS>(sql).FirstOrDefault();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetVagonsOfArrivalUZ(1)", eventID);
                return null;
            }
        }
        /// <summary>
        /// Вернуть вагоны прибывающие из станций (int[] idstation_uz) по Id состава и номера вагона
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <param name="num"></param>
        /// <param name="idstation_uz"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOfArrival(int id_mtsostav, int num, int[] idstation_uz)
        {
            try
            {
                string station_uz_s = idstation_uz.IntsToString(",");
                string sql = "SELECT * FROM dbo.VAGON_OPERATIONS where [IDSostav]=" + id_mtsostav.ToString() + " and [num_vagon] = " + num.ToString() + " and [id_stat] in(" + station_uz_s + ") and [st_lock_id_stat] >0 ";
                return rep_vo.db.SqlQuery<VAGON_OPERATIONS>(sql).AsQueryable();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetVagonsOfArrival(1)", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить все вагоны находящиеся на станциях УЗ
        /// </summary>
        /// <param name="idstation_uz"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsOfArrival(int[] idstation_uz)
        {
            try
            {
                string station_uz_s = idstation_uz.IntsToString(",");
                string sql = "SELECT * FROM dbo.VAGON_OPERATIONS where [id_stat] in(" + station_uz_s + ")";
                return rep_vo.db.SqlQuery<VAGON_OPERATIONS>(sql).AsQueryable();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetVagonsOfArrival(2)", eventID);
                return null;
            }
        }
        /// <summary>
        /// Удалить записи вагонов прибывающие из станций (int[] idstation_uz) по Id состава и номера вагона
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <param name="num"></param>
        /// <param name="idstation_uz"></param>
        /// <returns></returns>
        public int DeleteVagonsOfArrival(int id_mtsostav, int num, int[] idstation_uz)
        {
            try
            {
                string station_uz_s = idstation_uz.IntsToString(",");
                string sql = "DELETE FROM dbo.VAGON_OPERATIONS where [IDSostav]=" + id_mtsostav.ToString() + " and [num_vagon] = " + num.ToString() + " and [id_stat] in(" + station_uz_s + ") and [st_lock_id_stat] >0 ";
                return rep_vo.db.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteVagonsOfArrival", eventID);
                return -1;
            }
        }
        /// <summary>
        /// Принять вагон на станцию АМКР из станции УЗ
        /// </summary>
        /// <param name="vagon"></param>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <returns></returns>
        public int TakeVagonOfUZ(VAGON_OPERATIONS vagon, int natur, DateTime dt_amkr, int id_stations, int id_ways, int id_cond)
        {
            int? position = MaxPositionWay(id_ways);
            if (position != null)
            { position++; }
            else { position = 1; }
            VAGON_OPERATIONS new_vagon = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_uz = vagon.dt_uz,
                dt_amkr = dt_amkr,
                dt_out_amkr = null,
                n_natur = natur,
                id_vagon = vagon.id_vagon,
                id_stat = id_stations,
                dt_from_stat = null,
                dt_on_stat = dt_amkr,
                id_way = id_ways,
                dt_from_way = null,
                dt_on_way = dt_amkr,
                num_vag_on_way = position,
                is_present = 1,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = 15,
                id_gruz = vagon.id_gruz,
                id_gruz_amkr = vagon.id_gruz_amkr,
                id_shop_gruz_for = null,
                weight_gruz = vagon.weight_gruz,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = id_cond,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                IDSostav = vagon.IDSostav,
                num_vagon = vagon.num_vagon
            };
            int res = SaveVagonsOperations(new_vagon);
            if (res > 0)
            {
                vagon.is_hist = 1;
                vagon.st_lock_id_stat = null;
                vagon.st_lock_order = null;
                vagon.st_lock_train = null;
                vagon.st_lock_side = null;
                vagon.st_lock_locom1 = null;
                vagon.st_lock_locom2 = null;
                vagon.n_natur = natur;
                SaveVagonsOperations(vagon);
            }
            return res;
        }
        /// <summary>
        /// Принять вагон на станцию АМКР из станции УЗ
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <param name="num"></param>
        /// <param name="idstation_uz"></param>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <returns></returns>
        public int TakeVagonOfUZ(int id_mtsostav, int num, int[] idstation_uz, int natur, DateTime dt_amkr, int id_stations, int id_ways, int id_cond)
        {
            int res = 0;
            VAGON_OPERATIONS vagon = GetVagonsOfArrivalUZ(id_mtsostav, num, idstation_uz, id_stations);
            if (vagon != null)
            {
                res  = TakeVagonOfUZ(vagon, natur, dt_amkr, id_stations, id_ways, id_cond); // Примем вагон на станцию АМКР
                DeleteVagonsOfArrival(id_mtsostav, num, idstation_uz);             // Удалим с прибытия вагоны кроме принятого
            }
            return res;
        }
        /// <summary>
        /// Принять вагон на станцию АМКР из станции УЗ ( проверка идет по всем станциям УЗ)
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <param name="num"></param>
        /// <param name="idstation_uz"></param>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <param name="id_cond"></param>
        /// <returns></returns>
        public int TakeVagonOfAllUZ(int id_mtsostav, int num, int[] idstation_uz, int natur, DateTime dt_amkr, int id_stations, int id_ways, int id_cond)
        {
            int res = 0;
            IQueryable<VAGON_OPERATIONS> vagons_uz = GetVagonsOfArrival(id_mtsostav, num, idstation_uz);
            if (vagons_uz.Count()>0)
            {
                res = TakeVagonOfUZ(vagons_uz.First(), natur, dt_amkr, id_stations, id_ways, id_cond); // Примем вагон на станцию АМКР
                DeleteVagonsOfArrival(id_mtsostav, num, idstation_uz);             // Удалим с прибытия вагоны кроме принятого
            }
            return res;
        }
        /// <summary>
        /// Получить вагоны на указаном пути
        /// </summary>
        /// <param name="way"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetWagonsOfWay(int way) 
        {
            return GetVagonsOperations().Where(o => o.id_way == way & o.is_present==1);
        }
        /// <summary>
        /// Получить вагоны на указаной станции
        /// </summary>
        /// <param name="id_stat"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetWagonsOfStation(int id_stat) 
        {
            return GetVagonsOperations().Where(o => o.id_stat == id_stat & o.is_present == 1);
        }
        /// <summary>
        /// смищение(выравнивание) вагонов на пути с начальным номером
        /// </summary>
        /// <param name="way"></param>
        /// <param name="start_num"></param>
        public int OffSetCars(int way, int start_num)
        {
            try
            {
                int result = 0;
                List<VAGON_OPERATIONS> list = new List<VAGON_OPERATIONS>();
                list = GetWagonsOfWay(way).Where(o => o.lock_id_way == null).OrderBy(o => o.num_vag_on_way).ToList();

                foreach (VAGON_OPERATIONS wag in list)
                {
                    if (wag.num_vag_on_way != start_num)
                    {
                        wag.num_vag_on_way = start_num;
                        int res = SaveVagonsOperations(wag);
                        if (res > 0) result++;
                        if (res < 0)
                        {
                            LogRW.LogError(String.Format("[OffSetCars]: Ошибка выравнивания позиции вагона №{0}, id_oper {1}", wag.num_vagon, wag.id_oper), eventID);
                        }
                    }
                    start_num++;
                }
                return result;
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[Maneuvers.OffSetCars]: Ошибка, источник: {0}, № {1}, описание:  {2}", e.Source, e.HResult, e.Message), this.eventID);
                return -1;
            }

        }
        /// <summary>
        /// Получить список вагонов отправленых на УЗ со станций АМКР
        /// </summary>
        /// <param name="idstation_uz"></param>
        /// <returns></returns>
        public IQueryable<VAGON_OPERATIONS> GetVagonsAMKRToUZ(int[] idstation_uz)
        {
            try
            {
                string station_uz_s = idstation_uz.IntsToString(",");
                string sql = "SELECT * FROM dbo.VAGON_OPERATIONS where [st_lock_id_stat] in(" + station_uz_s + ") and [is_present]=0 and [is_hist]=0";
                return rep_vo.db.SqlQuery<VAGON_OPERATIONS>(sql).AsQueryable();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetVagonsAMKRToUZ", eventID);
                return null;
            }
        }
    }
}
