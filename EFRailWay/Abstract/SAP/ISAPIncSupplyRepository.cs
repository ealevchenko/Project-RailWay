using EFRailWay.Entities;
using EFRailWay.Entities.SAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.SAP
{
    public interface ISAPIncSupplyRepository : IDBRepository
    {
        IQueryable<SAPIncSupply> SAPIncSupply { get; }
        int SaveSAPIncSupply(SAPIncSupply SAPIncSupply);
        SAPIncSupply DeleteSAPIncSupply(int id);
    }
}
