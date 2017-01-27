using EFRailCars.Abstract;
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
        public int InsertVagon(int natur, DateTime dt_amkr, int id_vagon, int num_vagon, int? id_sostav, DateTime? dt_uz, int id_station, int id_way, int id_stat_kis)
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

        public int InsertInputVagon(int IDSostav, int doc, int natur, int id_vagon, int num_vagon, DateTime dt_uz, DateTime dt_amkr, DateTime dt_imp, int id_station_from, int position, int? id_gruz, string note,  int id_station_in, int num_train, int? id_cond)
        {
        //(id_vagon,  id_stat,            dt_from_stat,   id_gruz,    id_cond,            note,                   is_present, is_hist,    id_ora_23_temp,     st_lock_id_stat,        st_lock_order,          st_lock_train,  dt_amkr,                dt_on_stat,             dt_on_way,          num_vagon)
        //(@id_vagon, @id_sender_station, @DateTime,      @id_gruz,   @cursetwag_godn,    @cursetwag_rem_in_st,   0,          0,          @DocNum,            @id_receiver_station,   @cursetwag_n_in_st,     -1,             @cursetwag_dt_amkr,     @cursetwag_dt_amkr,     @cursetwag_dt_amkr, @cursetwag_n_vag)
            
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
                id_way = null,
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
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = doc,
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
            //TODO: !! ДОРАБОТАТЬ (ОБНОВЛЕНИЕ ВАГОНОВ ПО КИСУ - UpdateVagon) обновлять готовность по прибытию и дату зачисления на АМКР
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
        /// Удалить вагоны пренадлежащие документу прибытия на станцию
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public int DeleteVagonsToDocInput(int doc)
        {
            try
            {
                return rep_vo.db.ExecuteSqlCommand("DELETE FROM dbo.VAGON_OPERATIONS WHERE id_ora_23_temp=" + doc.ToString());
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteVagonsToDocInput", eventID);
                return -1;
            }
            
            
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
        public int TakeVagonOfUZ(VAGON_OPERATIONS vagon, int natur, DateTime dt_amkr, int id_stations, int id_ways)
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
        public int TakeVagonOfUZ(int id_mtsostav, int num, int[] idstation_uz, int natur, DateTime dt_amkr, int id_stations, int id_ways)
        {
            int res = 0;
            VAGON_OPERATIONS vagon = GetVagonsOfArrivalUZ(id_mtsostav, num, idstation_uz, id_stations);
            if (vagon != null)
            {
                res  = TakeVagonOfUZ(vagon, natur, dt_amkr, id_stations, id_ways); // Примем вагон на станцию АМКР
                DeleteVagonsOfArrival(id_mtsostav, num, idstation_uz);             // Удалим с прибытия вагоны кроме принятого
            }
            return res;
        }

        public int TakeVagonOfAllUZ(int id_mtsostav, int num, int[] idstation_uz, int natur, DateTime dt_amkr, int id_stations, int id_ways)
        {
            int res = 0;
            IQueryable<VAGON_OPERATIONS> vagons_uz = GetVagonsOfArrival(id_mtsostav, num, idstation_uz);
            if (vagons_uz.Count()>0)
            {
                res = TakeVagonOfUZ(vagons_uz.First(), natur, dt_amkr, id_stations, id_ways); // Примем вагон на станцию АМКР
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
    }
}
