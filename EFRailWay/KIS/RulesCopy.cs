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
        /// Получить правило по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Oracle_RulesCopy GetRulesCopy(int id)
        {
            return GetRulesCopy().Where(r => r.IDRulesCopy == id).FirstOrDefault();
        }
        /// <summary>
        /// Получить правило для по типу копирования для станций
        /// </summary>
        /// <param name="from"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public Oracle_RulesCopy GetRulesCopy(int from, int on, typeOracleRules tor)
        {
            return GetRulesCopy().Where(r => r.IDStationFrom == from & r.IDStationOn==on & r.TypeCopy==(int)tor).FirstOrDefault();
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
        /// <summary>
        /// Получить все правила по указанному типу правил
        /// </summary>
        /// <param name="tor"></param>
        /// <returns></returns>
        public IQueryable<Oracle_RulesCopy>GetRulesCopy(typeOracleRules tor)
        {
            return GetRulesCopy().Where(r => r.TypeCopy == (int)tor);
        }
        /// <summary>
        /// Получить правила для станции прибытия по типу правила копирования
        /// </summary>
        /// <param name="station_on"></param>
        /// <param name="tor"></param>
        /// <returns></returns>
        public IQueryable<Oracle_RulesCopy>GetRulesCopy(int station_on, typeOracleRules tor)
        {
            return GetRulesCopy().Where(r => r.TypeCopy == (int)tor & r.IDStationOn==station_on);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_from"></param>
        /// <param name="station_on"></param>
        /// <param name="tor"></param>
        /// <returns></returns>
        public OracleRules GetRulesCopyToOracleRules(int station_from, int station_on, typeOracleRules tor)
        {
            Oracle_RulesCopy rules = GetRulesCopy(station_from, station_on, tor);
            return rules != null ? new OracleRules() { IDOn = rules.IDStationOn, IDFrom = new int[] { rules.IDStationFrom } } : null;
        }
        /// <summary>
        /// Проверка есть на эти станции правила
        /// </summary>
        /// <param name="station_from"></param>
        /// <param name="station_on"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public bool IsRules(int station_from, int station_on, typeOracleRules tp) 
        {
            Oracle_RulesCopy rules = GetRulesCopy().Where(r => r.TypeCopy == (int)tp & r.IDStationFrom == station_from & r.IDStationOn == station_on).FirstOrDefault();
            return rules != null ? true : false;
        }
        /// <summary>
        /// Получение списка id правил для указанного списка правил
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        public List<int> GetListRules(IQueryable<Oracle_RulesCopy> rules)
        {
            List<int> list = new List<int>();
            if (rules != null)
            {
                foreach (Oracle_RulesCopy rule in rules)
                {
                    list.Add(rule.IDRulesCopy);
                }
            }
            return list;
        }
        /// <summary>
        /// Получение списка idfrom для указанного списка правил
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        public List<int> GetListIDfromRules(IQueryable<Oracle_RulesCopy> rules)
        {
            List<int> list = new List<int>();
            if (rules != null)
            {
                foreach (Oracle_RulesCopy rule in rules)
                {
                    list.Add(rule.IDStationFrom);
                }
            }
            return list;
        }

        /// <summary>
        /// Сохранить правило и для станции прибытия (если станция отправки station_from = 0, будут сохранены все станции которые еще не указанные для данной станции прибытия)
        /// </summary>
        /// <param name="station_on"></param>
        /// <param name="station_from"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public int CreateRulesCopy(int station_on, int station_from, typeOracleRules tp) 
        {
            int result = 0;
            int[] stations_from = station_from == 0 ? rcs.GetListStations(rcs.GetStationOfNotListID(GetListIDfromRules(GetRulesCopy(station_on, tp)).ToArray())).ToArray() : new int[] { station_from };
            foreach (int ids in stations_from) {
                Oracle_RulesCopy or_ryles = new Oracle_RulesCopy()
                {
                    IDRulesCopy = 0,
                    IDStationFrom = ids,
                    IDStationOn = station_on,
                    TypeCopy = (int)tp
                };
                int res = SaveOracle_RulesCopy(or_ryles);
                if (res>0) result++;
            }
            return result;
        }

        public int DeleteOracle_RulesCopy(int id_station_on, typeOracleRules tp)
        {
            try
            {
                return rep_rc.db.ExecuteSqlCommand("DELETE FROM RailWay.Oracle_RulesCopy WHERE IDStationOn=" + id_station_on.ToString() + " AND TypeCopy = "+((int)tp).ToString());
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteRulesCopy(1)", eventID);
                return -1;
            }
        }

        public int DeleteOracle_RulesCopy(int id_station_on, int id_station_from, typeOracleRules tp)
        {
            try
            {
                return rep_rc.db.ExecuteSqlCommand("DELETE FROM RailWay.Oracle_RulesCopy WHERE IDStationOn=" + id_station_on.ToString() + " AND IDStationFrom = " + id_station_from.ToString() + " AND TypeCopy = " + ((int)tp).ToString());
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "DeleteRulesCopy(2)", eventID);
                return -1;
            }
        }
        /// <summary>
        /// Удалить правила копирования для станции прибытия
        /// </summary>
        /// <param name="station_on"></param>
        /// <param name="station_from"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public int DeleteRulesCopy(int station_on, int station_from, typeOracleRules tp) 
        {
            return station_from == 0 ? DeleteOracle_RulesCopy(station_on, tp) : DeleteOracle_RulesCopy(station_on, station_from, tp);
        }
    }
}
