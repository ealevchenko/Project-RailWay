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
            return rep_MT.MTSostav.Where(s => s.IDMTSostav == id_mtsostav).SingleOrDefault();
        }
        /// <summary>
        /// Получить список всех составов
        /// </summary>
        /// <returns></returns>
        public IQueryable<MTSostav> Get_MTSostav()
        {
            return rep_MT.MTSostav.OrderBy(s => s.DateTime);
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
        public int SetNaturToMTList(int natur, int num, DateTime dt)
        {
            try
            {
                string sql = "UPDATE RailWay.MTList "+
	                            "SET NaturList = " + natur.ToString() +
	                            " where NaturList is null and CarriageNumber = "+num.ToString() +
                                " and (DateOperation >= convert(datetime,DATEADD(day,-1, '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'),120) and DateOperation < convert(datetime,'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',120))";
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

        #endregion

        #region MTConsignee
        /// <summary>
        /// Получить все кода грузополучателей
        /// </summary>
        /// <returns></returns>
        public IQueryable<MTConsignee> Get_MTConsignee()
        {
            return rep_MT.MTConsignee;
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
        #endregion

    }
}
