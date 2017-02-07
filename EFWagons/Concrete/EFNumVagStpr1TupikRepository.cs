using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFNumVagStpr1TupikRepository : EFRepository, INumVagStpr1TupikRepository
    {
        public IQueryable<NumVagStpr1Tupik> NumVagStpr1Tupik
        {
            get { return context.NumVagStpr1Tupik; }
        }
    }
}
