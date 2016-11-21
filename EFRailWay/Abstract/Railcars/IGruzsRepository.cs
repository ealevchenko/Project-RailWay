using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IGruzsRepository
    {
        IQueryable<GRUZS> GRUZS { get; }
        int SaveGRUZS(GRUZS GRUZS);
        GRUZS DeleteGRUZS(int id_gruz);
    }
}
