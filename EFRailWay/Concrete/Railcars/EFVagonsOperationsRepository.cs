using EFRailWay.Abstract.Railcars;
using EFRailWay.Entities.Railcars;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Railcars
{
    public class EFVagonsOperationsRepository : EFRepository, IVagonsOperationsRepository
    {
        private eventID eventID = eventID.EFRailWay_RailCars_EFVagonsOperationsRepository;

        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<VAGON_OPERATIONS> VAGON_OPERATIONS
        {
            get { return context.VAGON_OPERATIONS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="VAGONOPERATIONS"></param>
        /// <returns></returns>
        public int SaveVAGONOPERATIONS(VAGON_OPERATIONS VAGONOPERATIONS)
        {
            VAGON_OPERATIONS dbEntry;
            if (VAGONOPERATIONS.id_oper == 0)
            {
                dbEntry = new VAGON_OPERATIONS()
                {
                    id_oper = VAGONOPERATIONS.id_oper,
                    dt_amkr = VAGONOPERATIONS.dt_amkr,
                    n_natur = VAGONOPERATIONS.n_natur,
                    id_vagon = VAGONOPERATIONS.id_vagon,
                    id_stat = VAGONOPERATIONS.id_stat,
                    dt_from_stat = VAGONOPERATIONS.dt_from_stat,
                    dt_on_stat = VAGONOPERATIONS.dt_on_stat,
                    id_way = VAGONOPERATIONS.id_way,
                    dt_from_way = VAGONOPERATIONS.dt_from_way,
                    dt_on_way = VAGONOPERATIONS.dt_on_way,
                    num_vag_on_way = VAGONOPERATIONS.num_vag_on_way,
                    is_present = VAGONOPERATIONS.is_present,
                    id_locom = VAGONOPERATIONS.id_locom,
                    id_locom2 = VAGONOPERATIONS.id_locom2,
                    id_cond2 = VAGONOPERATIONS.id_cond2,
                    id_gruz = VAGONOPERATIONS.id_gruz,
                    id_gruz_amkr = VAGONOPERATIONS.id_gruz_amkr,
                    id_shop_gruz_for = VAGONOPERATIONS.id_shop_gruz_for,
                    weight_gruz = VAGONOPERATIONS.weight_gruz,
                    id_tupik = VAGONOPERATIONS.id_tupik,
                    id_nazn_country = VAGONOPERATIONS.id_nazn_country,
                    id_gdstait = VAGONOPERATIONS.id_gdstait,
                    id_cond = VAGONOPERATIONS.id_cond,
                    note = VAGONOPERATIONS.note,
                    is_hist = VAGONOPERATIONS.is_hist,
                    id_oracle = VAGONOPERATIONS.id_oracle,
                    lock_id_way = VAGONOPERATIONS.lock_id_way,
                    lock_order = VAGONOPERATIONS.lock_order,
                    lock_side = VAGONOPERATIONS.lock_side,
                    lock_id_locom = VAGONOPERATIONS.lock_id_locom,
                    st_lock_id_stat = VAGONOPERATIONS.st_lock_id_stat,
                    st_lock_order = VAGONOPERATIONS.st_lock_order,
                    st_lock_train = VAGONOPERATIONS.st_lock_train,
                    st_lock_side = VAGONOPERATIONS.st_lock_side,
                    st_gruz_front = VAGONOPERATIONS.st_gruz_front,
                    st_shop = VAGONOPERATIONS.st_shop,
                    oracle_k_st = VAGONOPERATIONS.oracle_k_st,
                    st_lock_locom1 = VAGONOPERATIONS.st_lock_locom1,
                    st_lock_locom2 = VAGONOPERATIONS.st_lock_locom2,
                    id_oper_parent = VAGONOPERATIONS.id_oper_parent,
                    grvu_SAP = VAGONOPERATIONS.grvu_SAP,
                    ngru_SAP = VAGONOPERATIONS.ngru_SAP,
                    id_ora_23_temp = VAGONOPERATIONS.id_ora_23_temp,
                    edit_user = VAGONOPERATIONS.edit_user,
                    edit_dt = VAGONOPERATIONS.edit_dt,
                    IDSostav = VAGONOPERATIONS.IDSostav,
                     num_vagon = VAGONOPERATIONS.num_vagon,
                };
                context_edit.VAGON_OPERATIONS.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.VAGON_OPERATIONS.Find(VAGONOPERATIONS.id_oper);
                if (dbEntry != null)
                {
                    dbEntry.id_oper = VAGONOPERATIONS.id_oper;
                    dbEntry.dt_amkr = VAGONOPERATIONS.dt_amkr;
                    dbEntry.n_natur = VAGONOPERATIONS.n_natur;
                    dbEntry.id_vagon = VAGONOPERATIONS.id_vagon;
                    dbEntry.id_stat = VAGONOPERATIONS.id_stat;
                    dbEntry.dt_from_stat = VAGONOPERATIONS.dt_from_stat;
                    dbEntry.dt_on_stat = VAGONOPERATIONS.dt_on_stat;
                    dbEntry.id_way = VAGONOPERATIONS.id_way;
                    dbEntry.dt_from_way = VAGONOPERATIONS.dt_from_way;
                    dbEntry.dt_on_way = VAGONOPERATIONS.dt_on_way;
                    dbEntry.num_vag_on_way = VAGONOPERATIONS.num_vag_on_way;
                    dbEntry.is_present = VAGONOPERATIONS.is_present;
                    dbEntry.id_locom = VAGONOPERATIONS.id_locom;
                    dbEntry.id_locom2 = VAGONOPERATIONS.id_locom2;
                    dbEntry.id_cond2 = VAGONOPERATIONS.id_cond2;
                    dbEntry.id_gruz = VAGONOPERATIONS.id_gruz;
                    dbEntry.id_gruz_amkr = VAGONOPERATIONS.id_gruz_amkr;
                    dbEntry.id_shop_gruz_for = VAGONOPERATIONS.id_shop_gruz_for;
                    dbEntry.weight_gruz = VAGONOPERATIONS.weight_gruz;
                    dbEntry.id_tupik = VAGONOPERATIONS.id_tupik;
                    dbEntry.id_nazn_country = VAGONOPERATIONS.id_nazn_country;
                    dbEntry.id_gdstait = VAGONOPERATIONS.id_gdstait;
                    dbEntry.id_cond = VAGONOPERATIONS.id_cond;
                    dbEntry.note = VAGONOPERATIONS.note;
                    dbEntry.is_hist = VAGONOPERATIONS.is_hist;
                    dbEntry.id_oracle = VAGONOPERATIONS.id_oracle;
                    dbEntry.lock_id_way = VAGONOPERATIONS.lock_id_way;
                    dbEntry.lock_order = VAGONOPERATIONS.lock_order;
                    dbEntry.lock_side = VAGONOPERATIONS.lock_side;
                    dbEntry.lock_id_locom = VAGONOPERATIONS.lock_id_locom;
                    dbEntry.st_lock_id_stat = VAGONOPERATIONS.st_lock_id_stat;
                    dbEntry.st_lock_order = VAGONOPERATIONS.st_lock_order;
                    dbEntry.st_lock_train = VAGONOPERATIONS.st_lock_train;
                    dbEntry.st_lock_side = VAGONOPERATIONS.st_lock_side;
                    dbEntry.st_gruz_front = VAGONOPERATIONS.st_gruz_front;
                    dbEntry.st_shop = VAGONOPERATIONS.st_shop;
                    dbEntry.oracle_k_st = VAGONOPERATIONS.oracle_k_st;
                    dbEntry.st_lock_locom1 = VAGONOPERATIONS.st_lock_locom1;
                    dbEntry.st_lock_locom2 = VAGONOPERATIONS.st_lock_locom2;
                    dbEntry.id_oper_parent = VAGONOPERATIONS.id_oper_parent;
                    dbEntry.grvu_SAP = VAGONOPERATIONS.grvu_SAP;
                    dbEntry.ngru_SAP = VAGONOPERATIONS.ngru_SAP;
                    dbEntry.id_ora_23_temp = VAGONOPERATIONS.id_ora_23_temp;
                    dbEntry.edit_user = VAGONOPERATIONS.edit_user;
                    dbEntry.edit_dt = VAGONOPERATIONS.edit_dt;
                    dbEntry.IDSostav = VAGONOPERATIONS.IDSostav;
                    dbEntry.num_vagon = VAGONOPERATIONS.num_vagon;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveVAGONOPERATIONS", eventID);
                return -1;
            }
            return dbEntry.id_oper;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public VAGON_OPERATIONS DeleteVAGONOPERATIONS(int id_oper)
        {
            VAGON_OPERATIONS dbEntry = context_edit.VAGON_OPERATIONS.Find(id_oper);
            if (dbEntry != null)
            {
                context_edit.VAGON_OPERATIONS.Remove(dbEntry);
                try
                {
                    context_edit.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteVAGONOPERATIONS", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
