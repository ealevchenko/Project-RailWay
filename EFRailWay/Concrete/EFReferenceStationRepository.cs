using EFRailWay.Abstract;
using EFRailWay.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFReferenceStationRepository : EFRepository, IReferenceStationRepository
    {
        private eventID eventID = eventID.EFRailWay_RailWay_EFReferenceStationRepository;

        public IQueryable<ReferenceStation> ReferenceStation
        {
            get { return context.ReferenceStation; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="ReferenceCountry"></param>
        /// <returns></returns>
        public int SaveReferenceStation(ReferenceStation ReferenceStation)
        {
            ReferenceStation dbEntry;
            if (ReferenceStation.IDStation == 0)
            {
                dbEntry = new ReferenceStation()
                {
                    IDStation = 0,
                    Name = ReferenceStation.Name,
                    Station = ReferenceStation.Station,
                    InternalRailroad = ReferenceStation.InternalRailroad,
                    IR_Abbr = ReferenceStation.IR_Abbr,
                    NameNetwork = ReferenceStation.NameNetwork,
                    NN_Abbr = ReferenceStation.NN_Abbr,
                    CodeCS = ReferenceStation.CodeCS
                };
                context.ReferenceStation.Add(dbEntry);
            }
            else
            {
                dbEntry = context.ReferenceStation.Find(ReferenceStation.IDStation);
                if (dbEntry != null)
                {
                    dbEntry.Name = ReferenceStation.Name;
                    dbEntry.Station = ReferenceStation.Station;
                    dbEntry.InternalRailroad = ReferenceStation.InternalRailroad;
                    dbEntry.IR_Abbr = ReferenceStation.IR_Abbr;
                    dbEntry.NameNetwork = ReferenceStation.NameNetwork;
                    dbEntry.NN_Abbr = ReferenceStation.NN_Abbr;
                    dbEntry.CodeCS = ReferenceStation.CodeCS;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveReferenceStation", eventID);
                return -1;
            }
            return dbEntry.IDStation;

        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDCountry"></param>
        /// <returns></returns>
        public ReferenceStation DeleteReferenceStation(int IDCountry)
        {
            ReferenceStation dbEntry = context.ReferenceStation.Find(IDCountry);
            if (dbEntry != null)
            {
                context.ReferenceStation.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteReferenceStation", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
