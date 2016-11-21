using EFRailWay.Abstract.Railcars;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Railcars
{
    public class EFOwnersRepository : EFRepository, IOwnersRepository
    {
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<OWNERS> OWNERS
        {
            get { return context.OWNERS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="WAYS"></param>
        /// <returns></returns>
        public int SaveOWNERS(OWNERS OWNERS)
        {
            OWNERS dbEntry;
            if (OWNERS.id_owner == 0)
            {
                dbEntry = new OWNERS()
                {
                    id_owner = OWNERS.id_owner,
                    name = OWNERS.name,
                    abr = OWNERS.abr,
                    id_country = OWNERS.id_country,
                    id_ora = OWNERS.id_ora,
                    id_ora_temp = OWNERS.id_ora_temp,
                };
                context_edit.OWNERS.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.OWNERS.Find(OWNERS.id_owner);
                if (dbEntry != null)
                {
                    dbEntry.id_owner = OWNERS.id_owner;
                    dbEntry.name = OWNERS.name;
                    dbEntry.abr = OWNERS.abr;
                    dbEntry.id_country = OWNERS.id_country;
                    dbEntry.id_ora = OWNERS.id_ora;
                    dbEntry.id_ora_temp = OWNERS.id_ora_temp;
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
            return dbEntry.id_owner;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public OWNERS DeleteOWNERS(int id_owner)
        {
            OWNERS dbEntry = context_edit.OWNERS.Find(id_owner);
            if (dbEntry != null)
            {
                context_edit.OWNERS.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }
    }
}
