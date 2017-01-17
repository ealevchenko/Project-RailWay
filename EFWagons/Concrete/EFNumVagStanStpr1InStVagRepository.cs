using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStanStpr1InStVagRepository : EFRepository, INumVagStanStpr1InStVagRepository
    {
        public IQueryable<NumVagStanStpr1InStVag> NumVagStanStpr1InStVag
        {
            get { return context.NumVagStanStpr1InStVag; }
        }
    }
}
