using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IOwnersContriesRepository
    {
        IQueryable<OWNERS_COUNTRIES> OWNERSCOUNTRIES { get; }
        int SaveOWNERSCOUNTRIES(OWNERS_COUNTRIES OWNERSCOUNTRIES);
        OWNERS_COUNTRIES DeleteOWNERSCOUNTRIES(int id_own_country);
    }
}
