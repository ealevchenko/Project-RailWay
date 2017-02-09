using EFRailWay.Concrete.MT;
using EFRailWay.Entities.MT;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.MT
{
    public interface IMTRepository : IDBRepository
    {
        IQueryable<MTSostav> MTSostav { get; }
        IQueryable<MTList> MTList { get; }
        IQueryable<MTConsignee> MTConsignee { get; }
        int SaveMTSostav(MTSostav MTSostav);
        MTSostav DeleteMTSostav(int IDMTSostav);
        int SaveMTList(MTList MTList);
        MTList DeleteMTList(int IDMTList);
        int SaveMTConsignee(MTConsignee MTConsignee);
        MTConsignee DeleteMTConsignee(int Code);

    }
}
