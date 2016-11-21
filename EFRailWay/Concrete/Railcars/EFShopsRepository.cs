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
    public class EFShopsRepository : EFRepository, IShopsRepository
    {
        private eventID eventID = eventID.EFRailWay_RailCars_EFShopsRepository;
        
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<SHOPS> SHOPS
        {
            get { return context.SHOPS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="SHOPS"></param>
        /// <returns></returns>
        public int SaveSHOPS(SHOPS SHOPS)
        {
            SHOPS dbEntry;
            if (SHOPS.id_shop == 0)
            {
                dbEntry = new SHOPS()
                {
                    id_shop = SHOPS.id_shop,
                    name = SHOPS.name,
                    name_full = SHOPS.name_full,
                    id_stat = SHOPS.id_stat,
                    id_ora = SHOPS.id_ora

                };
                context.SHOPS.Add(dbEntry);
            }
            else
            {
                dbEntry = context.SHOPS.Find(SHOPS.id_shop);
                if (dbEntry != null)
                {
                    dbEntry.name = SHOPS.name;
                    dbEntry.name_full = SHOPS.name_full;
                    dbEntry.id_stat = SHOPS.id_stat;
                    dbEntry.id_ora = SHOPS.id_ora;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveSHOPS", eventID);
                return -1;
            }
            return dbEntry.id_shop;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_shop"></param>
        /// <returns></returns>
        public SHOPS DeleteSHOPS(int id_shop)
        {
            SHOPS dbEntry = context.SHOPS.Find(id_shop);
            if (dbEntry != null)
            {
                context.SHOPS.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "SaveSHOPS", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
