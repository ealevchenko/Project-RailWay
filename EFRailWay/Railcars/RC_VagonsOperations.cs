using EFRailWay.Abstract.Railcars;
using EFRailWay.Concrete.Railcars;
using EFRailWay.Entities.Railcars;
using Logs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Railcars
{
    public class RC_VagonsOperations
    {
        IVagonsOperationsRepository rep_vo;
        private eventID eventID = eventID.EFRailWay_RailCars_RC_VagonsOperations;

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
        /// <summary>
        /// Вернуть операцию по вагону по указаному натурному листу и дате захода на АМКР
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <returns></returns>
        //public VAGON_OPERATIONS GetVagonsOperationsToNatur(int natur, DateTime dt_amkr, int id_vagon) 
        //{
        //    return GetVagonsOperationsToNatur(natur, dt_amkr).Where(o => o.id_vagon == id_vagon).FirstOrDefault();
        //}
        /// <summary>
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
        /// Операция над вагоном с указаным натуральным листом и датой захода существует?
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <returns></returns>
        //public bool IsVagonOperationKIS(int natur, DateTime dt_amkr, int id_vagon) 
        //{ 
        //    VAGON_OPERATIONS vo = GetVagonsOperationsToNatur(natur,dt_amkr,id_vagon);
        //    if (vo != null) { return true; }
        //    return false;
        //}
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
        /// <param name="id_way"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS DeleteVagonsOperations(int id_oper)
        {
            return rep_vo.DeleteVAGONOPERATIONS(id_oper);
        }
        /// <summary>
        /// Поставить вагон на станцию
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <param name="id_station"></param>
        /// <param name="id_way"></param>
        /// <param name="id_stat_kis"></param>
        /// <returns></returns>
        public int InsertVagon(int natur, DateTime dt_amkr, int id_vagon, int num_vagon, int id_station, int id_way, int id_stat_kis)
        {
            int? position = MaxPositionWay(id_way);
            if (position!=null) 
            {position++;} 
            else {position=0;}
            VAGON_OPERATIONS vo = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_amkr = dt_amkr,
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
                id_cond2 = null,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
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
                IDSostav = null,
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
        public int InsertVagon(int IDSostav, int id_vagon, int num_vagon, DateTime dt_amkr, int id_station_from, int position ,int? id_gruz, decimal weight_gruz,int id_station_in, int num_train, int id_cond2)
        {
            VAGON_OPERATIONS vo = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_amkr = dt_amkr,
                n_natur = null,
                id_vagon = id_vagon,
                id_stat = id_station_from,
                dt_from_stat = dt_amkr,
                dt_on_stat = dt_amkr,
                id_way = null,
                dt_from_way = null,
                dt_on_way = null,
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
        /// Поставить вагон на станцию
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_vagon"></param>
        /// <param name="id_station"></param>
        /// <param name="id_way"></param>
        /// <param name="id_stat_kis"></param>
        /// <returns></returns>
        public int UpdateVagon(DateTime dt_amkr, int num_vagon, int id_way, int id_gruz, int id_shop, decimal? wes_gr, int? id_cond)
        {
            try
            {
                string sql = "update  dbo.VAGON_OPERATIONS " +
                                "set id_gruz = " + id_gruz.ToString() + ", id_gruz_amkr = " + id_gruz.ToString() + ", id_shop_gruz_for = " + id_shop.ToString() + ", weight_gruz = " + (wes_gr != null ? ((decimal)wes_gr).ToString("F", CultureInfo.CreateSpecificCulture("en-US")) : "null") +
                                ", id_cond = " + (id_cond !=null ? id_cond.ToString(): "null ") +
                                ", id_cond2 = 15 " +
                                " where id_way= " + id_way.ToString() +
                                " and num_vagon= " + num_vagon.ToString() +
                                " and Convert(char(19),dt_amkr) ='" + dt_amkr.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " and is_present = 1 " +
                                " and is_hist = 0 ";
                return rep_vo.db.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "UpdateVagon", eventID);
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
                LogRW.LogError(e, "DeleteVagonsToInsertMT", eventID);
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

    }
}
