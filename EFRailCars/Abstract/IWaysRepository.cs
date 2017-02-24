using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IWaysRepository : IDBRepository
    {
        IQueryable<WAYS> WAYS { get; }
        int SaveWAYS(WAYS WAYS);
        WAYS DeleteWAYS(int id_way);
    }
}
