using EFRailCars.Railcars;
using EFRailWay.Abstract.KIS;
using EFRailWay.Concrete.KIS;
using EFRailWay.Entities.KIS;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.KIS
{
    public enum typeOracleRules : int { Input = 0, Output = 1}  
 
    public class OracleRules
    {
        public int IDOn {get;set;}
        public int[] IDFrom {get;set;}
    }

    public class RulesCopy
    {
        private eventID eventID = eventID.EFRailWay_KIS_RulesCopy;
        private RC_Stations rcs = new RC_Stations();

        IOracleRulesCopyRepository rep_rc;

        public RulesCopy()
        {
            this.rep_rc = new EFOracleRulesCopyRepository();
        }

        public RulesCopy(IOracleRulesCopyRepository rep_rc)
        {
            this.rep_rc = rep_rc;
        }
        /// <summary>
        /// Получить список всех правил
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_RulesCopy> GetRulesCopy()
        {
            try
            {
                return this.rep_rc.Oracle_RulesCopy;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetRulesCopy", eventID);
                return null;
            }
        }
        /// <summary>
        /// Сохранить правило
        /// </summary>
        /// <param name="oracle_RulesCopy"></param>
        /// <returns></returns>
        public int SaveOracle_RulesCopy(Oracle_RulesCopy oracle_RulesCopy)
        {
            return this.rep_rc.SaveOracle_RulesCopy(oracle_RulesCopy);
        }
        /// <summary>
        /// Удалить правило
        /// </summary>
        /// <param name="IDRulesCopy"></param>
        /// <returns></returns>
        public Oracle_RulesCopy DeleteOracle_RulesCopy(int IDRulesCopy)
        {
            return this.rep_rc.DeleteOracle_RulesCopy(IDRulesCopy);
        }

        public IQueryable<Oracle_RulesCopy>GetRulesCopy(typeOracleRules tor)
        {
            return GetRulesCopy().Where(r => r.TypeCopy == (int)tor);
        }
        /// <summary>
        /// Получить список правил с id системы railcars
        /// </summary>
        /// <param name="tor"></param>
        /// <returns></returns>
        public List<OracleRules> GetRulesCopyToOracleRules(typeOracleRules tor)
        {
            List<OracleRules> list = new List<OracleRules>();

            IEnumerable<IGrouping<int, Oracle_RulesCopy>> result = GetRulesCopy(tor).GroupBy(c => c.IDStationOn);
            foreach (IGrouping<int, Oracle_RulesCopy> gr in result) 
            {
                List<int> from = new List<int>();
                foreach(Oracle_RulesCopy rc in gr)
                {
                    from.Add(rc.IDStationFrom);
                }
                list.Add(new OracleRules() { IDOn = gr.Key, IDFrom = from.ToArray() });
            }
            return list;

        }
        /// <summary>
        /// Получить список правил с id системы КИС
        /// </summary>
        /// <param name="tor"></param>
        /// <returns></returns>
        public List<OracleRules> GetRulesCopyToOracleRulesOfKis(typeOracleRules tor)
        {
            List<OracleRules> list = new List<OracleRules>();

            IEnumerable<IGrouping<int, Oracle_RulesCopy>> result = GetRulesCopy(tor).GroupBy(c => c.IDStationOn);
            foreach (IGrouping<int, Oracle_RulesCopy> gr in result) 
            {
                List<int> from = new List<int>();
                foreach(Oracle_RulesCopy rc in gr)
                {
                    int? st_from = rcs.GetIDStationsKis(rc.IDStationFrom);
                    if (st_from != null) { from.Add((int)st_from); }
                }
                int? st_on = rcs.GetIDStationsKis(gr.Key);
                if (st_on != null) {
                    list.Add(new OracleRules() { IDOn = (int)st_on, IDFrom = from.ToArray() });
                }
            }
            return list;

        }
    }
}
