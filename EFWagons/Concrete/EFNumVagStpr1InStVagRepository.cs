using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStpr1InStVagRepository : EFRepository, INumVagStpr1InStVagRepository
    {
        public IQueryable<NumVagStpr1InStVag> NumVagStpr1InStVag
        {
            get { return context.NumVagStpr1InStVag; }
        }
    }
}
