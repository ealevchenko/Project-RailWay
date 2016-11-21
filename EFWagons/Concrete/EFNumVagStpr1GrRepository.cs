using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStpr1GrRepository : INumVagStpr1GrRepository
    {
        private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<NumVagStpr1Gr> NumVagStpr1Gr
        {
            get { return context.NumVagStpr1Gr; }
        }
    }
}
