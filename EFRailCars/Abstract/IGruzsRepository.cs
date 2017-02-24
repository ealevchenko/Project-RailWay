using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IGruzsRepository : IDBRepository
    {
        IQueryable<GRUZS> GRUZS { get; }
        int SaveGRUZS(GRUZS GRUZS);
        GRUZS DeleteGRUZS(int id_gruz);
    }
}
