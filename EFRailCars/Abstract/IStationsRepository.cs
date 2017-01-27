using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IStationsRepository
    {
        IQueryable<STATIONS> STATIONS { get; }
        int SaveSTATIONS(STATIONS STATIONS);
        STATIONS DeleteSTATIONS(int id_stat);
    }
}
