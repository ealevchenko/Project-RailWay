using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFLogErrorsRepository : EFRepository, ILogErrorsRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<LogErrors> LogErrors
        {
            get { return context.LogErrors; }
        }
    }
}
