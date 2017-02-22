using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{

    public class EFNumVagGDStaitRepository : EFRepository, INumVagGDStaitRepository
    {

        public IQueryable<NumVagGDStait> NumVagGDStait
        {
            get { return context.NumVagGDStait; }
        }
    }
}
