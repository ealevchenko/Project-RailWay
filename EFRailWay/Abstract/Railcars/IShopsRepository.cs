using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IShopsRepository
    {
        IQueryable<SHOPS> SHOPS { get; }
        int SaveSHOPS(SHOPS SHOPS);
        SHOPS DeleteSHOPS(int id_shop);
    }
}
