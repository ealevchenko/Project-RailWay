using EFRailWay.Abstract.Reference;
using EFRailWay.Entities.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Reference
{
    public class EFCodeCargoRepository : EFRepository, ICodeCargoRepository
    {
        public IQueryable<Code_Cargo> Code_Cargo
        {
            get { return context.Code_Cargo; }
        }
    }
}
