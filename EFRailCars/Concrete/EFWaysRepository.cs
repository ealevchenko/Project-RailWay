using EFRailCars.Abstract;
using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Concrete
{
    public class EFWaysRepository : EFRepository, IWaysRepository
    {
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<WAYS> WAYS
        {
            get { return context.WAYS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="WAYS"></param>
        /// <returns></returns>
        public int SaveWAYS(WAYS WAYS)
        {
            WAYS dbEntry;
            if (WAYS.id_way == 0)
            {
                dbEntry = new WAYS()
                {
                    id_way = WAYS.id_way,
                    id_stat = WAYS.id_stat,
                    id_park = WAYS.id_park,
                    num = WAYS.num,
                    name = WAYS.name,
                    vag_capacity = WAYS.vag_capacity,
                    order = WAYS.order,
                    bind_id_cond = WAYS.bind_id_cond,
                    for_rospusk = WAYS.for_rospusk, 
                };
                context_edit.WAYS.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.WAYS.Find(WAYS.id_way);
                if (dbEntry != null)
                {
                    dbEntry.id_way = WAYS.id_way;
                    dbEntry.id_stat = WAYS.id_stat;
                    dbEntry.id_park = WAYS.id_park;
                    dbEntry.num = WAYS.num;
                    dbEntry.name = WAYS.name;
                    dbEntry.vag_capacity = WAYS.vag_capacity;
                    dbEntry.order = WAYS.order;
                    dbEntry.bind_id_cond = WAYS.bind_id_cond;
                    dbEntry.for_rospusk = WAYS.for_rospusk;
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
            return dbEntry.id_way;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public WAYS DeleteWAYS(int id_way)
        {
            WAYS dbEntry = context_edit.WAYS.Find(id_way);
            if (dbEntry != null)
            {
                context_edit.WAYS.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }

    }
}
