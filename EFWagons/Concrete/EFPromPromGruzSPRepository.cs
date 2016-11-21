using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFPromGruzSPRepository : IPromGruzSPRepository
    {
        private EFDbORCContext context = new EFDbORCContext();
        public IQueryable<PromGruzSP> PromGruzSP
        {
            get { return context.PromGruzSP; }
        }
    }
}
