using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStpr1OutStDocRepository : EFRepository, INumVagStpr1OutStDocRepository
    {
        public IQueryable<NumVagStpr1OutStDoc> NumVagStpr1OutStDoc
        {
            get { return context.NumVagStpr1OutStDoc; }
        }
    }
}
