using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFKometaSobstvForNaklRepository : IKometaSobstvForNaklRepository
    {
        private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<KometaSobstvForNakl> KometaSobstvForNakl
        {
            get { return context.KometaSobstvForNakl; }
        }
    }
}
