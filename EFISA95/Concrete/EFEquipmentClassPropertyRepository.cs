using EFISA95.Abstract;
using EFISA95.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Concrete
{
    public class EFEquipmentClassPropertyRepository : IEquipmentClassPropertyRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<EquipmentClassProperty> EquipmentClassProperty
        {
            get { return context.EquipmentClassProperty; }
        }
    }
}
