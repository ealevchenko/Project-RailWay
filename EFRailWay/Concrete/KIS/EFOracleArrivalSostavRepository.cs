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
    public enum statusSting : int { Normal = 0, Delete=1, Insert=2, Update=3 }
    
    public class EFOracleArrivalSostavRepository : EFRepository, IOracleArrivalSostavRepository
    {
        private eventID eventID = eventID.EFRailWay_KIS_EFOracleArrivalSostavRepository;
        
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Oracle_ArrivalSostav> Oracle_ArrivalSostav
        {
            get { return context.Oracle_ArrivalSostav; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="TypeValue"></param>
        /// <returns></returns>
        public int SaveOracle_ArrivalSostav(Oracle_ArrivalSostav oracle_ArrivalSostav)
        {
            Oracle_ArrivalSostav dbEntry;
            if (oracle_ArrivalSostav.IDOrcSostav == 0)
            {
                dbEntry = new Oracle_ArrivalSostav()
                {
                    IDOrcSostav = oracle_ArrivalSostav.IDOrcSostav,
                    DateTime = oracle_ArrivalSostav.DateTime,
                    Day = oracle_ArrivalSostav.Day,
                    Month = oracle_ArrivalSostav.Month,
                    Year = oracle_ArrivalSostav.Year,
                    Hour = oracle_ArrivalSostav.Hour,
                    Minute = oracle_ArrivalSostav.Minute,
                    NaturNum = oracle_ArrivalSostav.NaturNum,
                    IDOrcStation = oracle_ArrivalSostav.IDOrcStation,
                    WayNum = oracle_ArrivalSostav.WayNum,
                    Napr = oracle_ArrivalSostav.Napr,
                    CountWagons = oracle_ArrivalSostav.CountWagons,
                    CountNatHIist = oracle_ArrivalSostav.CountNatHIist,
                    CountSetWagons = oracle_ArrivalSostav.CountSetWagons,
                    CountSetNatHIist = oracle_ArrivalSostav.CountSetNatHIist,
                    Close = oracle_ArrivalSostav.Close,
                    Status = oracle_ArrivalSostav.Status,
                    ListWagons = oracle_ArrivalSostav.ListWagons,
                    ListNoSetWagons = oracle_ArrivalSostav.ListNoSetWagons,
                    ListNoUpdateWagons = oracle_ArrivalSostav.ListNoUpdateWagons,
                };
                context_edit.Oracle_ArrivalSostav.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.Oracle_ArrivalSostav.Find(oracle_ArrivalSostav.IDOrcSostav);
                if (dbEntry != null)
                {
                        //dbEntry.IDOrcSostav = oracle_ArrivalSostav.IDOrcSostav;
                        dbEntry.DateTime = oracle_ArrivalSostav.DateTime;
                        dbEntry.Day = oracle_ArrivalSostav.Day;
                        dbEntry.Month = oracle_ArrivalSostav.Month;
                        dbEntry.Year = oracle_ArrivalSostav.Year;
                        dbEntry.Hour = oracle_ArrivalSostav.Hour;
                        dbEntry.Minute = oracle_ArrivalSostav.Minute;
                        dbEntry.NaturNum = oracle_ArrivalSostav.NaturNum;
                        dbEntry.IDOrcStation = oracle_ArrivalSostav.IDOrcStation;
                        dbEntry.WayNum = oracle_ArrivalSostav.WayNum;
                        dbEntry.Napr = oracle_ArrivalSostav.Napr;
                        dbEntry.CountWagons = oracle_ArrivalSostav.CountWagons;
                        dbEntry.CountNatHIist = oracle_ArrivalSostav.CountNatHIist;
                        dbEntry.CountSetWagons = oracle_ArrivalSostav.CountSetWagons;
                        dbEntry.CountSetNatHIist = oracle_ArrivalSostav.CountSetNatHIist;
                        dbEntry.Close = oracle_ArrivalSostav.Close;
                        dbEntry.Status = oracle_ArrivalSostav.Status;
                        dbEntry.ListWagons = oracle_ArrivalSostav.ListWagons;
                        dbEntry.ListNoSetWagons = oracle_ArrivalSostav.ListNoSetWagons;
                        dbEntry.ListNoUpdateWagons = oracle_ArrivalSostav.ListNoUpdateWagons; 
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveOracle_ArrivalSostav", eventID);
                return -1;
            }
            return dbEntry.IDOrcSostav;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDOrcSostav"></param>
        /// <returns></returns>
        public Oracle_ArrivalSostav DeleteOracle_ArrivalSostav(int IDOrcSostav)
        {
            Oracle_ArrivalSostav dbEntry = context_edit.Oracle_ArrivalSostav.Find(IDOrcSostav);
            if (dbEntry != null)
            {
                context_edit.Oracle_ArrivalSostav.Remove(dbEntry);
                try
                {
                    context_edit.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteOracle_ArrivalSostav", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
