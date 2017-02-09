using EFRailWay.Abstract.Reference;
using EFRailWay.Entities.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Reference
{
    public class EFCodeInternalRailroadRepository : EFRepository, ICodeInternalRailroadRepository
    {
        public IQueryable<Code_InternalRailroad> Code_InternalRailroad
        {
            get { return context.Code_InternalRailroad; }
        }
    }
}
