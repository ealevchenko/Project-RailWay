using EFRailWay.Abstract.Railcars;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Railcars
{
    public class EFOwnersContriesRepository : EFRepository, IOwnersContriesRepository
    {
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<OWNERS_COUNTRIES> OWNERSCOUNTRIES
        {
            get { return context.OWNERS_COUNTRIES; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="WAYS"></param>
        /// <returns></returns>
        public int SaveOWNERSCOUNTRIES(OWNERS_COUNTRIES OWNERSCOUNTRIES)
        {
            OWNERS_COUNTRIES dbEntry;
            if (OWNERSCOUNTRIES.id_own_country == 0)
            {
                dbEntry = new OWNERS_COUNTRIES()
                {
                    id_own_country = OWNERSCOUNTRIES.id_own_country, 
                    name = OWNERSCOUNTRIES.name, 
                    id_ora = OWNERSCOUNTRIES.id_ora,
                      
                };
                context_edit.OWNERS_COUNTRIES.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.OWNERS_COUNTRIES.Find(OWNERSCOUNTRIES.id_own_country);
                if (dbEntry != null)
                {
                     dbEntry.name = OWNERSCOUNTRIES.name;
                     dbEntry.id_ora = OWNERSCOUNTRIES.id_ora;
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
            return dbEntry.id_own_country;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public OWNERS_COUNTRIES DeleteOWNERSCOUNTRIES(int id_own_country)
        {
            OWNERS_COUNTRIES dbEntry = context_edit.OWNERS_COUNTRIES.Find(id_own_country);
            if (dbEntry != null)
            {
                context_edit.OWNERS_COUNTRIES.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }


    }
}
