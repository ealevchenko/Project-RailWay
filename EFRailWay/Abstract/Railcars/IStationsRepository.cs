using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IStationsRepository
    {
        IQueryable<STATIONS> STATIONS { get; }
        int SaveSTATIONS(STATIONS STATIONS);
        STATIONS DeleteSTATIONS(int id_stat);
    }
}
