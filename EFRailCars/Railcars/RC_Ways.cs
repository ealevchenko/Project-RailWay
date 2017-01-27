using EFRailCars.Abstract;
using EFRailCars.Concrete;
using EFRailCars.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Railcars
{
    public class RC_Ways
    {
        private eventID eventID = eventID.EFRailCars_RailCars_RC_Ways;
        IWaysRepository rep_w;

        public RC_Ways() 
        {
            this.rep_w = new EFWaysRepository();
        }

        public RC_Ways(IWaysRepository rep_w) 
        {
            this.rep_w = rep_w;
        }
        /// <summary>
        /// Получить список всех путей
        /// </summary>
        /// <returns></returns>
        public IQueryable<WAYS> GetWays()
        {
            try
            {
                return rep_w.WAYS;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetWays", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить путь по id
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public WAYS GetWays(int id_way)
        {
            return GetWays().Where(w => w.id_way == id_way).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть все пути на станции
        /// </summary>
        /// <param name="id_station"></param>
        /// <returns></returns>
        public IQueryable<WAYS> GetWaysOfStations(int id_station)
        {
            return rep_w.WAYS.Where(w => w.id_stat == id_station);
        }
        /// <summary>
        /// Вернуть путь по указанной станции и номеру пути
        /// </summary>
        /// <param name="id_station_kis"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public WAYS GetWaysOfStations(int id_station, string num)
        {
            return GetWaysOfStations(id_station).Where(w => w.num.ToUpper() == num.ToUpper()).FirstOrDefault();
        }

        public int? GetIDWaysToStations(int id_station, string num)
        {
            WAYS ws = GetWaysOfStations(id_station, num);
            if (ws != null) return ws.id_way;
            return null;
        }
        /// <summary>
        /// Вернуть ID станции
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public int? GetIDStationOfWay(int id_way) 
        {
            WAYS way = GetWays(id_way);
            return way != null ? way.id_stat : null;
        }

        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="WAYS"></param>
        /// <returns></returns>
        public int SaveWays(WAYS ways)
        {
            return rep_w.SaveWAYS(ways);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public WAYS DeleteWays(int id_way)
        {
            return rep_w.DeleteWAYS(id_way);
        }
    }
}
