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
    public class ArrivalSostav
    {
        private eventID eventID = eventID.EFRailWay_KIS_ArrivalSostav;

        IOracleArrivalSostavRepository rep_as;

        public ArrivalSostav() 
        {
            this.rep_as = new EFOracleArrivalSostavRepository();
        }

        public ArrivalSostav(IOracleArrivalSostavRepository rep_as) 
        {
            this.rep_as = rep_as;
        }
        /// <summary>
        /// Получить все сотавы 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_ArrivalSostav> Get_ArrivalSostav() 
        {
            try
            {
                return rep_as.Oracle_ArrivalSostav;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "Get_ArrivalSostav", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить строку состава по id
        /// </summary>
        /// <param name="IDOrcSostav"></param>
        /// <returns></returns>
        public Oracle_ArrivalSostav Get_ArrivalSostav(int IDOrcSostav) 
        {
            return Get_ArrivalSostav().Where(o => o.IDOrcSostav == IDOrcSostav).FirstOrDefault();
        }
        /// <summary>
        /// Выбрать перенесеные составы за указанный период 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<Oracle_ArrivalSostav> Get_ArrivalSostav(DateTime start, DateTime stop) 
        {
            return rep_as.Oracle_ArrivalSostav.Where(o => o.DateTime >= start & o.DateTime <= stop);
        }
        /// <summary>
        /// Выбрать не закрытые перенесеные составы
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_ArrivalSostav> Get_ArrivalSostavNoClose()
        {
            return rep_as.Oracle_ArrivalSostav.Where(o => o.Close == null).OrderBy(o => o.DateTime);
        }
        /// <summary>
        /// Вернуть последнее время по которому перенесли состав
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastDateTime() 
        {
            Oracle_ArrivalSostav oas = Get_ArrivalSostav().OrderByDescending(a => a.DateTime).FirstOrDefault();
            if (oas != null) { return oas.DateTime; }
            return null;
        }
        /// <summary>
        /// Сохранить добавить состав
        /// </summary>
        /// <param name="Oracle_ArrivalSostav"></param>
        /// <returns></returns>
        public int SaveOracle_ArrivalSostav(Oracle_ArrivalSostav Oracle_ArrivalSostav) 
        {
            return rep_as.SaveOracle_ArrivalSostav(Oracle_ArrivalSostav);
        }
    }
}
