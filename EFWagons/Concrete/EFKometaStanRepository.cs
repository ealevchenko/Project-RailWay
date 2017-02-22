using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{

    public class EFKometaStanRepository : EFRepository, IKometaStanRepository
    {

        public IQueryable<KometaStan> KometaStan
        {
            get { return context.KometaStan; }
        }
    }
}
