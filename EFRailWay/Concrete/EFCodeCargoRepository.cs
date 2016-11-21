using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFCodeCargoRepository : EFRepository, ICodeCargoRepository
    {
        public IQueryable<Code_Cargo> Code_Cargo
        {
            get { return context.Code_Cargo; }
        }
    }
}
