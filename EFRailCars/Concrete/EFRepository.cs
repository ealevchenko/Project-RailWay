using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Concrete
{
    public class EFRepository
    {
        protected EFDbContext context = new EFDbContext();
        protected EFDbContext context_edit = new EFDbContext(); //TODO: !!!! Удалить сделать одну (context) проблема после изменения данных нет обновления

        public EFRepository() { }

        public Database db
        {
            get { return context.Database; }
        }

    }
}
