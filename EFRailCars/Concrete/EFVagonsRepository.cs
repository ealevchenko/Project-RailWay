using EFRailCars.Abstract;
using EFRailCars.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Concrete
{
    public class EFVagonsRepository : EFRepository, IVagonsRepository
    {
        private eventID eventID = eventID.EFRailCars_RailCars_EFVagonsRepository;
        
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<VAGONS> VAGONS
        {
            get { return context.VAGONS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="WAYS"></param>
        /// <returns></returns>
        public int SaveVAGONS(VAGONS VAGONS)
        {
            VAGONS dbEntry;
            if (VAGONS.id_vag == 0)
            {
                dbEntry = new VAGONS()
                {
                    id_vag =  VAGONS.id_vag,
                    num =  VAGONS.num, 
                    id_ora =  VAGONS.id_ora, 
                    id_owner =  VAGONS.id_owner, 
                    id_stat  =  VAGONS.id_stat,
                    is_locom =  VAGONS.is_locom, 
                    locom_seria =  VAGONS.locom_seria, 
                    rod =  VAGONS.rod, 
                    st_otpr =  VAGONS.st_otpr, 
                    date_ar =  VAGONS.date_ar, 
                    date_end =  VAGONS.date_end,
                    date_in = VAGONS.date_in,
                    IDSostav =VAGONS.IDSostav, 
                    Natur = VAGONS.Natur,
                    Transit=VAGONS.Transit
                };
                context_edit.VAGONS.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.VAGONS.Find(VAGONS.id_vag);
                if (dbEntry != null)
                {
                    //dbEntry.id_vag =  VAGONS.id_vag;
                    dbEntry.num =  VAGONS.num; 
                    dbEntry.id_ora =  VAGONS.id_ora; 
                    dbEntry.id_owner =  VAGONS.id_owner; 
                    dbEntry.id_stat  =  VAGONS.id_stat;
                    dbEntry.is_locom =  VAGONS.is_locom; 
                    dbEntry.locom_seria =  VAGONS.locom_seria; 
                    dbEntry.rod =  VAGONS.rod; 
                    dbEntry.st_otpr =  VAGONS.st_otpr; 
                    dbEntry.date_ar =  VAGONS.date_ar;
                    dbEntry.date_end = VAGONS.date_end;
                    dbEntry.date_in = VAGONS.date_in;
                    dbEntry.IDSostav =VAGONS.IDSostav;
                    dbEntry.Natur = VAGONS.Natur;
                    dbEntry.Transit = VAGONS.Transit;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveVAGONS", eventID);
                return -1;
            }
            return dbEntry.id_vag;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public VAGONS DeleteVAGONS(int id_vag)
        {
            VAGONS dbEntry = context_edit.VAGONS.Find(id_vag);
            if (dbEntry != null)
            {
                context_edit.VAGONS.Remove(dbEntry);
                try
                {
                    context_edit.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteVAGONS", eventID);
                    return null;
                }
            }
            return dbEntry;
        }

    }
}
