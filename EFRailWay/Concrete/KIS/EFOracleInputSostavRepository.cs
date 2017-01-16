using EFRailWay.Abstract;
using EFRailWay.Abstract.KIS;
using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.KIS
{
    
    public class EFOracleInputSostavRepository : EFRepository, IOracleInputSostavRepository
    {
        private eventID eventID = eventID.EFRailWay_KIS_EFOracleInputSostavRepository;
        
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Oracle_InputSostav> Oracle_InputSostav
        {
            get { return context.Oracle_InputSostav; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="Oracle_InputSostav"></param>
        /// <returns></returns>
        public int SaveOracle_InputSostav(Oracle_InputSostav Oracle_InputSostav)
        {
            Oracle_InputSostav dbEntry;
            if (Oracle_InputSostav.ID == 0)
            {
                dbEntry = new Oracle_InputSostav()
                {
                    ID = 0,
                    DateTime = Oracle_InputSostav.DateTime,
                    DocNum = Oracle_InputSostav.DocNum,
                    IDOrcStationFrom = Oracle_InputSostav.IDOrcStationFrom,
                    WayNumFrom = Oracle_InputSostav.WayNumFrom,
                    NaprFrom = Oracle_InputSostav.NaprFrom,
                    IDOrcStationOn = Oracle_InputSostav.IDOrcStationOn,
                    CountWagons = Oracle_InputSostav.CountWagons,
                    CountSetWagons = Oracle_InputSostav.CountSetWagons,
                    CountUpdareWagons = Oracle_InputSostav.CountUpdareWagons,
                    Close = Oracle_InputSostav.Close,
                    Status = Oracle_InputSostav.Status
                };
                context.Oracle_InputSostav.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Oracle_InputSostav.Find(Oracle_InputSostav.ID);
                if (dbEntry != null)
                {

                    dbEntry.DateTime = Oracle_InputSostav.DateTime;
                    dbEntry.DocNum = Oracle_InputSostav.DocNum;
                    dbEntry.IDOrcStationFrom = Oracle_InputSostav.IDOrcStationFrom;
                    dbEntry.WayNumFrom = Oracle_InputSostav.WayNumFrom;
                    dbEntry.NaprFrom = Oracle_InputSostav.NaprFrom;
                    dbEntry.IDOrcStationOn = Oracle_InputSostav.IDOrcStationOn;
                    dbEntry.CountWagons = Oracle_InputSostav.CountWagons;
                    dbEntry.CountSetWagons = Oracle_InputSostav.CountSetWagons;
                    dbEntry.CountUpdareWagons = Oracle_InputSostav.CountUpdareWagons;
                    dbEntry.Close = Oracle_InputSostav.Close;
                    dbEntry.Status = Oracle_InputSostav.Status;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveOracle_InputSostav", eventID);                
                return -1;
            }
            return dbEntry.ID;
        }
        /// <summary>
        ///  Удалить
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Oracle_InputSostav DeleteOracle_InputSostav(int ID)
        {
            Oracle_InputSostav dbEntry = context.Oracle_InputSostav.Find(ID);
            if (dbEntry != null)
            {
                context.Oracle_InputSostav.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteOracle_InputSostav", eventID);
                    return null;
                }
            }
            return dbEntry;
        }

    }
}
