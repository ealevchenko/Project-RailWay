using EFRailWay.Abstract.Railcars;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Railcars
{
    public class EFStationsRepository : EFRepository, IStationsRepository
    {
        /// <summary>
        /// Получить
        /// </summary>
        public IQueryable<STATIONS> STATIONS
        {
            get { return context.STATIONS; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="STATIONS"></param>
        /// <returns></returns>
        public int SaveSTATIONS(STATIONS STATIONS)
        {
            STATIONS dbEntry;
            if (STATIONS.id_stat == 0)
            {
                dbEntry = new STATIONS()
                {
                    id_stat = STATIONS.id_stat,
                    name = STATIONS.name,
                    id_ora = STATIONS.id_ora,
                    outer_side = STATIONS.outer_side,
                    is_uz = STATIONS.is_uz,
                    
                };
                context_edit.STATIONS.Add(dbEntry);
            }
            else
            {
                dbEntry = context_edit.STATIONS.Find(STATIONS.id_stat);
                if (dbEntry != null)
                {
                    dbEntry.id_stat = STATIONS.id_stat;
                    dbEntry.name = STATIONS.name;
                    dbEntry.id_ora = STATIONS.id_ora;
                    dbEntry.outer_side = STATIONS.outer_side;
                    dbEntry.is_uz = STATIONS.is_uz;
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
            return dbEntry.id_stat;
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_stat"></param>
        /// <returns></returns>
        public STATIONS DeleteSTATIONS(int id_stat)
        {
            STATIONS dbEntry = context_edit.STATIONS.Find(id_stat);
            if (dbEntry != null)
            {
                context_edit.STATIONS.Remove(dbEntry);
                context_edit.SaveChanges();
            }
            return dbEntry;
        }

    }
}
