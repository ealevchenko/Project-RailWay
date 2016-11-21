using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Railcars
{
    public interface IVagonsRepository
    {
        IQueryable<VAGONS> VAGONS { get; }
        int SaveVAGONS(VAGONS VAGONS);
        VAGONS DeleteVAGONS(int id_vag);
    }
}
