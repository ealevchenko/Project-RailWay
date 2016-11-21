using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFKometaStranaRepository : IKometaStranaRepository
    {
        private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<KometaStrana> KometaStrana
        {
            get { return context.KometaStrana; }
        }
    }
}
