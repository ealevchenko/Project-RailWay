using EFRailWay.Abstract.Railcars;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Railcars
{
    public class EFGruzsRepository : EFRepository, IGruzsRepository
    {
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<GRUZS> GRUZS
        {
            get { return context.GRUZS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="GRUZS"></param>
        /// <returns></returns>
        public int SaveGRUZS(GRUZS GRUZS)
        {
            GRUZS dbEntry;
            if (GRUZS.id_gruz == 0)
            {
                dbEntry = new GRUZS()
                {
                    id_gruz = 0,
                    name = GRUZS.name,
                    name_full = GRUZS.name_full,
                    id_ora = GRUZS.id_ora,
                    id_ora2 = GRUZS.id_ora2, 
                    ETSNG = GRUZS.ETSNG
                };
                context_edit.GRUZS.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.GRUZS.Find(GRUZS.id_gruz);
                if (dbEntry != null)
                {
                    dbEntry.name = GRUZS.name;
                    dbEntry.name_full = GRUZS.name_full;
                    dbEntry.id_ora = GRUZS.id_ora;
                    dbEntry.id_ora2 = GRUZS.id_ora2;
                    dbEntry.ETSNG = GRUZS.ETSNG;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e)
            {
                return -1;
            }
            return dbEntry.id_gruz;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_gruz"></param>
        /// <returns></returns>
        public GRUZS DeleteGRUZS(int id_gruz)
        {
            GRUZS dbEntry = context_edit.GRUZS.Find(id_gruz);
            if (dbEntry != null)
            {
                context_edit.GRUZS.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
    }
}
