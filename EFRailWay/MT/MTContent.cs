using EFRailWay.Abstract;
using EFRailWay.Concrete;
using EFRailWay.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.MT
{

    public enum tMTOperation : int { not = 0, coming = 1, tsp = 2 }

    public enum tMTConsignee : int { AMKR = 1}

    public class MTContent
    {
        private eventID eventID = eventID.EFRailWay_MT_MTContent;
        
        private IMTRepository rep_MT;        
        #region КОНСТРУКТОРЫ
        /// <summary>
        /// 
        /// </summary>
        public MTContent() 
        {
            this.rep_MT = new EFMTRepository();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rep_MT"></param>
        public MTContent(IMTRepository rep_MT) 
        {
            this.rep_MT = rep_MT;
        }
        #endregion

        #region MTSostav
        /// <summary>
        /// Получить состав по ID
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <returns></returns>
        public MTSostav Get_MTSostav(int id_mtsostav)
        {
            return Get_MTSostav().Where(s => s.IDMTSostav == id_mtsostav).FirstOrDefault();
        }
        /// <summary>
        /// Получить список всех составов
        /// </summary>
        /// <returns></returns>
        public IQueryable<MTSostav> Get_MTSostav()
        {

            try
            {
                return rep_MT.MTSostav.OrderBy(s => s.DateTime);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "Get_MTSostav", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить перечень составов за период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<MTSostav> GetMTSostav(DateTime start, DateTime stop) 
        {
            return Get_MTSostav().Where(s => s.DateTime >= start & s.DateTime <= stop);
        }
        /// <summary>
        /// Получить список составов по указаному номеру
        /// </summary>
        /// <param name="CompositionIndex"></param>
        /// <returns></returns>
        public IQueryable<MTSostav> Get_MTSostav(string CompositionIndex)
        {
            return rep_MT.MTSostav.Where(s => s.CompositionIndex == CompositionIndex).OrderBy(s => s.DateTime);
        }
        /// <summary>
        /// Определить наличие состава
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsExistSostav(string file) 
        {
            MTSostav sostav = rep_MT.MTSostav.Where(s => s.FileName == file).FirstOrDefault();
            if (sostav != null) return true;
            return false;
        }
        /// <summary>
        /// вернуть состав по имени файла
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public MTSostav Get_MTSostavToFile(string file) 
        {
            return rep_MT.MTSostav.Where(s => s.FileName == file).FirstOrDefault();
        }
        /// <summary>
        /// Получить последний не закрытый состав 
        /// </summary>
        /// <param name="CompositionIndex"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public MTSostav Get_NoCloseMTSostav(string CompositionIndex, DateTime date) 
        {
            return rep_MT.MTSostav.Where(s => s.CompositionIndex == CompositionIndex & s.Close == null & s.DateTime <= date).OrderByDescending(s => s.DateTime).FirstOrDefault();
        }
        /// <summary>
        /// Получить последний не закрытый состав за период времени определенный от date-day по date
        /// </summary>
        /// <param name="CompositionIndex"></param>
        /// <param name="date"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public MTSostav Get_NoCloseMTSostav(string CompositionIndex, DateTime date, int day) 
        {
            try
            {
                DateTime dt_start = date.AddDays(-1 * day);
                return rep_MT.MTSostav.Where(s => s.CompositionIndex == CompositionIndex & s.Close == null & s.DateTime>= dt_start & s.DateTime <= date).OrderByDescending(s => s.DateTime).FirstOrDefault();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "Get_NoCloseMTSostav(3)", eventID);
                return null;
            }
            

        }
        /// <summary>
        /// Добавить или править состав
        /// </summary>
        /// <param name="mtsostav"></param>
        /// <returns></returns>
        public int SaveMTSostav(MTSostav mtsostav) 
        {
            return rep_MT.SaveMTSostav(mtsostav);
        }
        /// <summary>
        /// Удалить состав
        /// </summary>
        /// <param name="IDMTSostav"></param>
        /// <returns></returns>
        public MTSostav DeleteMTSostav(int IDMTSostav) 
        {
            return rep_MT.DeleteMTSostav(IDMTSostav);
        }
        /// <summary>
        /// Вернуть операцию
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public string OperationName(int operation) 
        {
            switch (operation) 
            {
                case 1: return "Прибытие";
                case 2: return "ТСП";
                default: return "Не определена";
            }
        }

        /// <summary>
        /// Получить состав через цепочку операций
        /// </summary>
        /// <param name="parent_id"></param>
        /// <returns></returns>
        public MTSostav GetMTSostavOfParentID(int parent_id)
        {
            return Get_MTSostav().Where(s=>s.ParentID == parent_id).FirstOrDefault();
        }
        /// <summary>
        /// Получить цепочку операций для состава
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_sostav"></param>
        protected void GetOperationMTSostav(ref List<MTSostav> list, int id_sostav)
        {
            MTSostav sostav = GetMTSostavOfParentID(id_sostav);
            if (sostav != null) { 
                list.Add(sostav);
                GetOperationMTSostav(ref list, sostav.IDMTSostav);
            }

        }
        /// <summary>
        /// Получить список операций для состава
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public List<MTSostav> GetOperationMTSostav(int id_sostav)
        {
            List<MTSostav> list = new List<MTSostav>();
            MTSostav sostav = Get_MTSostav(id_sostav);
            if (sostav != null)
            {
                list.Add(sostav);
                GetOperationMTSostav(ref list, id_sostav);
            }
            return list;
        }
        /// <summary>
        /// Получить список не закрытых составов.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<MTSostav> GetListOpenMTSostav(DateTime dt) 
        { 
            List<MTSostav> list = new List<MTSostav>();
            IQueryable<MTSostav> list_mtsostav = Get_MTSostav().Where(s => s.DateTime >= dt & s.Close == null);
            if (list_mtsostav != null) list = list_mtsostav.ToList();
            return list;
        }
        #endregion

        #region MTList
        /// <summary>
        /// Вернуть информацию о вагоне по id_mtList
        /// </summary>
        /// <param name="id_mtList"></param>
        /// <returns></returns>
        public MTList Get_MTList(int id_mtList)
        {
            return rep_MT.MTList.Where(l => l.IDMTList == id_mtList).SingleOrDefault();
        }
        /// <summary>
        /// Вернуть список вагонов указанного состава
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <returns></returns>
        public IQueryable<MTList> Get_MTListToSostav(int id_mtsostav)
        {
            return rep_MT.MTList.Where(l => l.IDMTSostav == id_mtsostav).OrderBy(l => l.Position);
        }
        /// <summary>
        /// Проверка у состава есть вагоны
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <returns></returns>
        public bool IsMTListToMTSostsv(int id_mtsostav) 
        {
            MTList mtlist = Get_MTListToSostav(id_mtsostav).FirstOrDefault();
            if (mtlist != null) return true;
            return false;
        }
        /// <summary>
        /// Вернуть все вагоны по всем составам
        /// </summary>
        /// <returns></returns>
        public IQueryable<MTList> Get_MTList()
        {
            return rep_MT.MTList.OrderBy(l => l.IDMTSostav).ThenBy(l => l.Position);
        }
        /// <summary>
        /// Вернуть номер поезда состава
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <returns></returns>
        public int? GetTrainNumberToSostav(int id_mtsostav)
        {
            MTList mtlist = Get_MTListToSostav(id_mtsostav).FirstOrDefault();
            if (mtlist != null) return mtlist.TrainNumber;
            return null;
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="mtlist"></param>
        /// <returns></returns>
        public int SaveMTList(MTList mtlist)
        {
            return rep_MT.SaveMTList(mtlist);
        }
        /// <summary>
        /// Удалить вагон
        /// </summary>
        /// <param name="IDMTSostav"></param>
        /// <returns></returns>
        public MTList DeleteMTList(int IDMTList)
        {
            return rep_MT.DeleteMTList(IDMTList);
        }
        /// <summary>
        /// Вернуть список id составов MT где есть указаные вагоны отсорт
        /// </summary>
        /// <param name="num_wag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int[] GetIDSostavToWagons(string num_wag, DateTime dt)
        {
            try
            {
                string[] wag_s = num_wag.Split(';');
                string sql = "SELECT IDMTSostav FROM RailWay.MTList " +
                               "WHERE (CarriageNumber IN (" + num_wag.Replace(";", ",").Remove(num_wag.Length - 1) + ")) AND (DateOperation <= CONVERT(datetime, '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "', 120)) " +
                                "GROUP BY DateOperation, IDMTSostav " +
                                "ORDER BY Count(IDMTSostav) DESC, DateOperation DESC";
                return rep_MT.db.SqlQuery<int>(sql).ToArray();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetIDSostavToWagons", eventID);
                return null;
            }
        }
        /// <summary>
        /// Проставить натурный лист на вагоны пришедшие по КИС
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="num"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int SetNaturToMTList(int natur, int num, DateTime dt, int day)
        {
            try
            {
                string sql = "UPDATE RailWay.MTList "+
	                            "SET NaturList = " + natur.ToString() +
	                            " where NaturList is null and CarriageNumber = "+num.ToString() +
                                " and (DateOperation >= convert(datetime,DATEADD(day,"+(day*-1).ToString()+", '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'),120) and DateOperation < convert(datetime,'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',120))";
                return rep_MT.db.ExecuteSqlCommand(sql);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SetNaturToMTList", eventID);
                return -1;
            }
        }
        /// <summary>
        /// Вернуть список вагонов металлург транса по которым проставлен натурный лист и не ранее указанной даты
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public IQueryable<MTList> GetListToNatur(int natur, DateTime dt) 
        {
            try
            {
                return rep_MT.MTList.Where(l => l.NaturList == natur & l.DateOperation >= dt).OrderByDescending(l => l.DateOperation).ThenBy(l => l.IDMTSostav).ThenBy(l => l.Position);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetListToNatur", eventID);
                return null;
            }
        }
        /// <summary>
        /// Вернуть список вагонов металлург транса по которым проставлен натурный лист и не ранее dt - day
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="dt"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public IQueryable<MTList> GetListToNatur(int natur, DateTime dt, int day) 
        {
            DateTime dt_op = dt.AddDays(-1 * day);
            return GetListToNatur(natur, dt_op);
        }
        /// <summary>
        /// Получить строку вагона из состава по натурному листу, номеру вагона, дате захода на АМКР
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="num_wag"></param>
        /// <param name="dt"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public MTList GetListToNatur(int natur, int num_wag, DateTime dt, int day) 
        {
            DateTime dt_st = dt.AddDays(-1 * day);
            try
            {
                return rep_MT.MTList.Where(l => l.NaturList == natur & l.CarriageNumber==num_wag & l.DateOperation >= dt_st & l.DateOperation <= dt).OrderByDescending(l => l.IDMTSostav).FirstOrDefault();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("GetListToNatur натурный лист: {0}, вагон: {1}, дата: {2}, интервал поиска: {3} дней.",natur,num_wag,dt, day), eventID);
                return null;
            }            
        }
        /// <summary>
        /// Вернуть количество вагонов в составе
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <returns></returns>
        public int CountMTList(int id_mtsostav) 
        {
            IQueryable<MTList> list = Get_MTListToSostav(id_mtsostav);
            return list != null ? list.Count() : 0;
        }
        /// <summary>
        /// Вернуть количество вагонов в составе
        /// </summary>
        /// <param name="id_mtsostav"></param>
        /// <param name="Consignees"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public int CountMTList(int id_mtsostav, int[] Consignees, int station) 
        {
            try
            {
                string Consignees_s = "";
                foreach (int c in Consignees) { Consignees_s += c.ToString() + ","; }
                string sql = "SELECT * FROM RailWay.MTList where IDMTSostav = " + id_mtsostav.ToString() + " and [Consignee] in(" + Consignees_s.Remove(Consignees_s.Length - 1) + ") and [IDStation] = "+ station.ToString();
                var list = rep_MT.db.SqlQuery<MTList>(sql);
                return list != null ? list.Count() : 0;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "CountMTList(2)", eventID);
                return 0;
            }
        }

        #endregion

        #region MTConsignee
        /// <summary>
        /// Получить все кода грузополучателей
        /// </summary>
        /// <returns></returns>
        public IQueryable<MTConsignee> Get_MTConsignee()
        {
            try
            {
                return rep_MT.MTConsignee;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "Get_MTConsignee", eventID);
                return null;
            }
            
            
        }
        /// <summary>
        /// Получить все кода указанного грузополучателя
        /// </summary>
        /// <param name="tmtc"></param>
        /// <returns></returns>
        public IQueryable<MTConsignee> Get_MTConsignee(tMTConsignee tmtc)
        {
            return Get_MTConsignee().Where(c=>c.Consignee == (int)tmtc);
        }
        /// <summary>
        /// Получить массив кодов грузополучателя по указаному типу грузополучателя
        /// </summary>
        /// <param name="tmtc"></param>
        /// <returns></returns>
        public int[] GetMTConsignee(tMTConsignee tmtc)
        {
            List<int> list_code = new List<int>();
            foreach (MTConsignee mtc in Get_MTConsignee(tmtc))
            {
                list_code.Add(mtc.Code);
            }
            return list_code.ToArray();
        }

        public int SaveMTConsignee(MTConsignee mtconsignee)
        {
            return rep_MT.SaveMTConsignee(mtconsignee);
        }

        public MTConsignee DeleteMTConsignee(int Code)
        {
            return rep_MT.DeleteMTConsignee(Code);
        }
        /// <summary>
        /// Получить строку грузополучателя по коду
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public MTConsignee MTConsignee(int Code)
        {
            return Get_MTConsignee().Where( c=>c.Code == Code).FirstOrDefault();
        }
        /// <summary>
        /// Код пренадлежит грузополучателю 
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsConsignee(int Code, tMTConsignee type) 
        {
            MTConsignee mtc = MTConsignee(Code);
            return mtc != null ? mtc.Consignee == (int)type ? true : false : false;
        }
        #endregion

    }
}
