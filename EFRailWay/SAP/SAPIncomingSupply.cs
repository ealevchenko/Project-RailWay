using EFRailWay.Abstract.SAP;
using EFRailWay.Concrete.SAP;
using EFRailWay.Entities.SAP;
using Logs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.SAP
{
    public class SAPIncomingSupply
    {
        private eventID eventID = eventID.EFRailWay_SAP;
        private ISAPIncSupplyRepository rep_sap;

        #region КОНСТРУКТОРЫ
        /// <summary>
        /// 
        /// </summary>
        public SAPIncomingSupply() 
        {
            this.rep_sap = new EFSAPIncSupplyRepository();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rep_MT"></param>
        public SAPIncomingSupply(ISAPIncSupplyRepository rep_sap) 
        {
            this.rep_sap = rep_sap;
        }
        #endregion

        /// <summary>
        /// Получить список всех строк справочника SAP Входящая поставка
        /// </summary>
        /// <returns></returns>
        public IQueryable<SAPIncSupply> GetSAPIncSupply()
        {
            try
            {
                return rep_sap.SAPIncSupply;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSAPIncSupply", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список вагонов из справочника пренадлежащих составу
        /// </summary>
        /// <param name="idsostav"></param>
        /// <returns></returns>
        public IQueryable<SAPIncSupply> GetSAPIncSupply(int idsostav) 
        {
            return GetSAPIncSupply().Where(s => s.IDMTSostav == idsostav).OrderBy(s => s.Position);
        }
        /// <summary>
        /// Вернуть строку по id сотава и номеру вагона
        /// </summary>
        /// <param name="idsostav"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public SAPIncSupply GetSAPIncSupply(int idsostav, int num) 
        {
            return GetSAPIncSupply(idsostav).Where(s => s.CarriageNumber == num).FirstOrDefault();
        }
        /// <summary>
        /// Получить список номеров вагонов из справочника пренадлежащих составу
        /// </summary>
        /// <param name="idsostav"></param>
        /// <returns></returns>
        public List<int> GetSAPIncSupplyToNumWagons(int idsostav) 
        {
            List<int> wagons = new List<int>();
            foreach (SAPIncSupply sap in GetSAPIncSupply(idsostav)) 
            {
                wagons.Add(sap.CarriageNumber);
            }
            return wagons;
        }
        /// <summary>
        /// Обновить id состава
        /// </summary>
        /// <param name="new_idsostav"></param>
        /// <param name="old_idsostav"></param>
        /// <returns></returns>
        public int UpdateSAPIncSupplyIDSostav(int new_idsostav, int old_idsostav) 
        {
            try
            {
                SqlParameter new_id = new SqlParameter("@new", new_idsostav);
                SqlParameter old_id = new SqlParameter("@old", old_idsostav);
                return rep_sap.db.ExecuteSqlCommand("UPDATE RailWay.SAP_Inc_Supply set IDMTSostav=@new where IDMTSostav = @old", new_id, old_id);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("UpdateSAPIncSupplyIDSostav новый состав: {0}, старый состав: {1}.", new_idsostav, old_idsostav), eventID);
                return -1;
            }
        }
        /// <summary>
        /// обновить позицию вагона в составе
        /// </summary>
        /// <param name="idsostav"></param>
        /// <param name="numvag"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public int UpdateSAPIncSupplyPosition(int idsostav, int numvag, int position) 
        {
            try
            {
                SqlParameter id = new SqlParameter("@idsostav", idsostav);
                SqlParameter num = new SqlParameter("@num", numvag);
                SqlParameter pos = new SqlParameter("@pos", position);
                return rep_sap.db.ExecuteSqlCommand("UPDATE RailWay.SAP_Inc_Supply set Position=@pos where IDMTSostav = @idsostav and CarriageNumber = @num", pos, id, num);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("UpdateSAPIncSupplyPosition состав: {0}, вагон: {1}, позиция: {1} ", idsostav, numvag, position), eventID);
                return -1;
            }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="SAPIncSupply"></param>
        /// <returns></returns>
        public int SaveSAPIncSupply(SAPIncSupply SAPIncSupply)
        {
            return rep_sap.SaveSAPIncSupply(SAPIncSupply);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SAPIncSupply DeleteSAPIncSupply(int id)
        {
            return rep_sap.DeleteSAPIncSupply(id);
        }
        /// <summary>
        /// Удалить строку справочника по номеру состава
        /// </summary>
        /// <param name="idsostav"></param>
        /// <param name="numvag"></param>
        /// <returns></returns>
        public int DeleteSAPIncSupplySostav(int idsostav) 
        {
            try
            {
                SqlParameter id = new SqlParameter("@idsostav", idsostav);
                return rep_sap.db.ExecuteSqlCommand("Delete RailWay.SAP_Inc_Supply where IDMTSostav = @idsostav", id);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("DeleteSAPIncSupplySostav состав: {0}.", idsostav), eventID);
                return -1;
            }
        }
        /// <summary>
        /// Удалить строку справочника по номеру состава и вагона
        /// </summary>
        /// <param name="idsostav"></param>
        /// <param name="numvag"></param>
        /// <returns></returns>
        public int DeleteSAPIncSupply(int idsostav, int numvag) 
        {
            try 
            {
                SqlParameter id = new SqlParameter("@idsostav", idsostav);
                SqlParameter num = new SqlParameter("@num", numvag);
                return rep_sap.db.ExecuteSqlCommand("Delete RailWay.SAP_Inc_Supply where IDMTSostav = @idsostav and CarriageNumber = @num", id, num);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, String.Format("DeleteSAPIncSupply состав: {0}, вагон: {1}.", idsostav, numvag), eventID);
                return -1;
            }
        }
        /// <summary>
        /// Вернуть количество записей по составу
        /// </summary>
        /// <param name="idsostav"></param>
        /// <returns></returns>
        public int CountSAPIncSupply(int idsostav) 
        {
            IQueryable<SAPIncSupply> list = GetSAPIncSupply(idsostav);
            return list != null ? list.Count() : 0;
        }

    }
}
