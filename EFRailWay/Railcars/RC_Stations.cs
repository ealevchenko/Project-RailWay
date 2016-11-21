using EFRailWay.Abstract.Railcars;
using EFRailWay.Concrete.Railcars;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Railcars
{
    public class RC_Stations
    {
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
            return rep_st.STATIONS;
        }
        /// <summary>
        /// Вернуть станцию по id системы КИС
        /// </summary>
        /// <param name="id_station_kis"></param>
        /// <returns></returns>
        public STATIONS GetStations(int id_station_kis) 
        {
            return GetStations().Where(s => s.id_ora == id_station_kis).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть ID станции системы Railcars
        /// </summary>
        /// <param name="id_station_kis"></param>
        /// <returns></returns>
        public int? GetIDStations(int id_station_kis) 
        {
            STATIONS st = GetStations(id_station_kis);
            if (st != null) return st.id_stat;
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

    }
}
