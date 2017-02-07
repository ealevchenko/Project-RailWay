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
    public class OutputSostav
    {
        private eventID eventID = eventID.EFRailWay_KIS_InputSostav;

        IOracleOutputSostavRepository rep_os;

        public OutputSostav() 
        {
            this.rep_os = new EFOracleOutputSostavRepository();
        }

        public OutputSostav(IOracleOutputSostavRepository rep_os) 
        {
            this.rep_os = rep_os;
        }
        /// <summary>
        /// Показать все составы
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_OutputSostav> GetOutputSostav()
        {
            try
            {
                return rep_os.Oracle_OutputSostav;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetOutputSostav", eventID);
                return null;
            }
        }
        /// <summary>
        /// Показать состав
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Oracle_OutputSostav GetOutputSostav(int ID)
        {
            return GetOutputSostav().Where(i => i.ID == ID).FirstOrDefault();
        }
        /// <summary>
        /// Показать составы за период
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<Oracle_OutputSostav> GetOutputSostav(DateTime start, DateTime stop)
        {
            return GetOutputSostav().Where(o => o.DateTime >= start & o.DateTime <= stop);
        }
        /// <summary>
        /// Получить список незакрытых составов
        /// </summary>
        /// <returns></returns>
        public IQueryable<Oracle_OutputSostav> GetOutputSostavNoClose()
        {
            return GetOutputSostav().Where(i => i.Close == null).OrderBy(i => i.DateTime);
        }
        /// <summary>
        /// Получить последнюю дату
        /// </summary>
        /// <returns></returns>
        public DateTime? GetLastDateTime()
        {
            Oracle_OutputSostav ois = GetOutputSostav().OrderByDescending(i => i.DateTime).FirstOrDefault();
            if (ois != null) { return ois.DateTime; }
            return null;
        }
        /// <summary>
        /// Добавить сохранить
        /// </summary>
        /// <param name="Oracle_OutputSostav"></param>
        /// <returns></returns>
        public int SaveOracle_OutputSostav(Oracle_OutputSostav Oracle_OutputSostav) 
        {
            return this.rep_os.SaveOracle_OutputSostav(Oracle_OutputSostav);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Oracle_OutputSostav DeleteOracle_OutputSostav(int ID) 
        {
            return this.rep_os.DeleteOracle_OutputSostav(ID);
        }


    }
}
