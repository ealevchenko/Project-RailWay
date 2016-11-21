using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFKometaVagonSobRepository : IKometaVagonSobRepository
    {
        private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<KometaVagonSob> KometaVagonSob
        {
            get { return context.KometaVagonSob; }
        }
    }
}
