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


    public class EFOracleRulesCopyRepository : EFRepository, IOracleRulesCopyRepository
    {
        private eventID eventID = eventID.EFRailWay_KIS_EFOracleRulesCopyRepository;
        
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<Oracle_RulesCopy> Oracle_RulesCopy
        {
            get { return context.Oracle_RulesCopy; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="oracle_RulesCopy"></param>
        /// <returns></returns>
        public int SaveOracle_RulesCopy(Oracle_RulesCopy oracle_RulesCopy)
        {
            Oracle_RulesCopy dbEntry;
            if (oracle_RulesCopy.IDRulesCopy == 0)
            {
                dbEntry = new Oracle_RulesCopy()
                {
                    IDRulesCopy = 0,
                    IDStationOn = oracle_RulesCopy.IDStationOn,
                    IDStationFrom = oracle_RulesCopy.IDStationFrom,
                    TypeCopy = oracle_RulesCopy.TypeCopy
                };
                context.Oracle_RulesCopy.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Oracle_RulesCopy.Find(oracle_RulesCopy.IDRulesCopy);
                if (dbEntry != null)
                {
                    dbEntry.IDStationOn = oracle_RulesCopy.IDStationOn;
                    dbEntry.IDStationFrom = oracle_RulesCopy.IDStationFrom;
                    dbEntry.TypeCopy = oracle_RulesCopy.TypeCopy;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveOracle_RulesCopy", eventID);                
                return -1;
            }
            return dbEntry.IDRulesCopy;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDRulesCopy"></param>
        /// <returns></returns>
        public Oracle_RulesCopy DeleteOracle_RulesCopy(int IDRulesCopy)
        {
            Oracle_RulesCopy dbEntry = context.Oracle_RulesCopy.Find(IDRulesCopy);
            if (dbEntry != null)
            {
                context.Oracle_RulesCopy.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteOracle_RulesCopy", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
