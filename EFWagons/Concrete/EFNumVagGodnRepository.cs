using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{

    public class EFNumVagGodnRepository : EFRepository, INumVagGodnRepository
    {
        public IQueryable<NumVagGodn> NumVagGodn
        {
            get { return context.NumVagGodn; }
        }
    }
}
