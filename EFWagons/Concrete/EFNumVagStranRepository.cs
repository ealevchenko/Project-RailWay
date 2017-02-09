using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStranRepository : EFRepository, INumVagStranRepository
    {
        public IQueryable<NumVagStran> NumVagStran
        {
            get { return context.NumVagStran; }
        }
    }
}
