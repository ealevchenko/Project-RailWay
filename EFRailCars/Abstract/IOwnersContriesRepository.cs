using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Abstract
{
    public interface IOwnersContriesRepository : IDBRepository
    {
        IQueryable<OWNERS_COUNTRIES> OWNERSCOUNTRIES { get; }
        int SaveOWNERSCOUNTRIES(OWNERS_COUNTRIES OWNERSCOUNTRIES);
        OWNERS_COUNTRIES DeleteOWNERSCOUNTRIES(int id_own_country);
    }
}
