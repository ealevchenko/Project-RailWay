using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IWaysRepository
    {
        IQueryable<WAYS> WAYS { get; }
        int SaveWAYS(WAYS WAYS);
        WAYS DeleteWAYS(int id_way);
    }
}
