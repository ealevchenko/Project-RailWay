using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStpr1OutStVagRepository : EFRepository, INumVagStpr1OutStVagRepository
    {
        public IQueryable<NumVagStpr1OutStVag> NumVagStpr1OutStVag
        {
            get { return context.NumVagStpr1OutStVag; }
        }
    }
}
