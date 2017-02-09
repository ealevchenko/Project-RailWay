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
    public class RC_Tupiki
    {
        private eventID eventID = eventID.EFRailCars_RailCars_RC_Tupiki;
        ITupikiRepository rep_tp;

        public RC_Tupiki() 
        {
            this.rep_tp = new EFTupikiRepository();
        }

        public RC_Tupiki(ITupikiRepository rep_tp) 
        {
            this.rep_tp = rep_tp;
        }

        /// <summary>
        /// Получить все тупики
        /// </summary>
        /// <returns></returns>
        public IQueryable<TUPIKI> GetTupiki()
        {
            try
            {
                return rep_tp.TUPIKI;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetTupiki", eventID);
                return null;
            }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="TUPIKI"></param>
        /// <returns></returns>
        public int SaveTUPIKI(TUPIKI tupiki)
        {
            return rep_tp.SaveTUPIKI(tupiki);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_tupik"></param>
        /// <returns></returns>
        public TUPIKI DeleteTUPIKI(int id_tupik)
        {
            return rep_tp.DeleteTUPIKI(id_tupik);
        }
        /// <summary>
        /// Получить тупик по id системы RailCars
        /// </summary>
        /// <param name="id_tupik"></param>
        /// <returns></returns>
        public TUPIKI GetTupik(int id_tupik) 
        {
            return GetTupiki().Where(t => t.id_tupik == id_tupik).FirstOrDefault();
        }
        /// <summary>
        /// Получить тупик по id системы КИС
        /// </summary>
        /// <param name="id_tupik_kis"></param>
        /// <returns></returns>
        public TUPIKI GetTupikOfKis(int id_tupik_kis) 
        {
            return GetTupiki().Where(t => t.id_ora == id_tupik_kis).FirstOrDefault();
        }
        /// <summary>
        /// Получить id системы RailCars по id системы КИС
        /// </summary>
        /// <param name="id_tupik_kis"></param>
        /// <returns></returns>
        public int? GetIDTupikOfKis(int id_tupik_kis) 
        {
            TUPIKI tp = GetTupikOfKis(id_tupik_kis);
            if (tp != null) { return tp.id_tupik; } else return null;
        }

    }
}
