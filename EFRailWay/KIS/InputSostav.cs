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
    public class InputSostav
    {
        private eventID eventID = eventID.EFRailWay_KIS_InputSostav;

        IOracleInputSostavRepository rep_is;

        public InputSostav() 
        {
            this.rep_is = new EFOracleInputSostavRepository();
        }

        public InputSostav(IOracleInputSostavRepository rep_is) 
        {
            this.rep_is = rep_is;
        }
        /// <summary>
        /// Показать все составы
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_InputSostav> GetInputSostav()
        {
            try
            {
                return rep_is.Oracle_InputSostav;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetInputSostav", eventID);
                return null;
            }
        }
        /// <summary>
        /// Показать состав
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Oracle_InputSostav GetInputSostav(int ID)
        {
            return GetInputSostav().Where(i => i.ID == ID).FirstOrDefault();
        }

        public IQueryable<Oracle_InputSostav> GetInputSostav(DateTime start, DateTime stop)
        {
            return GetInputSostav().Where(o => o.DateTime >= start & o.DateTime <= stop);
        }
        /// <summary>
        /// Получить список незакрытых составов
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_InputSostav> GetInputSostavNoClose()
        {
            return GetInputSostav().Where(i => i.Close == null).OrderBy(i => i.DateTime);
        }
        /// <summary>
        /// Получить последнюю дату
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastDateTime()
        {
            Oracle_InputSostav ois = GetInputSostav().OrderByDescending(i => i.DateTime).FirstOrDefault();
            if (ois != null) { return ois.DateTime; }
            return null;
        }
        /// <summary>
        /// Добавить сохранить
        /// </summary>
        /// <param name="Oracle_InputSostav"></param>
        /// <returns></returns>
        public int SaveOracle_InputSostav(Oracle_InputSostav Oracle_InputSostav) 
        {
            return this.rep_is.SaveOracle_InputSostav(Oracle_InputSostav);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Oracle_InputSostav DeleteOracle_InputSostav(int ID) 
        {
            return this.rep_is.DeleteOracle_InputSostav(ID);
        }


    }
}
