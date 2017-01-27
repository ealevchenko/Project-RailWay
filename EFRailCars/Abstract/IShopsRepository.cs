using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IShopsRepository
    {
        IQueryable<SHOPS> SHOPS { get; }
        int SaveSHOPS(SHOPS SHOPS);
        SHOPS DeleteSHOPS(int id_shop);
    }
}
