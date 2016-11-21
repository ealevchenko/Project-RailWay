using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface ICodeInternalRailroadRepository
    {
        IQueryable<Code_InternalRailroad> Code_InternalRailroad { get; }
    }
}
