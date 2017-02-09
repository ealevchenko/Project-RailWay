using EFRailWay.Abstract.Reference;
using EFRailWay.Entities.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Reference
{
    public class EFCodeStateRepository : EFRepository, ICodeStateRepository
    {

        public IQueryable<Code_State> Code_State
        {
            get { return context.Code_State; }
        }
    }
}
