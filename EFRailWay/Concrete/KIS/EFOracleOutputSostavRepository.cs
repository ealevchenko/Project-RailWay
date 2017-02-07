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

    public class EFOracleOutputSostavRepository : EFRepository, IOracleOutputSostavRepository
    {
        private eventID eventID = eventID.EFRailWay_KIS_EFOracleOutputSostavRepository;
        
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Oracle_OutputSostav> Oracle_OutputSostav
        {
            get { return context.Oracle_OutputSostav; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="Oracle_OutputSostav"></param>
        /// <returns></returns>
        public int SaveOracle_OutputSostav(Oracle_OutputSostav Oracle_OutputSostav)
        {
            Oracle_OutputSostav dbEntry;
            if (Oracle_OutputSostav.ID == 0)
            {
                dbEntry = new Oracle_OutputSostav()
                {
                    ID = 0,
                    DateTime = Oracle_OutputSostav.DateTime,
                    DocNum = Oracle_OutputSostav.DocNum,
                    IDOrcStationFrom = Oracle_OutputSostav.IDOrcStationFrom,
                    IDOrcStationOn = Oracle_OutputSostav.IDOrcStationOn,
                    WayNumOn = Oracle_OutputSostav.WayNumOn,
                    NaprOn = Oracle_OutputSostav.NaprOn,
                    CountWagons = Oracle_OutputSostav.CountWagons,
                    CountSetWagons = Oracle_OutputSostav.CountSetWagons,
                    CountUpdareWagons = Oracle_OutputSostav.CountUpdareWagons,
                    Close = Oracle_OutputSostav.Close,
                    Status = Oracle_OutputSostav.Status, 
                    Message = Oracle_OutputSostav.Message 
                };
                context.Oracle_OutputSostav.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Oracle_OutputSostav.Find(Oracle_OutputSostav.ID);
                if (dbEntry != null)
                {
                    dbEntry.DateTime = Oracle_OutputSostav.DateTime;
                    dbEntry.DocNum = Oracle_OutputSostav.DocNum;
                    dbEntry.IDOrcStationFrom = Oracle_OutputSostav.IDOrcStationFrom;
                    dbEntry.IDOrcStationOn = Oracle_OutputSostav.IDOrcStationOn;
                    dbEntry.WayNumOn = Oracle_OutputSostav.WayNumOn;
                    dbEntry.NaprOn = Oracle_OutputSostav.NaprOn;
                    dbEntry.CountWagons = Oracle_OutputSostav.CountWagons;
                    dbEntry.CountSetWagons = Oracle_OutputSostav.CountSetWagons;
                    dbEntry.CountUpdareWagons = Oracle_OutputSostav.CountUpdareWagons;
                    dbEntry.Close = Oracle_OutputSostav.Close;
                    dbEntry.Status = Oracle_OutputSostav.Status;
                    dbEntry.Message = Oracle_OutputSostav.Message;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveOracle_OutputSostav", eventID);                
                return -1;
            }
            return dbEntry.ID;
        }
        /// <summary>
        ///  Удалить
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Oracle_OutputSostav DeleteOracle_OutputSostav(int ID)
        {
            Oracle_OutputSostav dbEntry = context.Oracle_OutputSostav.Find(ID);
            if (dbEntry != null)
            {
                context.Oracle_OutputSostav.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteOracle_OutputSostav", eventID);
                    return null;
                }
            }
            return dbEntry;
        }

    }
}
