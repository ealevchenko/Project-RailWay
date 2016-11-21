using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
//using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFCodeStationRepository : EFRepository, ICodeStationRepository
    {
        public IQueryable<Code_Station> Code_Station
        {
            get { return context.Code_Station; }
        }
        /// <summary>
        /// Добавить или править Code_Station
        /// </summary>
        /// <param name="station"></param>
        public bool SaveStation(Code_Station station)
        {
            if (station.IDStation == 0)
            {
                context_edit.Code_Station.Add(station);
            }
            else
            {
                Code_Station dbEntry = context_edit.Code_Station.Find(station.IDStation);
                if (dbEntry != null)
                {
                    dbEntry.Code = station.Code;
                    dbEntry.CodeCS = station.CodeCS;
                    dbEntry.Station = station.Station;
                    dbEntry.IDInternalRailroad = station.IDInternalRailroad;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

    }


}
