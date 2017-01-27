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
    public class RC_Stations
    {
        private eventID eventID = eventID.EFRailCars_RailCars_RC_Stations;
        IStationsRepository rep_st;

        public RC_Stations() 
        {
            this.rep_st = new EFStationsRepository();
        }

        public RC_Stations(IStationsRepository rep_st) 
        {
            this.rep_st = rep_st;
        }
        /// <summary>
        /// Вернуть все станции
        /// </summary>
        /// <returns></returns>
        public IQueryable<STATIONS> GetStations() 
        {
            try
            {
                return rep_st.STATIONS;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetStations", eventID);
                return null;
            }
        }
        /// <summary>
        /// Вернуть станцию по id системы КИС
        /// </summary>
        /// <param name="id_station_kis"></param>
        /// <returns></returns>
        public STATIONS GetStationsOfKis(int id_station_kis) 
        {
            return GetStations().Where(s => s.id_ora == id_station_kis).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть станцию по id системы Railcars
        /// </summary>
        /// <param name="id_station"></param>
        /// <returns></returns>
        public STATIONS GetStations(int id_station) 
        {
            return GetStations().Where(s => s.id_stat == id_station).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть ID станции системы Railcars
        /// </summary>
        /// <param name="id_station_kis"></param>
        /// <returns></returns>
        public int? GetIDStationsOfKis(int id_station_kis) 
        {
            STATIONS st = GetStationsOfKis(id_station_kis);
            if (st != null) return st.id_stat;
            return null;
        }
        /// <summary>
        /// Вернуть ID станции системы КИС
        /// </summary>
        /// <param name="id_station"></param>
        /// <returns></returns>
        public int? GetIDStationsKis(int id_station) 
        {
            STATIONS st = GetStations(id_station);
            if (st != null) return st.id_ora;
            return null;
        }
        public int SaveStations(STATIONS stations)
        {
            return rep_st.SaveSTATIONS(stations);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_stat"></param>
        /// <returns></returns>
        public STATIONS DeleteStations(int id_stat)
        {
            return rep_st.DeleteSTATIONS(id_stat);
        }
        /// <summary>
        /// Вернуть список станций УЗ
        /// </summary>
        /// <returns></returns>
        public IQueryable<STATIONS> GetUZStations() 
        {
            return GetStations().Where(s => s.is_uz == 1);
        }
        /// <summary>
        /// Вернуть список станций АМКР
        /// </summary>
        /// <returns></returns>
        public IQueryable<STATIONS> GetAMKRStations() 
        {
            return GetStations().Where(s => s.is_uz == 0);
        }
        /// <summary>
        /// Вернуть список ID станций uz
        /// </summary>
        /// <returns></returns>
        public List<int> GetUZStationsToID() 
        {
            IQueryable<STATIONS> stations = GetUZStations();
            return GetListStations(stations);
        }
        /// <summary>
        /// Вернуть список ID станций AMKR
        /// </summary>
        /// <returns></returns>
        public List<int> GetAMKRStationsToID() 
        {
            IQueryable<STATIONS> stations = GetAMKRStations();
            return GetListStations(stations);
        }
        /// <summary>
        /// Вернуть список ID станций
        /// </summary>
        /// <param name="stations"></param>
        /// <returns></returns>
        private List<int> GetListStations(IQueryable<STATIONS> stations) 
        { 
            List<int> list = new List<int>();
            if (stations != null) 
            {
                foreach (STATIONS st in stations) 
                {
                    list.Add(st.id_stat);
                }
            }
            return list;        
        }

    }
}
