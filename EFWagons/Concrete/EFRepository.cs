using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFRepository
    {
        protected EFDbORCContext context = new EFDbORCContext();

        public EFRepository() { }

        public Database db
        {
            get { return context.Database; }
        }

    }
}
