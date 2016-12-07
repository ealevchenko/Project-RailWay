using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFCodeCountryRepository : EFRepository, ICodeCountryRepository
    {
        public IQueryable<Code_Country> Code_Country
        {
            get { return context.Code_Country; }
        }
    }
}
