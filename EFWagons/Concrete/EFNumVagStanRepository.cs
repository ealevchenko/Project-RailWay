using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStanRepository : INumVagStanRepository
    {
        private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<NumVagStan> NumVagStan
        {
            get { return context.NumVagStan; }
        }
    }
}
