using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFRepository
    {
        protected EFDbContext context = new EFDbContext();
        protected EFDbContext context_edit = new EFDbContext();

        public EFRepository() { }

        public Database db
        {
            get { return context.Database; }
        }

    }
}
