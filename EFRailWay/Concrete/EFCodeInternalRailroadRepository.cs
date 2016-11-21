using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFCodeInternalRailroadRepository : EFRepository, ICodeInternalRailroadRepository
    {
        public IQueryable<Code_InternalRailroad> Code_InternalRailroad
        {
            get { return context.Code_InternalRailroad; }
        }
    }
}
