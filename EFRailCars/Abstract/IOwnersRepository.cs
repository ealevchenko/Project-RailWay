using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IOwnersRepository
    {
        IQueryable<OWNERS> OWNERS { get; }
        int SaveOWNERS(OWNERS OWNERS);
        OWNERS DeleteOWNERS(int id_owner);
    }
}
