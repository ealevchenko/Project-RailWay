using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStanStpr1InStDocRepository : EFRepository, INumVagStanStpr1InStDocRepository
    {
        //private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<NumVagStanStpr1InStDoc> NumVagStanStpr1InStDoc
        {
            get { return context.NumVagStanStpr1InStDoc; }
        }
    }
}
