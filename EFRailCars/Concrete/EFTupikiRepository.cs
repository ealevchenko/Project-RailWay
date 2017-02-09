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
    public class EFTupikiRepository : EFRepository, ITupikiRepository
    {
        private eventID eventID = eventID.EFRailCars_RailCars_EFTupikiRepository;

        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<TUPIKI> TUPIKI
        {
            get { return context.TUPIKI; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="TUPIKI"></param>
        /// <returns></returns>
        public int SaveTUPIKI(TUPIKI tupiki)
        {
            TUPIKI dbEntry;
            if (tupiki.id_tupik == 0)
            {
                dbEntry = new TUPIKI()
                {
                    id_tupik = 0,
                    id_ora = tupiki.id_ora,
                    name = tupiki.name
                };
                context.TUPIKI.Add(dbEntry);
            }
            else
            {
                dbEntry = context.TUPIKI.Find(tupiki.id_tupik);
                if (dbEntry != null)
                {
                    dbEntry.id_ora = tupiki.id_ora;
                    dbEntry.name = tupiki.name;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveTUPIKI", eventID);
                return -1;
            }
            return dbEntry.id_tupik;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_tupik"></param>
        /// <returns></returns>
        public TUPIKI DeleteTUPIKI(int id_tupik)
        {
            TUPIKI dbEntry = context.TUPIKI.Find(id_tupik);
            if (dbEntry != null)
            {
                context.TUPIKI.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteTUPIKI", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
