using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStpr1InStDocRepository : EFRepository, INumVagStpr1InStDocRepository
    {
        //private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<NumVagStpr1InStDoc> NumVagStpr1InStDoc
        {
            get { return context.NumVagStpr1InStDoc; }
        }
    }
}
